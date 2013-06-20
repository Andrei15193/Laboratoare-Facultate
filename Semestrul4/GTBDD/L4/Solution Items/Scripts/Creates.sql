if exists(select name from sys.objects where type = 'U' and name = 'Transactions')
begin
    if exists(select name from sys.objects where type = 'U' and name = 'TransactionOperations')
        drop table TransactionOperations
    drop table Transactions
end
go
create table Transactions(
    id int identity(1, 1) not null,
    started datetime not null,
    ended datetime null,
    type char not null,
    result char null,
    constraint pkTransactions primary key (id),
    constraint checkTransactionType check (type = 'O' or type = 'P')
)
go

if exists(select name from sys.objects where type = 'U' and name = 'TransactionOperations')
    drop table TransactionOperations
go
create table TransactionOperations(
    transactionId int not null,
    tableName varchar(200) not null,
    operation varchar(1000) not null,
    operationMoment datetime not null,
    lockType char not null,
    constraint fkTranzactionOperationsToTranzaction foreign key (transactionId) references Transactions(id),
    constraint checkTransactionOperationLockType check (lockType = 'S' or lockType = 'X')
)
go

if exists(select name from sys.objects where type = 'P' and name = 'StartTransaction')
    drop procedure StartTransaction
go
create procedure StartTransaction @type char, @id int output as
begin
    insert into Transactions(started, type)
        values (CURRENT_TIMESTAMP, @type)
    set @id = @@IDENTITY
end
go

if exists(select name from sys.objects where type = 'P' and name = 'SubmitOperation')
    drop procedure SubmitOperation
go
create procedure SubmitOperation @transactionId int, @operation varchar(1000), @table varchar(100) as
begin
    if (select CHARINDEX('select', @operation)) > 0
    begin
        insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
            values (@operation, CURRENT_TIMESTAMP, @table, 'S', @transactionId)
        exec(@operation)
    end
    else
        insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
            values (@operation, CURRENT_TIMESTAMP, @table, 'X', @transactionId)
end
go

if exists(select name from sys.objects where type = 'P' and name = 'CommitTransaction')
    drop procedure CommitTransaction
go
create procedure CommitTransaction @transactionId int as
begin
    if (select type from Transactions where id = @transactionId) = 'O'
        and dbo.ValidateOcc(@transactionId) = 0
    begin
        update Transactions
            set ended = CURRENT_TIMESTAMP, result = 'R'
            where id = @transactionId
        raiserror('There are conflicts! Could not commit transaction.', 18, 1)
    end
    else
    begin
        declare @command varchar(1000)
        declare c cursor for    select operation
                                    from TransactionOperations
                                    where transactionId = @transactionId and lockType = 'X'
                                    order by operationMoment
        open c
        fetch next from c into @command
        while @@FETCH_STATUS = 0
        begin
            exec(@command)
            fetch next from c into @command
        end
        close c
        deallocate c
        update Transactions
            set ended = CURRENT_TIMESTAMP, result = 'C'
            where id = @transactionId
    end
end
go

if exists(select name from sys.objects where type = 'P' and name = 'RollbackTransaction')
    drop procedure RollbackTransaction
go
create procedure RollbackTransaction @transactionId int as
begin
    update Transactions
        set ended = CURRENT_TIMESTAMP, result = 'R'
        where id = @transactionId
end
go

if exists(select name from sys.objects where type = 'FN' and name = 'ValidateOccAgainstOcc')
    drop function ValidateOccAgainstOcc
go
-- Returns 1 if valid, 0 otherwise
create function ValidateOccAgainstOcc(@transactionId int) returns bit as
begin
    declare @result bit
    if exists(  select transactionId
                    from
                    (
                        select id, ended
                            from Transactions
                            where type = 'O' and result is not null and result = 'C'
                    ) as CommitedOptimisticTransactions
                    inner join
                    (
                        select transactionId
                            from TransactionOperations
                            where
                                TransactionOperations.lockType = 'X'
                                and transactionId <> @transactionId
                                and tableName in (select tableName from TransactionOperations where transactionId = @transactionId)
                    ) as CommitedOptimisticTransactionOperations
                        on transactionId = id
                    inner join
                    (
                        select operationMoment
                            from TransactionOperations
                            where transactionId = @transactionId
                    ) as CurrentTransactionOperations
                        on operationMoment < ended)
        set @result = 0
    else
        set @result = 1
    return @result
end
go

if exists(select name from sys.objects where type = 'FN' and name = 'ValidateOccAgainstCc')
    drop function ValidateOccAgainstCc
go
-- Returns 1 if valid, 0 otherwise
create function ValidateOccAgainstCc(@transactionId int) returns bit as
begin
    declare @result bit
    if exists( select id
                    from
                        (
                            select id, ended
                                from Transactions
                                where type = 'P'
                        ) as PesimisticTransactions
                        inner join
                        (
                            select transactionId
                                from TransactionOperations
                                where tableName in (select tableName from TransactionOperations where transactionId = @transactionId)
                        ) as OperationsOnTheSameTables
                            on id = transactionId
                        inner join
                        (
                            select *
                                from TransactionOperations
                                where transactionId = @transactionId
                        ) as CurrentTransactionOperations
                            on  PesimisticTransactions.ended is null or
                                CurrentTransactionOperations.operationMoment < PesimisticTransactions.ended)
        set @result = 0
    else
        set @result = 1
    return @result
end
go

if exists(select name from sys.objects where type = 'FN' and name = 'ValidateOcc')
    drop function ValidateOcc
go
-- Returns 1 if valid, 0 otherwise
create function ValidateOcc(@transactionId int) returns bit as
begin
    declare @result bit
    if dbo.ValidateOccAgainstOcc(@transactionId) = 0 or dbo.ValidateOccAgainstCc(@transactionId) = 0
        set @result = 0
    else
        set @result = 1
    return @result
end
go

if exists(select name from sys.objects where type = 'TR' and name = 'ccValidationTrigger')
    drop trigger ccValidationTrigger
go
create trigger ccValidationTrigger on TransactionOperations
instead of insert as
begin
    if (select Transactions.type
            from inserted inner join Transactions
                on inserted.transactionId = Transactions.id) = 'P'
        and exists(  select *
                            from inserted inner join TransactionOperations
                                on  (
                                        inserted.lockType <> TransactionOperations.lockType
                                        or (inserted.lockType = 'X' and TransactionOperations.lockType = 'X')
                                    )
                                    and inserted.tableName = TransactionOperations.tableName
                                    and inserted.transactionId <> TransactionOperations.transactionId
                            inner join
                            (
                                select id
                                    from Transactions
                                    where type = 'P' and result is null
                            ) as PessimisticTransactions
                                on  PessimisticTransactions.id = TransactionOperations.transactionId)
        raiserror('Could not achieve exclusive lock',18, 1)
    else
        insert into TransactionOperations(lockType, operation, operationMoment, tableName, transactionId)
            select lockType, operation, operationMoment, tableName, transactionId
                from inserted
end
go

if exists(select name from sys.objects where type = 'U' and name = 'Note')
    drop table Note
go
create table Note(
    materie varchar(100),
    student varchar(100),
    nota int,
)
go

insert into Note(materie, student, nota)
    values ('BMC', 'Andrei', 10)
insert into Note(materie, student, nota)
    values ('OOP', 'Florin', 10)
insert into Note(materie, student, nota)
    values ('FP', 'Elena', 10)
insert into Note(materie, student, nota)
    values ('MAP', 'Andrei', 10)

select * from TransactionOperations
select * from Transactions
select * from Note

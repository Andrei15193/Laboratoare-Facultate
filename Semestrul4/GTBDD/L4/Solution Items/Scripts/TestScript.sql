print 'No operations: ' + Convert(varchar(1), dbo.ValidateOcc(1))

insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'O')
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'O')
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 'S', 1)
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 'S', 2)
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('insert', CURRENT_TIMESTAMP, 'tabel', 'X', 2)
update Transactions set ended = CURRENT_TIMESTAMP, result = 'C' where id = 2
print 'optimistic OCC conflict (transaction id 1): ' + Convert(varchar(1), dbo.ValidateOcc(1))
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 1

insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'P')
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'O')
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('insert', CURRENT_TIMESTAMP, 'tabel', 'X', 3)
update Transactions set ended = CURRENT_TIMESTAMP, result = 'C' where id = 3
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 'S', 4)
print 'NO optimistic CC conflict (transaction id 4): ' + Convert(varchar(1), dbo.ValidateOcc(4))
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 4

insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'P')
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'O')
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('insert', CURRENT_TIMESTAMP, 'tabel', 'X', 5)
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('insert', CURRENT_TIMESTAMP, 'tabel', 'X', 6)
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 'S', 6)
print 'optimistic CC conflict (transaction id 6): ' + Convert(varchar(1), dbo.ValidateOcc(6))
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 5
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 6

insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'O')
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'O')
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 's', 7)
update Transactions set ended = CURRENT_TIMESTAMP, result = 'C' where id = 7
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 's', 8)
print 'NO optimistic OCC conflict (transaction id 8): ' + Convert(varchar(1), dbo.ValidateOcc(8))
update Transactions set ended = CURRENT_TIMESTAMP, result = 'C' where id = 8

print 'no CC conflict:'
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'P')
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'P')
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 's', 9)
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 's', 10)
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 9
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 10

print 'CC conflict:'
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'P')
insert into Transactions(started, type)
    values (CURRENT_TIMESTAMP, 'P')
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 'x', 11)
insert into TransactionOperations(operation, operationMoment, tableName, lockType, transactionId)
    values ('select', CURRENT_TIMESTAMP, 'tabel', 's', 12)
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 11
update Transactions set ended = CURRENT_TIMESTAMP, result = 'R' where id = 12

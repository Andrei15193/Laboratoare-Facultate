if exists(select name from sys.objects where type = 'P' and name = 'CreazaCalendar')
    drop procedure CreazaCalendar
go
create procedure CreazaCalendar as
begin
    if exists(select name from sys.objects where type = 'U' and name = 'Calendar')
    begin
        if exists(select name from sys.objects where type = 'U' and name = 'OferteProduse')
            drop table OferteProduse
        drop table Calendar
    end
    create table Calendar(
        codZi int identity(1, 1),
        data date,
        ziSaptamana int,
        luna int,
        an int,
        constraint pkCalendar primary key (codZi)
    )

    declare @dataMinimaCataloage date = (select MIN(CONVERT(date, CONVERT(varchar, Cataloage.luna) + '/1/' + CONVERT(varchar, Cataloage.an))) from Cataloage)
    declare @dataMinimaOferteSpeciale date = (select MIN(dela) from OferteSpeciale)
    declare @dataMaximaCataloage date = (select MAX(CONVERT(date, CONVERT(varchar, Cataloage.luna) + '/1/' + CONVERT(varchar, Cataloage.an))) from Cataloage)
    declare @dataMaximaOferteSpeciale date = (select MAX(dela) from OferteSpeciale)
    declare @curent date
    declare @final date

    if @dataMinimaCataloage < @dataMinimaOferteSpeciale
        set @curent = @dataMinimaCataloage
    else
        set @curent = @dataMinimaOferteSpeciale
    if @dataMaximaCataloage < @dataMaximaOferteSpeciale
        set @final = @dataMaximaOferteSpeciale
    else
        set @final = @dataMaximaCataloage

    while @curent < @final
    begin
        insert into Calendar(data, ziSaptamana, luna, an)
            values(@curent, DATEPART(WEEKDAY, @curent), MONTH(@curent), YEAR(@curent))
        set @curent = DATEADD(DAY, 1, @curent)
    end
end
go

if exists(select name from sys.objects where type = 'P' and name = 'CreazaOferteProduse')
    drop procedure CreazaOferteProduse
go
create procedure CreazaOferteProduse as
begin
    if exists(select name from sys.objects where type = 'U' and name = 'OferteProduse')
        drop table OferteProduse
    create table OferteProduse(
        codOferta int identity(1, 1),
        codP int,
        codM int,
        codZi int,
        pret int,
        catalogSauOferta char,
        constraint pkOferteProduse primary key (codOferta),
        constraint fkOferteProduseProduse foreign key (codP) references Produse(codP),
        constraint fkOferteProduseMagazine foreign key (codM) references Magazine(codM),
        constraint fkOferteProduseCalendar foreign key (codZi) references Calendar(codZi)
    )
    insert into OferteProduse (codP, codM, codZi, pret, catalogSauOferta)
        select OferteSpeciale.codP, OferteSpeciale.codM, Calendar.codZi, OferteSpeciale.pret, 'O'
            from OferteSpeciale inner join Calendar
                on OferteSpeciale.dela < Calendar.data and Calendar.data <= OferteSpeciale.panala
    insert into OferteProduse (codP, codM, codZi, pret, catalogSauOferta)
        select Cataloage.codP, Cataloage.codM, Calendar.codZi, Cataloage.pret, 'C'
            from Cataloage inner join Calendar
                on Cataloage.an = Calendar.an and Cataloage.luna = Calendar.luna
            where Calendar.data not in  (
                                            select Calendar.data
                                                from Calendar inner join OferteProduse
                                                    on Calendar.codZi = OferteProduse.codZi
                                        )
end
go

if exists(select name from sys.objects where type = 'P' and name = 'Problema4')
    drop procedure Problema4
go
create procedure Problema4 as
begin
    execute CreazaCalendar
    execute CreazaOferteProduse
end
go

execute Problema4
select OferteProduse.* from OferteProduse

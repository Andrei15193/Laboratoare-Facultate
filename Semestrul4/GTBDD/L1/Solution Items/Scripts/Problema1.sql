if exists(select name from sys.objects where type = 'P' and name = 'CreazaMagazine')
    drop procedure CreazaMagazine
go
create procedure CreazaMagazine as
begin
    if exists(select name from sys.objects where type = 'U' and name = 'Magazine')
    begin
        if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
            drop table Cataloage
        if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
            drop table OferteSpeciale
        drop table Magazine
    end
    create table Magazine(
        codM int,
        denumire varchar(30),
        adresa varchar(30),
        constraint pkMagazine primary key (codM)
    )
    insert into Magazine(codM, denumire, adresa)
        values(1, 'Billa', 'Calea Floresti, nr. 56')
    insert into Magazine(codM, denumire, adresa)
        values(2, 'Carrefour', 'str. Avram Iancu, nr.492 - 500')
    insert into Magazine(codM, denumire, adresa)
        values(3, 'Cora', 'Bd. 1 Decembrie 1918, nr.142')
end
go


if exists(select name from sys.objects where type = 'P' and name = 'CreazaProduse')
    drop procedure CreazaProduse
go
create procedure CreazaProduse as
begin
    if exists(select name from sys.objects where type = 'U' and name = 'Produse')
    begin
        if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
            drop table Cataloage
        if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
            drop table OferteSpeciale
        drop table Produse
    end
    create table Produse(
        codP int,
        denumire varchar(30),
        um varchar(30),
        constraint pkProduse primary key (codP)
    )
    insert into Produse (codP, denumire, um)
        values(1, 'Ciocolata', 'grame')
    insert into Produse (codP, denumire, um)
        values(2, 'Alune', 'grame')
    insert into Produse (codP, denumire, um)
        values(3, 'Apa', 'litri')
end
go

if exists(select name from sys.objects where type = 'P' and name = 'CreazaCataloage')
    drop procedure CreazaCataloage
go
create procedure CreazaCataloage as
begin
    if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
        drop table Cataloage
    create table Cataloage(
        codP int,
        codM int,
        an int,
        luna int,
        pret int,
        constraint pkCataloage primary key (codP, codM, an, luna),
        constraint fkCataloageProduse foreign key (codP) references Produse(codP),
        constraint fkCataloageMagazine foreign key (codM) references Magazine(codM)
    )
    insert into Cataloage(codP, codM, an, luna, pret)
        values(1, 1, 2013, 6, 10)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(1, 1, 2013, 7, 12)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(1, 1, 2013, 5, 11)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(2, 1, 2013, 5, 5)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(2, 1, 2013, 6, 6)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(2, 1, 2013, 7, 7)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(3, 2, 2013, 5, 5)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(3, 2, 2013, 6, 5)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(3, 2, 2013, 7, 5)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(2, 3, 2013, 5, 6)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(2, 3, 2013, 6, 7)
    insert into Cataloage(codP, codM, an, luna, pret)
        values(2, 3, 2013, 7, 5)
end
go

if exists(select name from sys.objects where type = 'P' and name = 'CreazaOferteSpeciale')
    drop procedure CreazaOferteSpeciale
go
create procedure CreazaOferteSpeciale as
begin
    if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
        drop table OferteSpeciale
    create table OferteSpeciale(
        codP int,
        codM int,
        dela date,
        panala date,
        pret int,
        constraint pkOferteSpeciale primary key (codP, codM, dela),
        constraint fkOferteSpecialeProduse foreign key (codP) references Produse(codP),
        constraint fkOferteSpecialeMagazine foreign key (codM) references Magazine(codM)
    )
    insert into OferteSpeciale (codP, codM, dela, panala, pret)
        values(1, 1, '1/15/2013', '1/20/2013', 5)
    insert into OferteSpeciale (codP, codM, dela, panala, pret)
        values(1, 2, '1/15/2013', '1/20/2013', 4)
end
go

if exists(select name from sys.objects where type = 'P' and name = 'Problema1')
    drop procedure Problema1
go
create procedure Problema1 as
begin
    execute CreazaMagazine
    execute CreazaProduse
    execute CreazaCataloage
    execute CreazaOferteSpeciale
end
go

execute Problema1

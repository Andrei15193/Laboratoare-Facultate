if exists(select name from sys.objects where type = 'U' and name = 'Magazine')
begin
    if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
        drop table OferteSpeciale
    if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
        drop table Cataloage
    drop table Magazine
end
go
create table Magazine(
    codM int,
    denumire varchar(100),
    adresa varchar(100),
    constraint pkMagazine primary key (codM)
)
go

if exists(select name from sys.objects where type = 'U' and name = 'Produse')
begin
    if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
        drop table OferteSpeciale
    if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
        drop table Cataloage
    if exists(select name from sys.objects where type = 'U' and name = 'DetaliiProduse')
        drop table DetaliiProduse
    drop table Produse
end
go
create table Produse(
    codP int,
    denumire varchar(100),
    um varchar(30),
    constraint pkProduse primary key (codP)
)
go

if exists(select name from sys.objects where type = 'U' and name = 'DetaliiProduse')
    drop table DetaliiProduse
go
create table DetaliiProduse(
    codP int,
    numar int,
    caracteristica varchar(100),
    valoare varchar(100),
    constraint fkDetaliiProduseProduse foreign key (codP) references Produse(codP)
)
go

if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
    drop table Cataloage
go
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
go

if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
    drop table OferteSpeciale
go
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
go

insert into Magazine(codM, denumire, adresa)
    values (1, 'Billa', 'Undeva in Cluj')

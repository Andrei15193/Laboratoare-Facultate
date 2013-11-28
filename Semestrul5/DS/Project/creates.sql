if db_id('Local') is null
    create database Local
go

use Local
go

if exists(select name from sys.objects where type = 'u' and name = 'Purchases')
    drop table Purchases
go

if exists(select name from sys.objects where type = 'u' and name = 'Products')
    drop table Products
go

if exists(select name from sys.objects where type = 'u' and name = 'Producers')
    drop table Producers
go

if exists(select name from sys.objects where type = 'u' and name = 'Shops')
    drop table Shops
go

if exists(select name from sys.objects where type = 'u' and name = 'Addresses')
    drop table Addresses
go

if exists(select name from sys.objects where type = 'u' and name = 'Incomes')
    drop table Incomes
go

if exists(select name from sys.objects where type = 'u' and name = 'Persons')
    drop table Persons
go

create table Persons(
    name varchar(100),
    preferedCurrency varchar(10) not null,
    constraint pkPersons primary key (name)
)
go

create table Incomes(
    sum int not null,
    dateReceived datetime not null,
    person varchar(100) not null,
    constraint fkIncomesToPerson foreign key (person) references Persons(name)
)
go

create table Addresses(
    id int,
    street varchar(100) not null,
    city varchar(100) not null,
    county varchar(100) not null,
    country varchar(100) not null,
    constraint pkAddresses primary key (id),
    constraint uqAddresses unique (street, city, county, country)
)
go

create table Shops(
    name varchar(100) not null,
    address int not null,
    type int not null,
    constraint pkShops primary key (name, address),
    constraint fkShopsToAddress foreign key (address) references Addresses(id)
)
go

create table Producers(
    name varchar(100),
    country varchar(100) not null,
    constraint pkProducer primary key (name)
)
go

create table Products(
    name varchar(100) not null,
    type int not null,
    producer varchar(100) not null,
    constraint pkProducts primary key (name, producer),
    constraint fkProductsToProducer foreign key (producer) references Producers(name)
)
go

create table Purchases(
    price int not null,
    quantity int not null,
    shop varchar(100) not null,
    shopAddress int not null,
    purchaser varchar(100) not null,
    product varchar(100) not null,
    productProducer varchar(100) not null,
    datePurchased datetime not null,
    constraint fkPurchasesToProduct foreign key (product, productProducer) references Products(name, producer),
    constraint fkPurchasesToPerson foreign key (purchaser) references Persons(name),
    constraint fkPurchasesToShop foreign key (shop, shopAddress) references Shops(name, address)
)
go

declare @addressId int = (select (ISNULL(max(id), 0) + 1) from Addresses)

insert into Persons(name, preferedCurrency)
    values ('Andrei', 'Euro')

insert into Incomes(dateReceived, person, sum)
    values (CURRENT_TIMESTAMP, 'Andrei', 1000)

insert into Addresses(city, country, county, id, street)
    values ('Cluj-Napoca', 'Romania', 'Cluj', @addressId, 'Calea Floresti 514')

insert into Shops(address, name, type)
    values (@addressId, 'Billa', 0)

insert into Producers(country, name)
    values ('Romania', 'Napolact')

insert into Products(name, producer, type)
    values ('Lapte 1L', 'Napolact', 0)

insert into Purchases(datePurchased, price, product, productProducer, purchaser, quantity, shop, shopAddress)
    values (CURRENT_TIMESTAMP, 10, 'Lapte 1L', 'Napolact', 'Andrei', 1, 'Billa', @addressId)
go

declare @addressId int = (select (ISNULL(max(id), 0) + 1) from Addresses)

insert into Persons(name, preferedCurrency)
    values ('Ion', 'Euro')

insert into Incomes(dateReceived, person, sum)
    values (CURRENT_TIMESTAMP, 'Ion', 1000)

insert into Addresses(city, country, county, id, street)
    values ('Cluj-Napoca', 'Romania', 'Cluj', @addressId, 'Calea Bciului 514')

insert into Shops(address, name, type)
    values (@addressId, 'Cora', 0)

insert into Purchases(datePurchased, price, product, productProducer, purchaser, quantity, shop, shopAddress)
    values (CURRENT_TIMESTAMP, 10, 'Lapte 1L', 'Napolact', 'Ion', 1, 'Cora', @addressId)
go

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
    preferedCurrency varchar(10),
    constraint pkPersons primary key (name)
)
go

create table Incomes(
    sum int,
    dateReceived datetime,
    person varchar(100),
    constraint fkIncomesToPerson foreign key (person) references Persons(name)
)
go

create table Addresses(
    id int identity,
    street varchar(100),
    city varchar(100),
    county varchar(100),
    country varchar(100),
    constraint pkAddresses primary key (id),
    constraint uqAddresses unique (street, city, county, country)
)
go

create table Shops(
    name varchar(100),
    type int,
    address int,
    constraint pkShops primary key (name, address),
    constraint fkShopsToAddress foreign key (address) references Addresses(id)
)
go

create table Producers(
    name varchar(100),
    country varchar(100),
    constraint pkProducer primary key (name)
)
go

create table Products(
    name varchar(100),
    type int,
    shop varchar(100),
    producer varchar(100),
    constraint pkProducts primary key (name),
    constraint fkProductsToProducer foreign key (name) references Producers(name)
)
go

create table Purchases(
    price int,
    quantity int,
    shop varchar(100),
    shopAddress int,
    purchaser varchar(100),
    product varchar(100)
    constraint fkPurchasesToProduct foreign key (product) references Products(name),
    constraint fkPurchasesToPerson foreign key (purchaser) references Persons(name),
    constraint fkPurchasesToShop foreign key (shop, shopAddress) references Shops(name, address)
)
go

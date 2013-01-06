create table Categorii(
    codC int,
	numeC varchar(30),
	constraint pkCategorii primary key (codC)
)

create table Preparate(
    codP int,
	codC int,
	numeP varchar(100),
	pret int,
	timp_preparare int,
	constraint pkPreparate primary key (codP),
	constraint fkPreparateCategorii foreign key (codC) references Categorii(codC)
)

create table Ingrediente(
    codI int,
    numeI varchar(30),
    unitate_masura varchar(30),
    constraint pkIngrediente primary key (codI),
)

create table Retete(
    codP int,
    codI int,
    cantitate int,
    constraint pkRetete primary key (codP, codI),
    constraint fkRetetePreparate foreign key (codP) references Preparate(codP),
    constraint fkReteteIngrediente foreign key (codI) references Ingrediente(codI)
)

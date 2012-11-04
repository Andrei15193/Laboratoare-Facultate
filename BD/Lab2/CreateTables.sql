create table ClientiInregistrati(
    codCard int,
    nume varchar(40),
    constraint pkClientiInregistrati primary key (codCard)
)

create table Carusele(
    denumire varchar(20),
    constraint pkCarusele primary key (denumire)
)

create table Parcuri(
    denumire varchar(30),
    locatie varchar(30),
    constraint pkParcuri primary key (denumire, locatie)
)

create table Bilete(
    denumireParc varchar(30),
    locatieParc varchar(30),
    denumireCarusel varchar(20),
    pretBilet int,
    constraint pkBilete primary key (denumireParc, locatieParc, denumireCarusel),
    constraint fkBileteParcuri foreign key (denumireParc, locatieParc) references Parcuri(denumire, locatie),
    constraint fkBileteCarusele foreign key (denumireCarusel) references Carusele(denumire)
)

create table BileteVandute(
    codVanzare int,
    denumireParc varchar(30),
    locatieParc varchar(30),
    denumireCarusel varchar(20),
    oraVanzarii time,
    dataVanzarii date,
    constraint pkBileteVandute primary key (codVanzare),
    constraint fkBileteVanduteBilete foreign key (denumireParc, locatieParc, denumireCarusel) references Bilete(denumireParc, locatieParc, denumireCarusel)
)

create table BileteCumparate(
    codBiletVandut int,
    codCard int,
    constraint pkBileteCumparate primary key (codBiletVandut),
    constraint fkBileteCumparateBileteVandute foreign key (codBiletVandut) references BileteVandute(codVanzare),
    constraint fkBileteCumparateClientiInregistrati foreign key (codCard) references ClientiInregistrati(codCard)
)

create table Discounturi(
    denumireParc varchar(30),
    locatieParc varchar(30),
    denumireCarusel varchar (20),
    codCardClient int,
    valoare int
    constraint pkDiscounturi primary key (denumireParc, locatieParc, denumireCarusel, codCardClient),
    constraint fkDiscounturiBilete foreign key (denumireParc, locatieParc, denumireCarusel) references Bilete(denumireParc, locatieParc, denumireCarusel),
    constraint fkDiscounturiClientiInregistrati foreign key (codCardClient) references ClientiInregistrati(codCard)
)

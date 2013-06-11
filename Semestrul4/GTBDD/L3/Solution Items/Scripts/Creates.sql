if exists(select name from sys.objects where type = 'U' and name = 'Rezervari')
    drop table Rezervari
create table Rezervari(
    cursa varchar(200),
    nr_locuri int,
    nr_locuri_libere int,
    constraint pkRezervari primary key (cursa)
)

insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Budapesta', 100, 50)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Arad', 100, 73)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Aiud', 100, 44)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Bucuresti', 100, 42)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Iasi', 100, 22)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Turda', 100, 81)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Dej', 100, 55)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Gherla', 100, 93)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Gilau', 100, 81)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Oradea', 100, 49)
insert into Rezervari(cursa, nr_locuri, nr_locuri_libere)
    values ('Cluj - Constanta', 100, 57)

select Rezervari.* from Rezervari

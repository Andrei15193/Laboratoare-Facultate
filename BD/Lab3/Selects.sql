--1. Disciplinele care sunt predate la “Informatica” si disciplinele care sunt predate la “Matematica-
--   Informatica”. A se elimina duplicatele. Cautarea sectiei se va face dupa numele ei, nu dupa codul care
--   stiti ca-l are.
select distinct denumired
    from discipline inner join(
        select codd
            from planinv inner join(
                select cods
                    from sectii
                    where denumires in ('Informatica', 'Matematica-Informatica')
            ) as rez1
            on planinv.cods = rez1.cods
    ) as rez2
    on discipline.codd = rez2.codd

--2. Disciplinele care sunt predate la (exact) doua sectii diferite (oarecare).
select denumired
    from discipline
    where 2 =(
        select COUNT(*)
            from(
                select cods
                from planinv
                where discipline.codd = planinv.codd
            ) as rez1
        )

--3. Studentii (numar matricol, nume, denumire disciplina) care au nota la disciplinele a caror denumire
--   contine cuvantul “de” (sau alt cuvant la alegerea fiecaruia), si aceste note.
select studenti.nrmatricol, nume, rez2.denumired, rez2.nota
    from studenti inner join(
        select denumired, nota, nrmatricol
            from rezultate inner join(
                select codd, denumired
                    from discipline
                    where denumired like 'date %' or  denumired like '% date %' or denumired like '% date'
                ) as rez1
                on rezultate.disciplina = rez1.codd
        ) as rez2
        on studenti.nrmatricol = rez2.nrmatricol

--4. Disciplinele (cod+denumire) la care nu este nici o nota.
select codd, denumired
    from discipline left join rezultate
        on codd = disciplina
    where nota is null
    
--5. Studentii si mediile acestora (media se va calcula numai daca acel student are toate notele >=5!!!).
select studenti.*, AVG(nota) as media
    from studenti inner join rezultate
        on studenti.nrmatricol = rezultate.nrmatricol
    where 5 < all(
        select nota
            from Rezultate
            where rezultate.nrmatricol = studenti.nrmatricol
    )
    group by studenti.nrmatricol, nume, cods, grupa, datan, nota

--6. Studentii care au (cel putin o) nota la “Sisteme de operare (1)” sau la “Baze de date (1)”.
--   Identificarea disciplinei se va face dupa numele acesteia, nu dupa codul ei.
select *
    from studenti
    where nrmatricol in(
        select distinct nrmatricol
            from rezultate inner join(
                select codd
                    from discipline
                    where denumired in ('Sisteme de operare (1)', 'Baze de date (1)')
            ) as rez1
            on disciplina = codd
    )

--7. Studentii care au cel putin o nota la “Sisteme de operare (1)” si cel putin o nota la “Baze de date (1)”.
--   Identificarea disciplinei se va face dupa numele acesteia, nu dupa codul ei.
select *
    from studenti
    where 2 =(
        select COUNT(*)
            from(
                select distinct nrmatricol, disciplina
                    from rezultate inner join(
                        select codd
                            from discipline
                            where denumired in ('Sisteme de operare (1)', 'Baze de date (1)')
                    ) as rez1
                    on codd = disciplina
            ) as rez2
            where rez2.nrmatricol = studenti.nrmatricol
    )

--8. Studentii care au cel putin o nota la “Sisteme de operare (1)” si nu au nota la “Baze de date (1)”.
--   Identificarea disciplinei se va face dupa numele acesteia, nu dupa codul ei.
select *
    from studenti
    where nrmatricol not in(
        select nrmatricol
            from rezultate inner join(
                select codd
                    from discipline
                    where denumired = 'Baze de date (1)'
            ) as rez1
            on disciplina = codd
        )
        and
        nrmatricol in(
            select nrmatricol
                from rezultate inner join(
                    select codd
                        from discipline
                        where denumired = 'Sisteme de operare (1)'
                ) as rez2
                on disciplina = codd
        )

--9. Sa se afiseze notele care sunt date incorect: sunt date la o disciplina care nu este in planul de
--   invatamant al sectiei in care este inscris studentul care a primit acea nota.
select nota
    from rezultate
    where(
        select cods
            from studenti
            where studenti.nrmatricol = rezultate.nrmatricol
    )
    not in(
        select cods
            from planinv
            where codd = rezultate.disciplina
    )
    

--10. Studentii care au note la toate disciplinele din planul de invatamant al sectiei din care fac parte.
select *
    from studenti
    where 0 =(
        select COUNT(*)
            from rezultate right join(
                select codd
                    from planinv
                    where planinv.cods = studenti.cods
            ) as rez1
            on rezultate.disciplina = rez1.codd
            where disciplina is null
    )
    

--11. Pentru fiecare sectie, numele studentului cu media cea mai mare, si media (aceeasi conditie asupra
--    mediei ca la punctul 5).
select sectii.denumires, medii.nume, medii.media
    from sectii inner join(
        select studenti.*, AVG(nota) as media
            from studenti inner join rezultate
                on studenti.nrmatricol = rezultate.nrmatricol
            where 5 < all(
                select nota
                    from Rezultate
                    where rezultate.nrmatricol = studenti.nrmatricol
            )
            group by studenti.nrmatricol, nume, cods, grupa, datan, nota
    ) as medii
    on sectii.cods = medii.cods
    where medii.media >= all(
        select AVG(nota)
            from studenti inner join rezultate
                on studenti.nrmatricol = rezultate.nrmatricol
            where 5 < all(
                select nota
                    from Rezultate
                    where rezultate.nrmatricol = studenti.nrmatricol
            )
            group by studenti.nrmatricol, nume, cods, grupa, datan, nota
            having cods = medii.cods
    )

--12. Pentru fiecare disciplina, sa se afiseze notele (*) si numele studentilor care au primit acele note
--    ((*) – notele care sunt mai mari decat media notelor la disciplina respectiva).
select discipline.denumired, rez1.nota, studenti.nume
    from discipline inner join(
        select r1.disciplina, r1.nrmatricol, r1.nota
            from rezultate as r1
            where
                r1.nota >(
                    select AVG(nota)
                    from rezultate as r2
                        where r1.disciplina = r2.disciplina
                )
    ) as rez1
    on discipline.codd = rez1.disciplina
    inner join studenti
    on rez1.nrmatricol = studenti.nrmatricol

--13. Studentii care au nota cea mai mica la o disciplina si nota cea mai mare la o alta disciplina, notele
--    respective si denumirea disciplinelor. Structura rezultatului va fi : Nume_student, Nota1, Disciplina1,
--    Nota2, Disciplina2. A se elimina duplicatele. (Nu afisati “Ionescu, 4, BD, 10, SO” si “Ionescu, 10,
--    SO, 4, BD” – e aceeasi informatie. Afisati doar una din aceste inregistrari.)
select rez1.nume, rez1.denumired, rez1.nota, rez2.denumired, rez2.nota
    from (
        select s1.nume, s1.nrmatricol, d1.denumired, r1.nota, r1.disciplina
            from studenti as s1 inner join rezultate as r1
                on s1.nrmatricol = r1.nrmatricol
                inner join discipline as d1
                on r1.disciplina = d1.codd 
            where nota <= all(
                select nota
                    from rezultate as r3
                    where r3.disciplina = r1.disciplina
            )
    ) as rez1 inner join(
        select s2.nume, s2.nrmatricol, d2.denumired, r2.nota, r2.disciplina
            from studenti as s2 inner join rezultate as r2
                on s2.nrmatricol = r2.nrmatricol
                inner join discipline as d2
                on r2.disciplina = d2.codd 
            where nota >= all(
                select nota
                    from rezultate as r4
                    where r4.disciplina = r2.disciplina
            )
    ) as rez2
    on
        rez1.nrmatricol = rez2.nrmatricol
        and
        rez1.disciplina <> rez2.disciplina

--14. Primii 5 studenti din fiecare grupa, in ordinea descrescatoare a mediilor.
select s1.*, rez1.media
    from studenti as s1 inner join(
        select nrmatricol, avg(nota) as media
            from rezultate
            group by nrmatricol
    )as rez1
    on s1.nrmatricol = rez1.nrmatricol
    where rez1.media in(
        select top 5 with ties avg(nota) as medie
            from rezultate inner join studenti as s2
            on rezultate.nrmatricol = s2.nrmatricol
            where s2.grupa = s1.grupa
            group by rezultate.nrmatricol
            order by medie desc
        )

--15. Studentul de pe pozitia 5 in ordinea mediilor, din fiecare sectie.
select denumires, nume, media
    from sectii inner join(
        select s1.nrmatricol, s1.cods, s1.nume, AVG(nota) as media
            from studenti as s1 inner join rezultate as r1
            on s1.nrmatricol = r1.nrmatricol
            group by s1.nrmatricol, s1.cods, s1.nume
    ) as rez1
    on rez1.cods = sectii.cods
    where
        media = (
            select top 1 media
                from(
                    select top 5 AVG(nota) as media
                        from studenti as s2 inner join rezultate as r2
                        on s2.nrmatricol = r2.nrmatricol
                        where s2.cods = rez1.cods
                        group by s2.nrmatricol, s2.cods, s2.nume
                        order by media desc
                ) as rez2
                order by media asc
        )

--16. Sectiile la care se predau cele mai multe discipline si numarul acestora (al disciplinelor predate).
select top 1 with ties sectii.denumires, COUNT(*) as numarDiscipline
    from sectii inner join planinv
        on sectii.cods = planinv.cods
    group by sectii.cods, sectii.denumires
    order by numarDiscipline desc

--17. Pentru fiecare student – numarul total de note, numarul de note >=5, numarul de note <5 (optional, vezi
--    CASE – SQL-Server documentation).
select *,
    noteTotale =
        case when 1 = 1 then
            (select COUNT(*)
                from rezultate as r1
                where r1.nrmatricol = studenti.nrmatricol)
        end,
    noteDeTrecere =
        case when 1 = 1 then
            (select COUNT(*)
                from rezultate as r2
                where
                    r2.nrmatricol = studenti.nrmatricol
                    and
                    r2.nota >= 5)
        end,
    noteSubCinci =
        case when 1 = 1 then
            (select COUNT(*)
                from rezultate as r3
                where
                    r3.nrmatricol = studenti.nrmatricol
                    and
                    r3.nota < 5)
        end
    from studenti

--18. Stiind ca din fiecare sectie 40% din studenti primesc bursa, sa se afiseze studentii care
--    primesc bursa din fiecare sectie (un student primeste bursa daca toate notele sale sunt de trecere).
select s1.nume, denumires
    from studenti as s1 inner join sectii
    on sectii.cods = s1.cods
    where s1.nrmatricol in(
        select rez1.nrmatricol
            from(
                select top 40 percent s3.nrmatricol, AVG(nota) as media
                    from rezultate inner join studenti as s3
                    on s3.nrmatricol = rezultate.nrmatricol
                    where
                        s3.nrmatricol = s1.nrmatricol
                        and
                        5 < all(
                            select nota 
                                from rezultate
                                where rezultate.nrmatricol = s1.nrmatricol
                        )
                    group by s3.nrmatricol
                    order by media desc
            ) as rez1
    )

--19. Studentii care nu au cea mai mica nota din grupa din care fac parte.
select s1.*, media
    from studenti as s1 inner join(
        select nrmatricol, AVG(nota) as media
            from rezultate
            group by nrmatricol
    ) as rez1
    on s1.nrmatricol = rez1.nrmatricol
    where media <= all(
        select AVG(nota)
            from rezultate inner join studenti as s2
            on rezultate.nrmatricol = s2.nrmatricol
            where s2.grupa = s1.grupa
            group by s2.nrmatricol
    )

--20. Pentru fiecare sectie se afiseaza numarul de studenti. Pentru sectiile in care nu este nici un student
--    se afiseaza 0.
select denumires, numarStudenti = case when studenti.cods is null then 0 else COUNT(*) end
    from sectii left join studenti
    on sectii.cods = studenti.cods
    group by denumires, studenti.cods


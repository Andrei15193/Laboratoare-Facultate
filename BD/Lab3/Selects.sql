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
    where 0 <>(
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

--12. Pentru fiecare disciplina, sa se afiseze notele (*) si numele studentilor care au primit acele note
--    ((*) – notele care sunt mai mari decat media notelor la disciplina respectiva).

--13. Studentii care au nota cea mai mica la o disciplina si nota cea mai mare la o alta disciplina, notele
--    respective si denumirea disciplinelor. Structura rezultatului va fi : Nume_student, Nota1, Disciplina1,
--    Nota2, Disciplina2. A se elimina duplicatele. (Nu afisati “Ionescu, 4, BD, 10, SO” si “Ionescu, 10,
--    SO, 4, BD” – e aceeasi informatie. Afisati doar una din aceste inregistrari.)

--14. Primii 5 studenti din fiecare grupa, in ordinea descrescatoare a mediilor.

--15. Studentul de pe pozitia 5 in ordinea mediilor, din fiecare sectie.

--16. Sectiile la care se predau cele mai multe discipline si numarul acestora (al disciplinelor predate).

--17. Pentru fiecare student – numarul total de note, numarul de note >=5, numarul de note <5 (optional, vezi
--    CASE – SQL-Server documentation).

--18. Stiind ca din fiecare sectie 40% din studenti primesc bursa, sa se afiseze studentii care
--    primesc bursa din fiecare sectie (un student primeste bursa daca toate notele sale sunt de trecere).

--19. Studentii care nu au cea mai mica nota din grupa din care fac parte.

--20. Pentru fiecare sectie se afiseaza numarul de studenti. Pentru sectiile in care nu este nici un student
--    se afiseaza 0.

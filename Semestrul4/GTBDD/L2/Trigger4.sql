if exists(select name from sys.objects where type = 'TR' and name = 'Trigger4')
    drop trigger Trigger4
go
create trigger Trigger4 on DetaliiProduse
after insert, update, delete as
begin
    if exists(select codP from inserted)
        if  exists(select codP from deleted)
            and (select COUNT(*)
                    from inserted inner join deleted
                        on inserted.codP = deleted.codP
                        and inserted.caracteristica = deleted.caracteristica) < (select COUNT(*) from deleted)
        begin
            rollback
            raiserror('Nu se pot modifica valorile codP sau caracteristica!', 18, 1)
        end
        else
            if (select COUNT(*) from DetaliiProduse) > (select COUNT(*) from (select distinct codP, caracteristica from DetaliiProduse) as detaliiUnice)
            begin
                rollback
                raiserror('Nu se poate ca un produs sa aiba aceasi caracteristica inregistrata de mai multe ori!', 18, 1)
            end
    update DetaliiProduse
        set DetaliiProduse.numar = DetaliiProduseNormalizate.numar
        from(select DetaliiProduse.codP, DetaliiProduse.caracteristica, DetaliiProduse.valoare, ROW_NUMBER() over (partition by DetaliiProduse.codP order by DetaliiProduse.codP) as numar
                    from DetaliiProduse inner join  (
                                                        select distinct DetaliiModificate.codP
                                                            from(
                                                                    select distinct inserted.codP from inserted
                                                                    union
                                                                    select distinct deleted.codP from deleted
                                                                ) as DetaliiModificate
                                                    ) as DetaliiProduseModificate
                    on
                        DetaliiProduse.codP = DetaliiProduseModificate.codP
            ) as DetaliiProduseNormalizate
        where
                DetaliiProduse.codP = DetaliiProduseNormalizate.codP
                and DetaliiProduse.caracteristica = DetaliiProduseNormalizate.caracteristica
end
go

insert into Produse(codP, denumire, um)
    values (2, 'Margarina', 'kg')
insert into Produse(codP, denumire, um)
    values (3, 'Ciocolata', 'g')

insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values (1, 1, 'Culoare', 'De unt')
insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values (2, 1, 'Culoare', 'Albui')
insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values (3, 1, 'Culoare', 'Neagra')
insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values(1, 20, 'Stare de agregare', 'Solida')
insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values(2, 30, 'Stare de agregare', 'Solida')
insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values(3, 1, 'Stare de agregare', 'Solida')
    
select DetaliiProduse.* from DetaliiProduse order by codP, caracteristica
insert into DetaliiProduse(codP, numar, caracteristica, valoare)
    values (1, 1, 'Culoare', 'De unt')
select DetaliiProduse.* from DetaliiProduse order by codP, caracteristica

update DetaliiProduse set valoare = 'Albui' where codP = 1 and caracteristica = 'Culoare'
select DetaliiProduse.* from DetaliiProduse order by codP, caracteristica
delete from DetaliiProduse where codP = 1 and caracteristica = 'Culoare'
select DetaliiProduse.* from DetaliiProduse order by codP, caracteristica

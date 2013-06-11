if exists(select name from sys.objects where type = 'TR' and name = 'Trigger2')
    drop trigger Trigger2
go
create trigger Trigger2 on OferteSpeciale
instead of insert, update as
begin
    if  (
            exists  (
                        select OferteSpeciale.codM
                            from OferteSpeciale inner join inserted
                                on OferteSpeciale.panala >= inserted.dela and inserted.panala >= OferteSpeciale.dela and OferteSpeciale.dela <> inserted.dela
                    )
        )
        raiserror('Intervalele ofertelor nu se pot suprapune!', 18, 1)
    else
        if exists(select codP from deleted)
            if  (   select COUNT(*)
                        from deleted inner join inserted
                            on  deleted.codP = inserted.codP
                                and deleted.codM = inserted.codM
                                and deleted.dela = inserted.dela
                ) = (select COUNT(*) from deleted)
                update OferteSpeciale set OferteSpeciale.panala = inserted.panala, OferteSpeciale.pret = inserted.pret
                   from inserted
                   where
                            OferteSpeciale.codM = inserted.codM
                            and OferteSpeciale.codP = inserted.codP
                            and OferteSpeciale.dela = inserted.dela
            else
                raiserror('Nu se pot modifica valorile cheilor primare!', 18, 1)
        else
            insert into OferteSpeciale(codP, codM, dela, panala, pret)
                select codP, codM, dela, panala, pret from inserted
end
go

insert into OferteSpeciale(codP, codM, dela, panala, pret)
    values (1, 1, '1/15/2013', '1/20/2013', 10)
update OferteSpeciale set panala = '1/16/2013' where codM = 1 and codP = 1 and dela = '1/15/2013'
insert into OferteSpeciale(codP, codM, dela, panala, pret)
    values (1, 1, '1/17/2013', '1/19/2013', 5)
update OferteSpeciale set panala = '1/18/2013' where codM = 1 and codP = 1 and dela = '1/15/2013'

select OferteSpeciale.* from OferteSpeciale

if exists(select name from sys.objects where type = 'TR' and name = 'Trigger3')
    drop trigger Trigger3
go
create trigger Trigger3 on OferteSpeciale
after delete as
begin
    if not exists   (
                        select OferteSpeciale.codP
                            from OferteSpeciale inner join deleted
                            on  OferteSpeciale.codM = deleted.codM
                                and OferteSpeciale.codP = deleted.codP
                    )
        insert into OferteSpeciale(codP, codM, dela, panala, pret)
            select top 1 codP, codM, dela, panala, pret
                from deleted order by dela desc
end

delete from OferteSpeciale where codP = 1 and codM = 1

select OferteSpeciale.* from OferteSpeciale

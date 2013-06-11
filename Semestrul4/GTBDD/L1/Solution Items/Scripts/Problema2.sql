if exists(select name from sys.objects where type = 'P' and name = 'Problema2')
    drop procedure Problema2
go
create procedure Problema2 @codP int, @codM int, @dela date, @panala date, @pret int, @out bit output as
begin
    if exists(select OferteSpeciale.*
                  from OferteSpeciale
                  where codP = @codP and codM = @codM
                        and DATEDIFF(day, dela, @panala) >= 0
                        and DATEDIFF(day, @dela, panala) >= 0)
        raiserror('Oferta existenta pentru intervalul de zile specificat', 18, 1)
    else
    begin
        declare @pretMinimDinCatalog int = (select min(pret)
                                                from OferteSpeciale
                                                where codP = @codP and codM = @codM)
        declare @pretMinimDinOferteSpeciale int = (select min(pret) as pret
                                                       from Cataloage
                                                       where codP = @codP and codM = @codM)
        if ((@pretMinimDinCatalog is null and @pretMinimDinOferteSpeciale is null)
            or
            (@pretMinimDinCatalog is not null and @pretMinimDinCatalog > @pret)
            or
            (@pretMinimDinOferteSpeciale is not null and @pretMinimDinOferteSpeciale > @pret)
           )
            set @out = 1
        else
            set @out = 0
        insert into OferteSpeciale(codP, codM, dela, panala, pret)
            values(@codP, @codM, @dela, @panala, @pret)
    end
end
go

declare @rezultat bit
execute Problema2 1, 1, '6/1/2013', '6/18/2013', 2, @rezultat output
print @rezultat

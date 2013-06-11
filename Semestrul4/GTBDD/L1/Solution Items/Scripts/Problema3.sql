if exists(select name from sys.objects where type = 'P' and name = 'Problema3')
    drop procedure Problema3
go
create procedure Problema3 @d date as
begin
    declare @da date = @d
    select Magazine.denumire as denumire_magazin, Produse.denumire as denumire_produs, OfertaZilei.pret, OfertaZilei.dela as valabilitate_dela, OfertaZilei.panala as valabilitate_panala, case when pret = ProduseDeVanzare.pretMinim then '<<da>>' else '<<nu>>' end as cea_mai_buna_oferta
        from
                (
                select
                        ISNULL(CataloageDeAzi.codM, OferteSpecialeDeAzi.codM) as codM,
                        ISNULL(CataloageDeAzi.codP, OferteSpecialeDeAzi.codP) as codP,
                        ISNULL(OferteSpecialeDeAzi.pret, CataloageDeAzi.pret) as pret,
                        ISNULL(OferteSpecialeDeAzi.dela, CONVERT(date, CONVERT(varchar, CataloageDeAzi.luna) + '/1/' + CONVERT(varchar, CataloageDeAzi.an))) as dela,
                        ISNULL(OferteSpecialeDeAzi.panala, DATEADD(DAY, - 1, CONVERT(date, CONVERT(varchar, CataloageDeAzi.luna + 1) + '/1/' + CONVERT(varchar, CataloageDeAzi.an)))) as panala
                    from
                            (
                                select codM, codP, pret, dela, panala
                                    from OferteSpeciale
                                    where DATEDIFF(day, dela, @da) >= 0 and DATEDIFF(day, panala, @da) <= 0
                            ) as OferteSpecialeDeAzi
                    full outer join
                            (
                                select codM, codP, pret, luna, an
                                    from Cataloage
                                    where MONTH(@da) = luna and YEAR(@da) = an
                            ) as CataloageDeAzi
                        on
                                OferteSpecialeDeAzi.codM = CataloageDeAzi.codM
                                and OferteSpecialeDeAzi.codP = CataloageDeAzi.codP
                ) as OfertaZilei
        inner join
                (
                    select Cataloage.codP as codP, min(case when (OferteSpeciale.pret is null or Cataloage.pret < OferteSpeciale.pret) then Cataloage.pret else OferteSpeciale.pret end) as pretMinim
                        from Cataloage full outer join OferteSpeciale
                            on Cataloage.codP = OferteSpeciale.codP
                        group by Cataloage.codP
                ) as ProduseDeVanzare
            on OfertaZilei.codP = ProduseDeVanzare.codP
        inner join Magazine
            on OfertaZilei.codM = Magazine.codM
        inner join Produse
            on OfertaZilei.codP = Produse.codP
end
go

execute Problema3 '6/10/2013'

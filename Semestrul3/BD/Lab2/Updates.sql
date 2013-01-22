update ClientiInregistrati set nume = 'Florica' where nume like '%a'
select * from ClientiInregistrati

update Bilete set pretBilet = 5 where pretBilet between 10 and 20 or pretBilet in (1, 2, 3, 4, 5)
select * from Bilete

update Bilete set pretBilet = 10 where pretBilet < 10
select * from Bilete

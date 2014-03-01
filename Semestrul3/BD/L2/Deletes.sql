update ClientiInregistrati set nume = null where nume like 'A%'
select * from ClientiInregistrati

select * from Discounturi
delete from Discounturi where codCardClient = 1
select * from Discounturi

select * from BileteCumparate
delete from BileteCumparate where codCard = 1
select * from BileteCumparate

select * from BileteVandute
delete from BileteVandute where codVanzare not in (select codBiletVandut from BileteCumparate)
select * from BileteVandute

select * from ClientiInregistrati
delete from ClientiInregistrati where nume is null
select * from ClientiInregistrati

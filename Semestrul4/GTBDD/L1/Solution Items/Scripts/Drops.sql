if exists(select name from sys.objects where type = 'U' and name = 'OferteProduse')
    drop table OferteProduse
if exists(select name from sys.objects where type = 'U' and name = 'Calendar')
    drop table Calendar
if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
    drop table OferteSpeciale
if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
    drop table Cataloage
if exists(select name from sys.objects where type = 'U' and name = 'Produse')
    drop table Produse
if exists(select name from sys.objects where type = 'U' and name = 'Magazine')
    drop table Magazine

if exists(select name from sys.objects where type = 'P' and name = 'CreazaMagazine')
    drop procedure CreazaMagazine
if exists(select name from sys.objects where type = 'P' and name = 'CreazaProduse')
    drop procedure CreazaProduse
if exists(select name from sys.objects where type = 'P' and name = 'CreazaCataloage')
    drop procedure CreazaCataloage
if exists(select name from sys.objects where type = 'P' and name = 'CreazaOferteSpeciale')
    drop procedure CreazaOferteSpeciale
if exists(select name from sys.objects where type = 'P' and name = 'CreazaCalendar')
    drop procedure CreazaCalendar
if exists(select name from sys.objects where type = 'P' and name = 'CreazaOferteProduse')
    drop procedure CreazaOferteProduse
if exists(select name from sys.objects where type = 'P' and name = 'Problema1')
    drop procedure Problema1
if exists(select name from sys.objects where type = 'P' and name = 'Problema2')
    drop procedure Problema2
if exists(select name from sys.objects where type = 'P' and name = 'Problema3')
    drop procedure Problema3
if exists(select name from sys.objects where type = 'P' and name = 'Problema4')
    drop procedure Problema4

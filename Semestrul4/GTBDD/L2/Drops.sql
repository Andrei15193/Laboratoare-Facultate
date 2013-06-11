if exists(select name from sys.objects where type = 'TR' and name = 'Trigger1')
    drop trigger Trigger1
if exists(select name from sys.objects where type = 'TR' and name = 'Trigger2')
    drop trigger Trigger2
if exists(select name from sys.objects where type = 'TR' and name = 'Trigger3')
    drop trigger Trigger3
if exists(select name from sys.objects where type = 'TR' and name = 'Trigger4')
    drop trigger Trigger4

if exists(select name from sys.objects where type = 'U' and name = 'OferteSpeciale')
    drop table OferteSpeciale
if exists(select name from sys.objects where type = 'U' and name = 'Cataloage')
    drop table Cataloage
if exists(select name from sys.objects where type = 'U' and name = 'DetaliiProduse')
    drop table DetaliiProduse
if exists(select name from sys.objects where type = 'U' and name = 'Produse')
    drop table Produse
if exists(select name from sys.objects where type = 'U' and name = 'Magazine')
    drop table Magazine

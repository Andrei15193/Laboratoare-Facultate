if exists(select name from sys.objects where type = 'U' and name = 'Rezervari')
    drop table Rezervari

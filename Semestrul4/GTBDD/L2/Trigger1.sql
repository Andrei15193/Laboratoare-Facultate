if exists(select name from sys.objects where type = 'TR' and name = 'Trigger1')
    drop trigger Trigger1
go
create trigger Trigger1 on Produse
instead of insert as
begin
    if  (
            (select count(*) from inserted) > 1
            or exists(select codP from inserted where denumire = '' or um = '')
        )
        raiserror('Nu se permite adaugarea a mai multor produse "dintr-un shut" sau produse care nu au descriere sau unitate de masura!', 18, 1)
    else
        insert into Produse(codP, denumire, um) select codP, denumire, um from inserted
end
go

insert into Produse values (1, '', 'kg')
insert into Produse values (1, 'unt', '')
insert into Produse values (1, 'unt', 'kg'),(2, 'margarina', 'kg')
insert into Produse values (1, 'unt', 'kg')

select Produse.* from Produse

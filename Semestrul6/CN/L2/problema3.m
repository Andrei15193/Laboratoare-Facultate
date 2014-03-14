anterior = 1/exp(1);
curent = 1 - anterior;
n = 2;
while abs(curent - anterior) > eroare
    n = n + 1;
    aux = curent;
    curent = 1 - n* anterior;
    anterior = aux;
end
if abs(1/exp(1) - anterior) <= eroare
    curent
else
    n
    error('Nu covnerge')
end
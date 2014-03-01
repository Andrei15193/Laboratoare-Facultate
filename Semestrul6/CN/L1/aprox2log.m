function t = aprox2log(x, eroare)
    tCurent = x;
    tUrmator = x-x.^2/2;
    k = 3;
    
    while abs(tUrmator - tCurent) > eroare
        tCurent = tUrmator;
        tUrmator = tCurent + (-1)^(k+1)*x.^k/k;
        k = k + 1;
    end
    
    t = tUrmator;
end
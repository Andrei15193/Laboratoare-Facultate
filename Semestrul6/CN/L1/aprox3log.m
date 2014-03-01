function t = aprox3log(x, eroare)
    tCurent = 2 * x;
    tUrmator = tCurent + 2 * x.^3/3;
    k = 5;
    
    while abs(tUrmator - tCurent) > eroare
        tCurent = tUrmator;
        tUrmator = tCurent + 2 * x.^k/k;
        k = k + 2;
    end
    
    t = tUrmator;
end
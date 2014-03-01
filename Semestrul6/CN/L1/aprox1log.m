function t = aprox1log(x, numarTermeni)
    t = 0;
    for k = 1 : numarTermeni
        t = t + (-1)^(k+1)*x.^k/k;
    end
end
function [tsin, tcos k] = sinCosAprox(x, eroare)
    x = rem(x, 2*pi);
    termeni = ones(1, length(x));
    tcos = termeni;
    termeni = termeni .* x;
    tsin = termeni;
    k = 2;
    
    while max(abs(termeni)) > eroare
        termeni = -termeni .* x / k;
        tcos = tcos + termeni;
        k = k + 1;
        termen = termeni .* x/k;
        tsin = tsin + termen;
        k = k + 1;
    end
end
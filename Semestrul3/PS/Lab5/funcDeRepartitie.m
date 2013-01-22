function rez = funcDeRepartitie(X, P, x)
    i = 1;
    size = length(X);
    sums = cumsum(P);
    while i <= size && X(i) <= x
        i = i + 1;
    end
    rez = sums(i);

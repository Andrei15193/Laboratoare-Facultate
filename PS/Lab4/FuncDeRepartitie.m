function rez = FuncDeRepartitie(X, x)
    s = 0;
    i = 1;
    size = length(X);
    while i <= size && X(1, i) <= x
        s = s + X(2, i);
        i = i + 1;
    end
    rez = s;

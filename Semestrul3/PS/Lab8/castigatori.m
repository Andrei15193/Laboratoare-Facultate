function rez = castigatori(n, N, M)
    categ = zeros(1, 4);
    extras = extrage(n, N);
    afiseazaExtrase(extras);
    for i = 1: M
        switch sum(ismember(extras, extrage(n, N)))
            case 3
                categ(4) = categ(4) + 1;
            case 4
                categ(3) = categ(3) + 1;
            case 5
                categ(2) = categ(2) + 1;
            case 6
                categ(1) = categ(1) + 1;
        end
    end
    P = (categ(1) + categ(2) + categ(3) + categ(4)) / M
    rez = categ;

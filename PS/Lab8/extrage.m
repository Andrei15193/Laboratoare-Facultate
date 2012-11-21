function rez = extrage(n, N)
    variante = 1: N;
    extrase = zeros(1, n);
    for i = 1: n
        norocos = unidrnd(N - i + 1);
        extrase(i) = variante(norocos);
        variante(norocos) = [];
    end
    rez = extrase;

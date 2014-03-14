function eps1 = epsM
    eps1 = 1;
    anterior = eps1;
    while 1 + eps1 ~= 1
        anterior = eps1;
        eps1 = eps1/2;
    end
    eps1 = anterior;
end
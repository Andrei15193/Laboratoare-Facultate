function rez = sumeZar(m)
    sums = zeros(1, m);
    for i = 1: m
        sums(i) = unidrnd(6) + unidrnd(6);
    end
    rez = sums;
    
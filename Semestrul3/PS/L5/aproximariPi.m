function rez = aproximariPi(m, n)
    sums = zeros(1, n);
    punct = struct('x', 0, 'y', 0);
    
    for i = 1: n
        for j = 1: m
            punct.x = unidrnd(500)/100 + 1;
            punct.y = unidrnd(500)/100 + 1;
            if (sqrt(power(punct.x - 3.5, 2) + power(punct.y - 3.5, 2)) <= 2.5)
                sums(i) = sums(i) + 1;
            end
        end
    end
    rez = 4 * sums/m;

clear all
n = 10;
p = 0.3;
m = 100000;
sums = zeros(1, m);
probs = struct('unu', 0, 'doi', 0, 'trei', 0);
a = unidrnd(n + 1) - 1;
b = a;
while (b <= a)
    b = unidrnd(n + 1) - 1;
end
for i = 1: m
    sums(i) = sum(binornd(1, p, 1, n));
    if (a <= sums(i) && sums(i) <= b)
        probs.unu = probs.unu + 1;
        probs.doi = probs.doi + 1;
        probs.trei = probs.trei + 1;
    else
        if (sums(i) < b)
            probs.doi = probs.doi + 1;
        end
        if (a <= sums(i))
            probs.trei = probs.trei + 1;
        end
    end
end
tabulate(sums)
prob1 = probs.unu / m
prob2 = probs.doi / m
prob3 = probs.trei / m

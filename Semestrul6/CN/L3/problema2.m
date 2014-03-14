clc;

for n = 10:15
    h = hilb(n);
    nc = cond(h);
    fprintf('n=%d nc=%g\n', n, nc);
end
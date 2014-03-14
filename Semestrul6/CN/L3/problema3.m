clc;

for n = 10:15
    k = 0:n;
    t = -1 + 2 * k/m;
    v = vander(t);
    nc = cond(v, 1);
    fprintf('n=%d nc=%g\n', n, nc);
end
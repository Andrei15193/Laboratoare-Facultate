n = 1000
x = normrnd(100, 0.65, 1, n);
m = 0;
for i = 1 : n
    if 98 < x(i) && x(i) < 102
        m = m + 1;
    end
end
mean(x)
var(x, 1)/n
m/n
(normcdf(102, 100, 0.65) - normcdf(98, 100, 0.65))
(n - m)/n * 100
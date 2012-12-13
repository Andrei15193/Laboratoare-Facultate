clear all
clf
hold on

n = 100;
%x = unifrnd(100, 2000, 1, n);
x = chi2rnd(1:n);
a = min(x);
b = max(x);
k = floor(1 + 10/3 * log10(n));
Is = a : ((b - a)/k) : b;
s = histc(x, Is);
for i = 1 : k
    h = s(i)/n;
    fill([Is(i) Is(i) Is(i + 1) Is(i + 1)], [0 h h 0], 'r');
end

[M, I] = max(s);
dfj  = ((s(I) - s(I - 1))/n);
%dfj2 = (s(I + 1) - s(I))/n;
%m = Is(I - 1) + (Is(I) - Is(I - 1))* Dfj/(Dfj - Dfj2);


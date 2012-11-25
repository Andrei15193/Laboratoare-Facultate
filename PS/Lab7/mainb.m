clear all

%u = binornd(100, 0.5, 1, n)/100
%p = unidrnd(100, 1, n)/100;
n = 9;
x = [   12, 4,  56, 43, 63, 98, 15, 93, 1] / 100;
p = [0, 11, 8, 10, 15, 9, 12, 7, 15, 13] / 100;
probs = cumsum(p);
u = unifrnd(0, 1, 1, n);
m = 0;
rez = [];
for i = 1: n
    if probs(i) < u(i) && u(i) < probs(i + 1)
        m = m + 1;
        rez(m) = x(i);
    end
end
rez


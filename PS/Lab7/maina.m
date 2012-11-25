clear all
clf
hold on

n = 10000;
l = 2;
u = unifrnd(0, 1, 1, n);
r1 = log(1 - u)/-l;
r2 = exprnd(1/l, 1, n);
subplot(2, 1, 1); hist(r2, n/100);
subplot(2, 1, 2); hist(r2, n/100);

[mean(r1), var(r1, 1);
mean(r2), var(r2, 1)]

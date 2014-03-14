clc;
x = linspace(-5, 5);
y = exp(x);
[t1, t2, t3, t4, t5] = expAproxML(x);

plot(x, y, x, t1, x, t2, x, t3, x, t4, x, t5);
legend('exp', 'T1', 'T2', 'T3', 'T4', 'T5');
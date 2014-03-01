clc;
x = linspace(-pi, pi);
y = sin(x);
[t1, t3, t5] = sinAprox(x);

plot(x, y, x, t1, x, t3, x, t5);
legend('sin', 'T1', 'T3', 'T5');
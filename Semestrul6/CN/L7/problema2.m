clc;
n = 8;

x = linspace(-pi, pi, n);
y = sin(x) + normrnd(0, 0.3, 1, n);
xi = linspace(-pi, pi, 200);
yi = polyval(polyfit(x, y, n - 1), xi);
subplot(1, 2, 1);
plot(x, y, '*', xi, yi, '-');

yi1 = interp2(x, y, xi);
subplot(1, 2, 2);
plot(x, y, '*', xi, yi1, '-');
clc;
x = linspace(-1, 1, 150);
y = 2 * x.^3 - 3 * x.^2 + x - 5;
y = y + normrnd(0, 0.2, 1, 150);

c0 = polyfit(x, y, 3);
y0 = polyval(c0, x);

subplot(1, 2, 1);
plot(x, y, '.', x, y0, '-r');
disp(c0);

% myPolyFitCont
c1 = myPolyFitCont(x, y, 3);
y1 = polyval(c1, x);

subplot(1, 2, 2);
plot(x, y, '.', x, y1, '-r');
disp(c1);
clc;
x = linspace(0, 3 * pi/2, 10);
y = sin(x);


xi = linspace(0, 3 * pi / 2, 100);
yi = splineNat(x, y, xi);

plot(x, y, '*', xi, yi, '-');
[x,y] = ginput;
t = linspace(0, 1, length(x));
ti = linspace(0, 1, 100);
xi = splineNat(t, x, ti);
yi = splineNat(t, y, ti);
plot(x, y, '*', xi, yi, '-');
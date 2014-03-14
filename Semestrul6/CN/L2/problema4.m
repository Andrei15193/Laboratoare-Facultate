clc;

x = linspace(-pi, pi);
m = 7;
k = 4;
[pSin, pCos] = padeSinCos(x, m, k);

legendPSin = sprintf('R_{%d,%d}[sin]', m, k);
legendPCos = sprintf('R_{%d,%d}[cos]', m, k);
plot(x, sin(x), '.', x, cos(x), '.', x, pSin, x, pCos)
legend('sin', 'cos', legendPSin, legendPCos)
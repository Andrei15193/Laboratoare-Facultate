clc;
f = @(x) x .^ 4;
[A, x] = genereazaLegendre(10);
I = aplCuadratura(A, x, f);
disp(I);
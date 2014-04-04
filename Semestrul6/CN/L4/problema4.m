clc;
A = round(normrnd(0, 10, 7, 7));
b = A * ones(7, 1);
x0 = A \ b;
x1 = rezolvaGauss(A, b);
x2 = rezolvaLUP(A, b);
x3 = rezolvaQR(A, b);
x4 = rezolvaCholesky(A, b);

x0
x1
x2
x3
x4
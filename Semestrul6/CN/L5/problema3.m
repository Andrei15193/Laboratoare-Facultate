clc;
dimensiune = 8;
A = diag(ones(dimensiune, 1)) * 5 - diag(ones(dimensiune - 1, 1), 1) - diag(ones(dimensiune - 1, 1), -1);
b = [4; zeros(dimensiune - 2, 1) + 3; 4];
x0 = A\b
x1 = rezolvaGaussSeidel(A, b, 1e-5)
x2 = rezolvaJacobi(A, b, 1e-5)
x3 = rezolvaSOR(A, b, 0.5, 1e-5)
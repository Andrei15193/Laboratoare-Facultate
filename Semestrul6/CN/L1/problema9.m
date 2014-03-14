% Exemplu, pentru f(x) = exp(x) => c(i) = 1/i!, pentru i=1..m+k+1
x = linspace(-1.5, 1.5);

m = 1; k = 1;
c = 1./factorial([0:m+k+1]);
fAprox11= aproxPade(x, c, m, k);

m = 2; k = 2;
c = 1./factorial([0:m+k+1]);
fAprox22= aproxPade(x, c, m, k);

m = 5; k = 2;
c = 1./factorial([0:m+k+1]);
fAprox53= aproxPade(x, c, m, k);

plot(x, exp(x), x, fAprox11, x, fAprox22, x, fAprox53)
legend('exp', 'R_{1,1}', 'R_{2,2}', 'R_{5,3}')
clc;

n = 20;
cp = 2 .^ n - (1:20); % calculeaza coeficientii polinomului cu radacinile 1, 2, ..., 20
                          % (x-1)(x-2) ... (x-20)  
r = roots(cp); % calculeaza radacinile polinomului cu coeficienti cp
               % sau r = (20:-1:1)';

nc = 0;
for kTest = 1:1e4
    cp1 = cp + normrnd(0, 1e-10, 1, length(cp));
    r1 = roots(cp1);
    nc = nc + norm(r1 - r)/norm(cp1 - cp);
end

nc / 1000
function [A, X] = genereazaLegendre(n)
    alpha = zeros(1, n);
    
    k = 1:n-1;
    beta = (4 - k .^(-2)).^(-1);
        
    J = diag(alpha) + diag(sqrt(beta), 1)  + diag(sqrt(beta), -1);
    [V, D] = eig(J);
    
    A = 2 * V(1,:) .^ 2;
    X = diag(D);
end
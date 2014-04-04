function [Q R] = factQR(A)
    % calculeaza factorizarea QR a unei matrici 
    % A = Q*R unde Q*Q' = I si R superior triunghilara
    
    [m n] = size(A); % A nu trebuie sa fie patratica
    R = zeros(n);
    for k = 1:n
        for i = 1:k-1
            R(i, k) = A(:, i)'*A(:, k);
        end
        for i = 1:k-1
            A(:, k) = A(:, k) - A(:, i) * R(i, k);
        end
        R(k, k) = sqrt(A(:,k)'*A(:,k)); % sau sum(A(:,k).^2)
        A(:, k) = A(:, k) / R(k,k);
    end
    Q = A;
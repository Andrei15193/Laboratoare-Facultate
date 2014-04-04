function R = factCholesky(A)
    % calculeaza Factorizarea Cholesky a matricii A = R' * R
    % A - hermitiana si pozitiv definita
    
    [m n] = size(A);
    if m~=n 
        error('Matricea nu este patratica')
    end

    R = A;
    for k = 1:m
        for j = k+1:m
            R(j, j:m) = R(j, j:m) - R(k, j:m)*R(k, j)/R(k,k);
        end
        R(k, k:m) = R(k, k:m)/sqrt(R(k,k));
    end
    R = triu(R);
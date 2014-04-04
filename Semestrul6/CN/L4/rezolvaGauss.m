function x = rezolvaGauss(A, b)
    [n, m] = size(A);
    if n ~= m
        error('Matricea nu e patratica');
    end
    A = [A, b]; %matricea extinsa
    x = zeros(n, 1);
    
    for i = 1:n-1
        p = i;
        while p <= n && A(p, i) == 0
            p = p + 1;
        end

        if p > n
            error('Nu exista solutie unica!');
        end
        if (p ~= i)
            A([p, i],:) = A([i, p], :);
        end
        
        for j = i + 1:n
            mji = A(j, i)/A(i, i);
            A(j, :) = A(j, :) - mji * A(i, :);
        end
    end
    if A(m, n) == 0
        error('Nu exista solutie unica!');
    end
    x(n, 1) = A(n, n + 1)/A(n, n);
    for i = n - 1:-1:1
        suma = 0;
        for j = i + 1:n
            suma = suma + A(i, j) * x(j);
        end
        
        x(i, 1) = (A(i, n + 1) - suma) / A(i, i);
        % x(i, 1) = (A(i, n + 1) - A(i, i + 1:n) * x(i + 1:n)) / A(i, i)
    end
end

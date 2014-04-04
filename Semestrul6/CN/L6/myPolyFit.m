function c = myPolyFit(x, y, n)
    m = length(x);
    if m ~= length(y)
        error('x si y nu au aceasi lungime!');
    end
    
    A = zeros(n + 1);
    b = zeros(n + 1, 1);

    for i = 1:n+1
        for j = 1:n+1
            A(i, j) =  x.^(n - i + 1) * (x.^(n - j + 1))';
        end
        b(i) = x.^(n - i + 1) * y';
    end

    c = (A\b)';
end
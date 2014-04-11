function yi = interp1(x, y, xi)
    n = length(x);
    if n ~= length(y)
        error('x si y nu au aceasi lungime');
    end
    
    A = zeros(n, n);
    for j = 1:n
        A(:, j) = (x') .^ (n - j);
    end
    
    c = A \ y';
    yi = polyval(c, xi);
end
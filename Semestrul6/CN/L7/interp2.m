function yi = interp2(x, y, xi)
    n = length(x);
    if n ~= length(y)
        error('x si y nu au aceasi lungime');
    end

    A = zeros(1, n);
    m = length(xi);
    yi = zeros(1, m);

    for j = 1:n
        A(j) = 1 / prod(x(j) - x([1:j-1, j + 1:n]));
    end

    for k = 1:m
        p = find(x == xi(k));

        if ~isempty(p)
            yi(k) = y(p);
        else
            yi(k) = sum(y .* A ./ (xi(k) - x)) / sum(A ./ (xi(k) - x));
        end
    end
end
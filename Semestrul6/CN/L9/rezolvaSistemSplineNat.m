function a = rezolvaSistemSplineNat(x, y)
    n = length(x);
    A = zeros(3 * n - 3);
    b = zeros(3 * n - 3, 1);
    A(1, 2) = 1;

    for i = 1 : n - 2
        diffX = x(i + 1) - x(i);
        
        A(3 * i - 1, 3 * i - 2) = diffX ^ 3;
        A(3 * i - 1, 3 * i - 1) = diffX ^ 2;
        A(3 * i - 1, 3 * i)     = diffX;
        b(3 * i - 1) = y(i + 1) - y(i);
        
        A(3 * i, 3 * i - 2) = 3 * diffX ^ 2;
        A(3 * i, 3 * i - 1) = 2 * diffX;
        A(3 * i, 3 * i)     = 1;
        A(3 * i, 3 * i + 3) = -1;
        
        A(3 * i + 1, 3 * i - 2) = 6 * diffX;
        A(3 * i + 1, 3 * i - 1) = 2;
        A(3 * i + 1, 3 * i + 2) = -2;
    end

    diffX = x(n) - x(n - 1);
    A(3 * n - 4, 3 * n - 5) = diffX ^ 3;
    A(3 * n - 4, 3 * n - 4) = diffX ^ 2;
    A(3 * n - 4, 3 * n - 3) = diffX;
    b(3 * n - 4) = y(n) - y(n - 1);
    
    A(3 * n - 3, 3 * n - 5) = 6 * diffX;
    A(3 * n - 3, 3 * n - 4) = 2;
    
    a = A\b;
end
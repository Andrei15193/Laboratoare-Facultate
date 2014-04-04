function x = rezolvaCholesky(A, b)
% rezolva sistemul (posibil supradeterminat) A*x = b, prin factorizarea
% Cholesky a matricii A'*A

    b1 = A' * b;  % ecuatia echivalenta (A'*A)*x = A'*b este acum determinata
    R = factCholesky(A' * A); % A*A' este hermitiana (lucram cu matrici reale)
    [m n] = size(R);
    % R este superior triunghiulara => R' triunghiulara inferior,
    % asemanator cu LUP
    y = zeros(n, 1);
    for i=1:n
        y(i) = (b1(i)-R(1:i-1, i)' * y(1:i-1))/R(i,i);
    end
    x = zeros(n, 1);
    x(n) = y(n)/R(n,n);
    for i = n-1:-1:1
        x(i) = (y(i) - R(i, i+1:n) * x(i+1:n))/R(i,i);
    end
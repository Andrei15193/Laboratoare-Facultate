function x = rezolvaQR(A, y)
% rezolva ecuatia A*x = y prin factorizare QR
	[Q R] = factQR(A);

    y1 = Q' * y;  % ecuatia echivalenta R*x = Q'*y pt ca avem Q*Q' = I
    [m n] = size(Q); % in general m ~= n, sistemul poate fi supradeterminat
                     % rezolvarea se face in sensul celor mai mici patrate

    % R este superior triunghiulara => substitutie inversa
    x = zeros(n, 1);
    x(n) = y1(n)/R(n,n);
    for i = n-1:-1:1
        x(i) = (y1(i) - R(i, i+1:n) * x(i+1:n))/R(i,i);
    end
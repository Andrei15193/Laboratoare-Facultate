function rez = myRand(X, P, n)
    randoms = zeros(1, n);
    U = unifrnd(0, 1, 1, n);
    myP = [0 cumsum(P)];
    for i = 1: n
        k = 1;
        while k <= n && ~(myP(k) <= U(i) && U(i) < myP(k + 1)) 
            k = k + 1;
        end
        randoms(i) = X(k);
    end
    rez = randoms;

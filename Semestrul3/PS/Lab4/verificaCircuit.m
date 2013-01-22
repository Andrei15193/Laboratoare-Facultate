function rez = verificaCircuit(A)
    if (length(A) >= 11)
        rez = A(1) && ((((A(3) && A(4) && A(5)) || A(2)) && A(6)) || (A(7) && A(8))) && A(9) && (A(10) || A(11));
    else
        rez = 0;
    end
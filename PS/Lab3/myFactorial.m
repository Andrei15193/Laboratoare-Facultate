function rez = myFactorial(n)
    if (n == 0 || n == 1)
        rez = 1;
    else
        rez = n * myFactorial(n - 1);
    end

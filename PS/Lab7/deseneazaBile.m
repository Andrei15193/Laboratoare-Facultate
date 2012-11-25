function deseneazaBile(B, y, C)
    size = length(B);
    n = 1;
    for i = 1: size
        for j = 1: B(i)
            disc(n, y, 0.4, C(i));
            n = n + 1;
        end
    end

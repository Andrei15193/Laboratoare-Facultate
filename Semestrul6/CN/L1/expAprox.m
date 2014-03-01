function [t1, t2, t3, t4, t5] = expAprox(x)
    % https://www.wolframalpha.com/input/?i=series+sin%28x%29&dataset=
    t1 = 1 + x;
    t2 = t1 + x.^2/2;
    t3 = t2 + x.^3/6;
    t4 = t3 + x.^4/24;
    t5 = t4 + x.^5/120;
end
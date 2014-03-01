function [t1, t3, t5] = sinAprox(x)
    % https://www.wolframalpha.com/input/?i=series+sin%28x%29&dataset=
    t1 = x;
    t3 = t1 -x.^3/6;
    t5 = t3 + x.^5/120;
end
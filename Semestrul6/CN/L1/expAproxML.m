function [t1, t2, t3, t4, t5] = expAproxML(x)
    c1 = [1, 1];
    t1 = polyval(c1, x);
    
    c2 = [1/2, 1, 1];
    t2 = polyval(c2, x);
    
    c3 = [1/6, c2];
    t3 = polyval(c3, x);
    
    c4 = [1/24, c3];
    t4 = polyval(c4, x);
    
    c5 = [1/120, c4];
    t5 = polyval(c5, x);
end
function rez=cerc(x, y, r, c)
    a = 0: 0.1: 2*pi;
    rez = fill(x + r*cos(a), y + r*sin(a), c);
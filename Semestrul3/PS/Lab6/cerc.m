function rez = cerc(x, y, r)
    a = 0: 0.1: 2*pi;
    rez = fill(x + r*cos(a), y + r*sin(a), 'w');
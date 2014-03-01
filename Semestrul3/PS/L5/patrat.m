function rez=patrat(x, y, l, c)
    rez=fill([x, x + l, x + l, x], [y, y, y + l, y + l], c);
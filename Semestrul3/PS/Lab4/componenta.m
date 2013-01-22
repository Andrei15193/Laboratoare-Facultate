function componenta(x, y, functionala, nr)
    colour = ['r', 'y'];
    hold on
    fill([x, x, x + 2, x + 2], [y, y + 1, y + 1, y], colour(mod(functionala, 2) + 1));
    text(x + 0.4, y + 0.5, ['A', int2str(nr)], 'Color', colour(mod(functionala + 1, 2) + 1));
function taie(x, y, offset)
    set(plot([x - offset, x + offset], [y - offset, y + offset], 'k'), 'LineWidth', 1.5 ); 
    set(plot([x - offset, x + offset], [y + offset, y - offset], 'k'), 'LineWidth', 1.5 ); 
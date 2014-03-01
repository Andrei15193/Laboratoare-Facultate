function afiseazaExtrase(extrase)
    size = length(extrase);
    clf
    hold on
    axis equal

    color = ['y', 'm', 'c', 'r', 'g', 'b'];
    for i = 1: size
        disc(i, 1, 0.4, color(unidrnd(length(color))));
        text(i - 0.1, 1, int2str(extrase(i)));
    end

function plotGrid(n, l, c)
    for i = 1: n + 1
        plot([i, i], [1, (n + 1) * l], c)
        plot([1, (n + 1) * l], [i, i], c)
    end
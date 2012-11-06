function plotFuncDeRepartitie()
    clf
    hold on
    for i = 1: 0.01: 13
        plot(i, FuncDeRepartitie([2:12 ;[1/36: 1/36: 1/6, 5/36: -1/36: 1/36]], i), 'r-')
    end

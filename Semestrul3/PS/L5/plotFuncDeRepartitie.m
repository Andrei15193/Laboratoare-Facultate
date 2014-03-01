function plotFuncDeRepartitie()
    clf
    hold on
    for i = 0: 0.01: 10
        plot(i, funcDeRepartitie(0: 10, [0.0277 0.1204 0.2328 0.2672 0.1999 0.1032 0.0377 0.0094 0.0015 0.0001 0 0], i), 'r');
    end
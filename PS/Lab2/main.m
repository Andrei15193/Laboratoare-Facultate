clear all

for n = 3: 4
    for i = 1: 3
        clf
        hold on
        plotGrid(n, 1, 'k');
        pause(1);
        makeRoad(n, 2, 'red*-', 1);
    end
end
hold on
text(1.1, 0.8, 'Done!')
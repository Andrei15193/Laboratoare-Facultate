clear all
n = 4;
s = 0;
m = 100;

for i = 1: m
    clf
    hold on
    axis equal
    plotGrid(n, 1, 'k');
    if makeRoad(n, 2, 'red*-', 0) ~= 0
        s = s + 1;
        text(1.1, 0.8, 'Succes!', 'Color', 'g')
    else
        text(1.1, 0.8, 'Esuat!', 'Color', 'r') 
    end
    pause(1);
end
hold on
text(1.1, 1.8, ['Done! Succeeded: ' num2str(s) '. Probability: ' num2str(s/n)])
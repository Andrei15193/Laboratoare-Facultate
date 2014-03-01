clear all
m = 40;
s = 0;
%         1   2   3   4   5   6   7   8   9   10  11
coords = [1   6.5 4   6.5 9   12  6   10  15  18  18;
          2.5 2   3.5 3.5 3.5 3   0.5 0.5 2.5 1.5 3.5];
componente = zeros(1, 11);
for j = 1: m
    componente(1)  = componentaBuna(0.3);
    componente(2)  = componentaBuna(0.13);
    componente(3)  = componentaBuna(0.21);
    componente(4)  = componentaBuna(0.3);
    componente(5)  = componentaBuna(0.5);
    componente(6)  = componentaBuna(0.1);
    componente(7)  = componentaBuna(0.7);
    componente(8)  = componentaBuna(0.1);
    componente(9)  = componentaBuna(0.3);
    componente(10) = componentaBuna(0.2);
    componente(11) = componentaBuna(0.2);
    clf
    hold on
    fill([0, 22, 22, 0], [0, 0, 20, 20], 'w');
    
    plot([0.2 3.5 3.5 3.7 3.7 11.5 11.5 14.5 14.5 3.5 3.5], [3 3 3.5 3.5 4 4 3.5 3.5 1 1 3], 'k');
    plot([3.7 3.7 11.5 11.5], [3.5 2.5 2.5 3.5], 'k');
    plot([14.5 17.5 17.5 20.5 20.5 17.5 17.5], [3 3 4 4 2 2 3], 'k');
    plot([20.5 21], [3 3], 'k');
    fill([0.4 0.8 0.4], [3.2 3 2.8], 'k');
    fill([21 21.4 21], [3.2 3 2.8], 'k');
    for i = 1: 11
        componenta(coords(1, i), coords(2, i), componente(i), i);
    end
    if (verificaCircuit(componente))
        s = s + 1;
    end
    text(1, 6, ['Solution ' int2str(j) ' of ' int2str(m)]);
    text(10, 6, [int2str(s) ' working solutions of ' int2str(m)]);
    pause(1);
end
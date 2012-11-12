clear all
clf
hold on

k = 10;
poz = 0;
p = 0.5;

fill([-k, k, k, -k], [0, 0, 10, 10], 'w');
plot([-k, k], [1, 1], 'k');
disc(poz, 1, 0.4, 'r');
text(poz - 0.2, 2, int2str(i));
text(poz - 0.2, 1, int2str(poz));
for i = 2: k
    if (unidrnd(100) / 100 < p)
        poz = poz + 1;
    else
        poz = poz - 1;
    end
    pause(1);
    fill([-k, k, k, -k], [0, 0, 10, 10], 'w');
    plot([-k, k], [1, 1], 'k');
    disc(poz, 1, 0.4, 'r');
    text(poz - 0.2, 2, int2str(i));
    text(poz - 0.2, 1, int2str(poz));
end

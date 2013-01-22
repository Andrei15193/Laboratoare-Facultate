clear all
clf
hold on

n = 20;
u = 360/n;
m = 20;
p = 0.5;
poz = 0;
r = 5;

for i = 1: m
    if (unidrnd(100) / 100 < p)
        poz = poz + 1;
    else
        poz = poz - 1;
    end
    fill([-1, -1, 2 * r + 1, 2 * r + 1], [-1, 2 * r + 1, 2 * r + 1, -1], 'w');
    cerc(r, r, r);
    x = r + r * sind(u * poz);
    y = r + r * cosd(u * poz);
    disc(x, y, 0.5, 'c');
    text(x - 0.1, y, int2str(i));
    pause(1);
end

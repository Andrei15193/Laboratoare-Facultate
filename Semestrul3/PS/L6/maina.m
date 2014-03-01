clear all
clf
hold on
r = 2;
cerc(3, 3, r);
I = 0;
U = 90;
L = r * U * pi / 180;
s = 0;
m = 100;

for i = 1 : m
    p = unidrnd(360) - 1;
    if p - U < U
        s = s + 1;
        c = 'm*';
    else
        c = 'r*';
    end
    plot(3 + 2 * sind(p - U + I), 3 + 2 * cosd(p - U + I), c);    
end
text(1, 0.6, [int2str(s) ' puncte din ' int2str(m) ' => P = ' num2str(s/m, 5)]);
plot(3 + 2 * sind(I), 3 + 2 * cosd(I), 'b*');
plot(3 + 2 * sind(I + U), 3 + 2 * cosd(I + U), 'g*');
plot(3 + 2 * sind(I - U), 3 + 2 * cosd(I - U), 'g*');

clear all
clf
hold on

m = 500;
s = 0;
punct = struct('x', 0, 'y', 0);
patrat(1, 1, 5, 'b');
cerc(3.5, 3.5, 2.5, 'r');
for i = 1: m
    punct.x = unidrnd(500)/100 + 1;
    punct.y = unidrnd(500)/100 + 1;
    if (sqrt(power(punct.x - 3.5, 2) + power(punct.y - 3.5, 2)) <= 2.5)
        s = s + 1;
    end
    plot(punct.x, punct.y, 'y*')
end
text(1, 6.2, [int2str(s) ' points out of ' int2str(m) ' are in the circle! (P = ' num2str(s/m, 4) ' ~ ' num2str(pi/4, 4) ')'])

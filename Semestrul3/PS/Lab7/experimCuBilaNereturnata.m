clf
hold on
axis equal

n = 12;
culori = ['y' 'r' 'g'];
indexsi = 1 : n;
bile = [5 4 3];
delim = cumsum(bile);
text(1, 11, 'Bile din urna: ');
deseneazaBile(bile, 10, culori);
text(1, 9, 'Bile extrase: ');
for i = 1 : 7
    pause(1);
    extras = unidrnd(n);
    if (indexsi(extras) <= delim(1))
        disc(i, 8, 0.4, culori(1));
    elseif (indexsi(extras) <= delim(2))
        disc(i, 8, 0.4, culori(2));
    else
        disc(i, 8, 0.4, culori(3));
    end
    taie(indexsi(extras), 10, 0.4);
    indexsi(extras) = [ ];
    n = n - 1;
end



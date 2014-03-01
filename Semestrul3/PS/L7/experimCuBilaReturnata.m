clear all
clf
hold on

axis equal
culori = ['y' 'r' 'g'];
text(1, 11, 'Bile din urna: ');
text(1, 9, 'Bile extrase: ');
deseneazaBile([5 4 3], 10, culori);
extrageri = myRand([1 2 3], [5/12 4/12 3/12], 7);
size = length(extrageri);
for i = 1 : size
    pause(1);
    disc(i, 8, 0.4, culori(extrageri(i)));
end

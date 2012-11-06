clear all
clf
hold on
patrat(0, 0, 7, 'w');
set(plot([1, 1, 6, 6, 1], [1, 6, 6, 1, 1], 'k'), 'lineWidth', 1);
punct = struct('x', unidrnd(500)/100 + 1, 'y', unidrnd(500)/100 + 1);
plot([1, punct.x], [1, punct.y], 'k');
plot([1, punct.x], [6, punct.y], 'k');
plot([6, punct.x], [6, punct.y], 'k');
plot([6, punct.x], [1, punct.y], 'k');
punct.x
punct.y
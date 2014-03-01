clear all
clf
hold on

n = unidrnd(8) + 2;
numberOfSolutions = factorial(n  - 1);
pozFix = struct('x', unidrnd(n), 'y', unidrnd(n));
solutii = perms(1: (n - 1));
culoare = ['k', 'w'];
for i = 1: numberOfSolutions
    clf
    hold on
    for j = 1: n
        for k = 1: n
            patrat(j, k, 1, culoare(mod(j + k, 2) + 1))
        end
    end

    cerc(pozFix.x + 0.5, pozFix.y + 0.5, 0.45, 'b')
    for j = 1: pozFix.x - 1
        if solutii(i, j) < pozFix.y
            cerc(j + 0.5, solutii(i, j) + 0.5, 0.45, 'r');
        else
            cerc(j + 0.5, solutii(i, j) + 1.5, 0.45, 'r');
        end
    end
    for j = pozFix.x: n - 1
        if solutii(i, j) < pozFix.y
            cerc(j + 1.5, solutii(i, j) + 0.5, 0.45, 'r');
        else
            cerc(j + 1.5, solutii(i, j) + 1.5, 0.45, 'r');
        end
    end
    
    text(1, 0.5, ['Solution ', int2str(i), ' of ', int2str(numberOfSolutions)]);
    pause(1);
end
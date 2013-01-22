function makeRoad(n, lineWidth, c, waitInterval)
    curent = struct('x', n + 1, 'y', n + 1);
    while (curent.x ~= 1 || curent.y ~= 1)
        anterior = curent;
        if (curent.x == 1)
            curent.y = curent.y - 1;
        elseif (curent.y == 1)
            curent.x = curent.x - 1;
        else
            whichWay = unidrnd(3);
            if (whichWay <= 2)
                curent.x = curent.x - 1;
            end
            if (2 <= whichWay)
                curent.y = curent.y - 1;
            end
        end
        desen = plot([anterior.x, curent.x], [anterior.y, curent.y], c);
        set(desen, 'LineWidth', lineWidth);
        pause(waitInterval);
    end
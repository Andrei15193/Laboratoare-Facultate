function rez = makeRoad(n, lineWidth, c, waitInterval)
    curent = struct('x', n, 'y', n);
    anterior = curent;
    whichWay = unidrnd(2);
    if (whichWay == 1)
        curent.y = curent.y - 1;
    elseif (whichWay == 2)
        curent.x = curent.x - 1;
    end
    while curent.x ~= 0 && curent.y ~= 0
        desen = plot([anterior.x + 0.5, curent.x + 0.5], [anterior.y + 0.5, curent.y + 0.5], c);
        set(desen, 'LineWidth', lineWidth);
        pause(waitInterval);
        anterior = curent;
        whichWay = unidrnd(2);
        if (whichWay == 1)
            curent.y = curent.y - 1;
        elseif (whichWay == 2)
            curent.x = curent.x - 1;
        end
    end
    if (anterior.x ~= anterior.y)
        if curent.x == 0
            desen = plot([anterior.x + 0.5, curent.x + 1], [anterior.y + 0.5, curent.y + 0.5], c);
            set(desen, 'LineWidth', lineWidth);
            pause(waitInterval);
        else
            desen = plot([anterior.x + 0.5, curent.x + 0.5], [anterior.y + 0.5, curent.y + 1], c);
            set(desen, 'LineWidth', lineWidth);
            pause(waitInterval);
        end
        rez = 0;
    else
        rez = 1;
    end
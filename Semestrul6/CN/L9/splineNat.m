function yi = splineNat(x, y, xi)
    a = rezolvaSistemSplineNat(x, y)';
    n = length(xi);
    yi = zeros(1, n);

    for j = 1 : n
       i_find = find(x>=xi(j)); % gaseste primul j pt care x(j)>=xi(i), pp x sortate
        if ~isempty(i_find)
            i = i_find(1)-1;
            if i<1
                i = 1; % in exteriorul intervalului, xi(i)<x(1)
            end
        else
            % in exteriorul intervalului xi(i)>=x(n) 
            i = n-1; % 
        end
        % s(x) = a(3*i-2) * (x-x(i))^3 + a(3*i-2) * (x-x(i))^2+ a(3*i-1) * (x-x(i)) + y(i)
        yi(j) = polyval([a(3*i-2:3*i), y(i)], xi(j)-x(i));
    end
end

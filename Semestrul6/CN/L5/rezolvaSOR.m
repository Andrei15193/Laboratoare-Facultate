function x = rezolvaSOR(A, b, omega, eroare)
    D = diag(diag(A));
	L = -tril(A,-1);
	U = -triu(A,1);
	
    M = D/omega - L;
    
	Minv = inv(omega * M);
	T = Minv * ((1 - omega) * D + omega * U);
	c = omega * Minv * b;
	
	nIteratii = 0;
	x1 = b;
	x2 = T*x1 + c;
	while norm(x2-x1)>(1-norm(T))/norm(T)*eroare
		x1 = x2;
		x2 = T*x1 + c;
		nIteratii = nIteratii+1;
		if (nIteratii > 100)
			error('Nu converge');
		end
	end 
	x = x1;
end

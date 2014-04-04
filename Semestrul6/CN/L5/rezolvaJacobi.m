function x = rezolvaJacobi(A, b, eroare)
    D = diag(diag(A));
	L = -tril(A,-1);
	U = -triu(A,1);
	
	M = D;
	N = L + U;
	
	Minv = inv(M);
	T = Minv * N;
	c = Minv * b;
	
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
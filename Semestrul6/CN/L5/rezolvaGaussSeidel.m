function x = rezolvaGaussSeidel(A,b, eroare)
	% rezolva iterativ sistemul A*x = b prin metoda Gauss-Seidel
	D = diag(diag(A));
	L = -tril(A,-1);
	U = -triu(A,1);
	%D=A+L+U
	
	M = D - L;
	N = U;
	
	Minv = inv(M);
	%mai eficient in acest caz particular: Minv = diag(1./diag(A));
	
	T = Minv * U;
	c = Minv *b;
	
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
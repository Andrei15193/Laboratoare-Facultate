function [pSin, pCos] = padeSinCos(x, k, m)
	x = rem(x, 2*pi);
	n = k+m+1;
	cSin = [0, 1]; % coeficientii MacLaurin pt. sin
	cCos = [1]; % coeficientii MacLaurin pt. cos
	coefK = 1; % k!
	for i = 1: n
		coefK = - coefK * (2*i); 
		cCos = [cCos, 0, 1/coefK];
		coefK = coefK * (2*i+1); 
		cSin = [cSin, 0, 1/coefK];
	end
	pSin = aproxPade(x, cSin, m, k);
	pCos = aproxPade(x, cCos, m, k);
	
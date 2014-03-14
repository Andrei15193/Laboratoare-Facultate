function lBits = bit_double(x)
	ix = typecast(double(x), 'uint64');
	lBits = [];
	iMask = 1;
	for iBit = 1:64
		iBitOn = bitand(ix, iMask) ~= 0; % 0 sau 1 daca bitul iBit este "on"(==1)
		lBits = [lBits,  iBitOn]; 
		iMask = iMask * 2;
    end
end
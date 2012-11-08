numarNoduri(nil, 0).
numarNoduri(arb(_, St, Dr), S) :-
	numarNoduri(St, Ss),
	numarNoduri(Dr, Sd),
	S is 1 + Ss + Sd.

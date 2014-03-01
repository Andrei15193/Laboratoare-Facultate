adauga(L, M, E, R) :-
	adauga(L, M, E, 0, R).

adauga([ ], M, E, I, [E]) :-
	0 is mod(I, M),
	!.

adauga([ ], _, _, _, [ ]).

adauga([H|T], M, E, 0, [H|R]) :-
	adauga(T, M, E, 1, R),
	!.

adauga([H|T], M, E, I, [E|[H|R]]) :-
	0 is mod(I, M),
	I1 is I + 1,
	adauga(T, M, E, I1, R),
	!.

adauga([H|T], M, E, I, [H|R]) :-
	I1 is I + 1,
	adauga(T, M, E, I1, R),
	!.

adaugaInLista([ ], _, _, [ ]).

adaugaInLista([l(H)|T], M, E, [l(R1)|R2]) :-
	adauga(H, M, E, R1),
	adaugaInLista(T, M, E, R2),
	!.

adaugaInLista([H|T], M, E, [H|R]) :-
	adaugaInLista(T, M, E, R),
	!.

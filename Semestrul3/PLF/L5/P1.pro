apartine([E], E).

apartine([E|_], E).

apartine([_|T], E) :-
	apartine(T, E).

submultimi([ ], 0, [ ]).

submultimi([H|T], N, [H|R]) :-
	N > 0,
	N1 is N - 1,
	not(apartine(T, H)),
	submultimi(T, N1, R).

submultimi([_|T], N, R) :-
	submultimi(T, N, R).

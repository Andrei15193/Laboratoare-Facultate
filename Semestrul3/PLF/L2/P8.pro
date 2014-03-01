/*
Laborator 2.8
Sa se sorteze o lista cu eliminarea dublurilor. Exemplu: [4 2 6 2 3 4]-->[2 3 4 6]
*/

% min(in L, out Min)
%     L: lista (nevida)
%     Min: elementul minim din L
min([H|T], E) :-
      min(T, H, E).
min([ ], M, M).
min([H|T], M, E) :-
      H < M,
      min(T, H, E),
      !.
min([_|T], M, E) :-
      min(T, M, E).

% elimina(in E, in L, out Rez)
%     E: element
%     L: lista
%     Rez: L \ {E} (toate aparitile sunt eliminate)
elimina(_, [ ], [ ]).
elimina(E, [H|T], Rez) :-
	H = E,
	elimina(E, T, Rez),
	!.
elimina(E, [H|T], Rez) :-
	elimina(E, T, Rez1),
	Rez = [H|Rez1].

% sortare(in L, out Rez)
%     L: lista
%     Rez: L multimea ordonata crescator formata cu elementele din L.
sortare([ ], [ ]).
sortare([H|T], Rez) :-
	min([H|T], Min),
	elimina(Min, [H|T], Rez1),
	sortare(Rez1, Rez2),
	Rez = [Min|Rez2].

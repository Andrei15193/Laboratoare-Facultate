/*
Laborator 2.8
Sa se sorteze o lista cu eliminarea dublurilor. Exemplu: [4 2 6 2 3 4]-->[2 3 4 6]
*/

% esteMinimSauMaiMic(in L, in E)
%     L: lista
%     E: un element
%     Returneaza: true daca E este mai mic decat orice element din L
%                 sau egal cu minimul din L
%                 false altfel.
esteMinimSauMaiMic([ ], _).
esteMinimSauMaiMic([H|T], E) :-
	E =< H,
	esteMinimSauMaiMic(T, E).

% min(L, Min)
%     L: lista (nevida)
%     Min: elementul minim din L
min([H|T], H) :-
    esteMinimSauMaiMic(T, H),
    !.
min([_|T], Min) :-
	min(T, Min).

% elimina(E, L, Rez)
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

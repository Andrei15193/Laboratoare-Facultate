/*
Laborator 1.7
a) Sa se scrie un predicat care transforma o lista intr-o multime, in
   ordinea primei aparitii. Exemplu: [1,2,3,1,2] e transformat in [1,2,3].

b) Sa se scrie o functie care descompune o lista de numere intr-o lista
   de forma [lista-de-numere-pare, lista-de-numere-impare] (deci lista
   cu doua elemente care sunt liste de intregi), si va intoarce si numarul
   elementelor pare si impare.
*/

% elimina(in E, in L, out Rez)
%     E: element
%     L: lista
%     Rez = L \ {E} (toate aparitile sunt eliminate)
elimina(_, [ ], [ ]).
elimina(E, [H|T], Rez) :-
	E = H,
	elimina(E, T, Rez),
	!.
elimina(E, [H|T], Rez) :-
	elimina(E, T, Rez1),
	Rez = [H|Rez1].

% multime(in L, out Rez)
%     L: lista
%     Rez = L fara duplicate, elementele is pastreaza ordinea.
multime([ ], [ ]).
multime([H|T], L) :-
	elimina(H, T, L1),
	multime(L1, L2),
	L = [H|L2].

% pare(L, Rez)
%     L: lista
%     Rez = L fara numere impare
pare([ ], [ ]).
pare([H|T], Rez) :-
	1 is H mod 2,
	pare(T, Rez),
	!.
pare([H|T], Rez) :-
	pare(T, Rez1),
	Rez = [H|Rez1].

% impare(L, Rez)
%     L: lista
%     Rez = L fara numere pare
impare([ ], [ ]).
impare([H|T], Rez) :-
	0 is H mod 2,
	impare(T, Rez),
	!.
impare([H|T], Rez) :-
        impare(T, Rez1),
	Rez = [H|Rez1].

% descompune(in L, out [RezPare, RezImpare], out LenPare, out LenImpare)
%     L: lista
%     RezPare: L fara numere impare
%     RezImpare: L fara numere pare
%     LenPare: lungimea sirului RezPare
%     LenImpare: lungimea sirului RezImpare
descompune([ ], [[ ], [ ]], LenPare, LenImpare) :-
         LenPare is 0,
         LenImpare is 0.
descompune(L, [RezPare, RezImpare], LenPare, LenImpare) :-
	pare(L, RezPare),
	impare(L, RezImpare),
        len(RezPare, LenPare),
        len(RezImpare, LenImpare).

% len(in L, out N)
%     L: lista
%     N: lungimea listei
len([ ], N) :-
       N is 0.
len([_|T], N) :-
       len(T, N1),
       N is 1 + N1.

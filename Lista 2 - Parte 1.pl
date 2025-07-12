
% Fatos: pai e mãe
pai(ivo, eva).
mae(ana, eva).

pai(gil, clo).
mae(bia, clo).

pai(gil, ary).
mae(bia, ary).

pai(gil, rai).
mae(bia, rai).

pai(rai, noe).
mae(eva, noe).

pai(ary, gal).
mae(lia, gal).

% Gênero
homem(ivo).
homem(gil).
homem(rai).
homem(ary).
homem(noe).

mulher(ana).
mulher(bia).
mulher(eva).
mulher(clo).
mulher(lia).
mulher(gal).

% Regra gerou
gerou(X, Y) :- pai(X, Y).
gerou(X, Y) :- mae(X, Y).

% Filho e filha
filho(X, Y) :- gerou(Y, X), homem(X).
filha(X, Y) :- gerou(Y, X), mulher(X).

% Irmão e irmã
irmao(X, Y) :- pai(P, X), pai(P, Y), mae(M, X), mae(M, Y), homem(X), X \== Y.
irma(X, Y) :- pai(P, X), pai(P, Y), mae(M, X), mae(M, Y), mulher(X), X \== Y.

% Tio e tia
tio(X, Y) :- (pai(P, Y); mae(P, Y)), irmao(X, P).
tia(X, Y) :- (pai(P, Y); mae(P, Y)), irma(X, P).

% Primo e prima
primo(X, Y) :- (tio(T, Y); tia(T, Y)), filho(X, T).
prima(X, Y) :- (tio(T, Y); tia(T, Y)), filha(X, T).

% Avô e avó
avo(X, Y) :- homem(X), gerou(X, Z), gerou(Z, Y).
avoa(X, Y) :- mulher(X), gerou(X, Z), gerou(Z, Y).

% Pessoas felizes
feliz(X) :- gerou(X, _).

% Casal (têm filhos em comum)
casal(X, Y) :- gerou(X, Z), gerou(Y, Z), X \== Y.

% Consultas
% ?- gerou(X, eva).              % Deve retornar X = ivo ; X = ana.
% ?- filha(clo, bia).            % true
% ?- tio(X, noe).                % Deve retornar X = rai ; X = ary.
% ?- primo(gal, noe).            % true
% ?- feliz(bia).                 % true
% ?- casal(gil, bia).            % true

% Relação de geração (pais e filhos)
gerou(maria, joao).
gerou(jose, joao).
gerou(maria, ana).
gerou(jose, ana).
gerou(joao, mario).
gerou(ana, helena).
gerou(ana, joana).
gerou(helena, carlos).
gerou(mario, carlos).

% Gênero
feminino(maria).
feminino(ana).
feminino(helena).
feminino(joana).

masculino(jose).
masculino(joao).
masculino(mario).
masculino(carlos).

% pai e mãe
pai(X, Y) :- gerou(X, Y), masculino(X).
mae(X, Y) :- gerou(X, Y), feminino(X).

% irmãos e irmãs
irmao(X, Y) :- pai(P, X), pai(P, Y), mae(M, X), mae(M, Y), masculino(X), X \== Y.
irma(X, Y) :- pai(P, X), pai(P, Y), mae(M, X), mae(M, Y), feminino(X), X \== Y.
irmaos(X, Y) :- gerou(Z, X), gerou(Z, Y), X \== Y.

% descendente
descendente(X, Y) :- gerou(Y, X).
descendente(X, Y) :- gerou(Y, Z), descendente(X, Z).

% avô e avó
avo(X, Y) :- masculino(X), gerou(X, Z), gerou(Z, Y).
avoh(X, Y) :- feminino(X), gerou(X, Z), gerou(Z, Y).

% tio: irmão de um dos pais
tio(X, Y) :- pai(P, Y), irmao(X, P).
tio(X, Y) :- mae(M, Y), irmao(X, M).

% primo: filho de um tio ou tia
primo(X, Y) :- tio(T, X), gerou(T, Y).
primo(X, Y) :- tio(T, Y), gerou(T, X).

%consultas
% 1. pai(jose, joao).
% 2. gerou(maria, X).
% 3. primo(X, mario).
% 4. tio(Tio, Sobrinho), findall(Sobrinho, tio(_, Sobrinho), Lista), sort(Lista, Unicos), length(Unicos, N).
% 5. irmaos(X, helena).

% Fatos com as informações da tabela
pessoa(ana, fem, 23, 1.55, 56.0).
pessoa(bia, fem, 19, 1.71, 61.3).
pessoa(ivo, masc, 22, 1.80, 70.5).
pessoa(lia, fem, 17, 1.85, 57.3).
pessoa(eva, fem, 28, 1.75, 68.7).
pessoa(ary, masc, 25, 1.72, 68.9).

% a) Mulheres com mais de 20 anos de idade
mulher_mais_20(Nome) :- pessoa(Nome, fem, Idade, _, _), Idade > 20.

% b) Pessoas com pelo menos 1.70m de altura e menos de 65kg
altura_peso(Nome) :- pessoa(Nome, _, _, Altura, Peso), Altura >= 1.70, Peso < 65.

% c) Casais onde o homem é mais alto que a mulher
casal_possivel(Homem, Mulher) :-
    pessoa(Homem, masc, _, AltH, _),
    pessoa(Mulher, fem, _, AltM, _),
    AltH > AltM.

% Consultas:
% ?- mulher_mais_20(Nome).       % Mulheres com mais de 20 anos
% ?- altura_peso(Nome).          % Pessoas com altura >= 1.70m e peso < 65kg
% ?- casal_possivel(Homem, Mulher). % Casais onde o homem é mais alto que a mulher

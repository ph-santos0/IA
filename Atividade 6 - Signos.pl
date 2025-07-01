% Base de dados de aniversários
aniversario(pedro, data(30, outubro, 2003)).
aniversario(ana, data(15, maio, 2001)).
aniversario(bruno, data(3, fevereiro, 2000)).
aniversario(carol, data(23, julho, 2004)).
aniversario(diego, data(8, dezembro, 2002)).

% Regra de signo
signo(Pessoa, aquario) :- aniversario(Pessoa, data(Dia, janeiro, _)), Dia >= 20.
signo(Pessoa, aquario) :- aniversario(Pessoa, data(Dia, fevereiro, _)), Dia =< 18.
signo(Pessoa, peixes) :- aniversario(Pessoa, data(Dia, fevereiro, _)), Dia >= 19.
signo(Pessoa, peixes) :- aniversario(Pessoa, data(Dia, marco, _)), Dia =< 20.
signo(Pessoa, aries) :- aniversario(Pessoa, data(Dia, marco, _)), Dia >= 21.
signo(Pessoa, aries) :- aniversario(Pessoa, data(Dia, abril, _)), Dia =< 19.
signo(Pessoa, touro) :- aniversario(Pessoa, data(Dia, abril, _)), Dia >= 20.
signo(Pessoa, touro) :- aniversario(Pessoa, data(Dia, maio, _)), Dia =< 20.
signo(Pessoa, gemeos) :- aniversario(Pessoa, data(Dia, maio, _)), Dia >= 21.
signo(Pessoa, gemeos) :- aniversario(Pessoa, data(Dia, junho, _)), Dia =< 20.
signo(Pessoa, cancer) :- aniversario(Pessoa, data(Dia, junho, _)), Dia >= 21.
signo(Pessoa, cancer) :- aniversario(Pessoa, data(Dia, julho, _)), Dia =< 22.
signo(Pessoa, leao) :- aniversario(Pessoa, data(Dia, julho, _)), Dia >= 23.
signo(Pessoa, leao) :- aniversario(Pessoa, data(Dia, agosto, _)), Dia =< 22.
signo(Pessoa, virgem) :- aniversario(Pessoa, data(Dia, agosto, _)), Dia >= 23.
signo(Pessoa, virgem) :- aniversario(Pessoa, data(Dia, setembro, _)), Dia =< 22.
signo(Pessoa, libra) :- aniversario(Pessoa, data(Dia, setembro, _)), Dia >= 23.
signo(Pessoa, libra) :- aniversario(Pessoa, data(Dia, outubro, _)), Dia =< 22.
signo(Pessoa, escorpiao) :- aniversario(Pessoa, data(Dia, outubro, _)), Dia >= 23.
signo(Pessoa, escorpiao) :- aniversario(Pessoa, data(Dia, novembro, _)), Dia =< 21.
signo(Pessoa, sagitario) :- aniversario(Pessoa, data(Dia, novembro, _)), Dia >= 22.
signo(Pessoa, sagitario) :- aniversario(Pessoa, data(Dia, dezembro, _)), Dia =< 21.
signo(Pessoa, capricornio) :- aniversario(Pessoa, data(Dia, dezembro, _)), Dia >= 22.
signo(Pessoa, capricornio) :- aniversario(Pessoa, data(Dia, janeiro, _)), Dia =< 19.
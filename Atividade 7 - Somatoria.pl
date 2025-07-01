soma([], 0).
soma([X|Y], N) :- 
    soma(Y, NY),      
    N is X + NY.
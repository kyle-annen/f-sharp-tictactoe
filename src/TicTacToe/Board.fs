module TicTacToe.Board

type Space = 
    | A 
    | B
    | Empty
    
type Board = Space list

let initBoard (n:int) :Board =
    [ for i in 1..(n * n) do yield Empty]
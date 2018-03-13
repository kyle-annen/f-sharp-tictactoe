module TicTacToe.MockGameState
open Types
open Board

let getInitialGameState : GameState = 
    let board = initBoard 3
    let human = { PlayerType = Human; Difficulty = Hard; Space = A }
    let computer = { PlayerType = Computer; Difficulty = Hard; Space = B }
    { CurrentPlayer = human; NextPlayer = computer; Board = board; Result = None; }
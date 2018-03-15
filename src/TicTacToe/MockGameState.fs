module TicTacToe.MockGameState

open Types
open Board

let GetInitialGameState : GameState = 
    let board = InitBoard 3
    let human = { PlayerType = Human; Difficulty = Hard; Space = A }
    let computer = { PlayerType = Computer; Difficulty = Hard; Space = B }
    { CurrentPlayer = human; NextPlayer = computer; Board = board; Result = None; }
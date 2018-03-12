module TicTacToe.AI

open Types
open Board

let swapPlayers (state:GameState) =
    { state with 
        CurrentPlayer = state.NextPlayer;
        NextPlayer = state.CurrentPlayer;}

let updateBoard (state:GameState) (newBoard:Board) =
    { state with 
        Board = newBoard; 
        Result = (getResult newBoard) }

let negamax (board:Board) : int = 
    board |> getOpenMoves |> List.head


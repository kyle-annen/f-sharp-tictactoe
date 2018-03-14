module TicTacToe.GameState

open Types
open Board

let SwapPlayers (state:GameState) =
    { state with 
        CurrentPlayer = state.NextPlayer;
        NextPlayer = state.CurrentPlayer;}

let UpdateBoard (state:GameState) (newBoard:Board) =
    { state with 
        Board = newBoard; 
        Result = (GetResult newBoard) }
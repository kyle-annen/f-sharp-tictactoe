module TicTacToe.AI

open Board

type PlayerType =
    | Human
    | Computer

type Difficulty = 
    | Easy
    | Medium
    | Hard

type Player = {
    PlayerType: PlayerType;
    Difficulty: Difficulty;
    Space: Space;
}

type GameState = {
    CurrentPlayer: Player;
    NextPlayer: Player;
    Board: Board;
    Result: Result;
}

let swapPlayers (state:GameState) =
    { state with 
        CurrentPlayer = state.NextPlayer;
        NextPlayer = state.CurrentPlayer;}

// let negamax (board:Board) : int = 
//   getOpenMoves board
//   |> List.head

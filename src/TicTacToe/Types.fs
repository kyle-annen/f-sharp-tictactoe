module TicTacToe.Types

type Space = 
    | A 
    | B
    | Empty

type Board = Space list

type Result =
    | Win
    | Tie
    | None

type Difficulty = 
    | Easy
    | Medium
    | Hard

type PlayerType =
    | Human
    | Computer

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

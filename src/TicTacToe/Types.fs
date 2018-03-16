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

type IndexedBoard = (int * Space) list

type NegamaxState = {
    MaxSpace: Space;
    CurrentSpace: Space;
    Depth: int;
    IndexedBoard: IndexedBoard;
}

type ScoreStrategy =
    | Max
    | Min

type LogLevel =
    | Info
    | Debug
    | Danger
    | Game

type Response =
    | Yes
    | No

type GameVersion =
    | ComputerVsComputer
    | HumanVsComputer
    | HumanVsHuman

type Option = char

type Options = Option list

type Score = int

type Move = int

type MoveScore = Move * Score

type Depth = int

type Moves = Move list

type OutputFn = string -> unit

type InputFn = bool -> Option
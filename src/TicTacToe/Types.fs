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

type Language = 
    | English
    | Mandarin

type DialogSet = {
    Greeting : string;
    SelectLang : string;
    SelectPlayerType : string;
    SelectDifficulty : string;
    PTypeHuman : string;
    PTypeComputer : string;
    PickBoardSize : string;
    TurnPrompt : string;
    PlayerAnnounce : string;
    GameOver : string;
    Win : string;
    Tie : string;
    InvalidPlay : string;
    InputPrompt : string;
    ContinuePlaying : string;
    Easy : string;
    Medium : string;
    Hard : string;
}
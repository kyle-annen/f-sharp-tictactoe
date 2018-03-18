module TicTacToe.Game

open Types

let private boardSize3x3 = 3
let private player1 = 1
let private player2 = 2

let GetComputerDifficulty
    (input : InputFn) (output : OutputFn) (playerNumber : int) : Difficulty =
    UI.DisplayDifficultyPrompt output playerNumber
    Input.GetDifficulty input

let SetupGameState
    (input : InputFn) (output : OutputFn) : GameState =
    UI.DisplayGameTypePrompt output

    let basePlayer = { PlayerType = Computer; Difficulty = Easy; Space = A }

    let players =
        match Input.GetGameVersion input with
        | ComputerVsComputer ->
            let player1 = { basePlayer with 
                Difficulty = GetComputerDifficulty input output player1}
            let player2 = { basePlayer with Difficulty = GetComputerDifficulty input output player2; Space = B }
            (player1, player2)
        | HumanVsComputer ->
            let player1 = { basePlayer with PlayerType = Human; }
            let player2 = { basePlayer with Difficulty = GetComputerDifficulty input output player2; Space = B }
            (player1, player2)
        | HumanVsHuman ->
            let player1 = { basePlayer with PlayerType = Human; }
            let player2 = { basePlayer with PlayerType = Human; Space = B }
            (player1, player2)

    { CurrentPlayer = players |> fst;
        NextPlayer = players |> snd;
        Board = Board.InitBoard boardSize3x3;
        Result = Playing;}

let rec GameLoop (input : InputFn) (output : OutputFn) (gameState : GameState) =
    UI.DisplayUI output gameState

    match gameState.Result with
    | Playing ->
        gameState
        |> Input.GetMove input
        |> GameState.ProgressGameState gameState
        |> GameLoop input output
    | _ -> ()

let rec PlayGame (input : InputFn) (output : OutputFn) (playing : bool) : unit =
    if playing then
        UI.DisplayGreeting output
        SetupGameState input output |> GameLoop input output
        ContinueOrQuit input output

and ContinueOrQuit (input : InputFn) (output : OutputFn) : unit =
    UI.DisplayContinueMessage output

    match Input.GetYesOrNo input with
    | Yes -> PlayGame input output true
    | _ -> PlayGame input output false

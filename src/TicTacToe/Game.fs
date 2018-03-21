module TicTacToe.Game

open Types

let private boardSize3x3 = 3
let private player1 = 1
let private player2 = 2

let GetComputerDifficulty
    (input : IInput) (output : IOutput) (playerNumber : int) : Difficulty =
    UI.DisplayDifficultyPrompt output playerNumber
    Input.GetDifficulty input

let SetupGameState
    (input : IInput) (output : IOutput) : GameState =
    UI.DisplayGameTypePrompt output

    let basePlayer = { PlayerType = Computer; Difficulty = Easy; Space = A }

    let players =
        match Input.GetGameVersion input with
        | ComputerVsComputer ->
            let player1 = { basePlayer with Difficulty = GetComputerDifficulty input output player1}
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

let rec GameLoop (input : IInput) (output : IOutput) (gameState : GameState) =
    UI.DisplayUI output gameState

    match gameState.Result with
    | Playing ->
        gameState
        |> Input.GetMove input
        |> GameState.ProgressGameState gameState
        |> GameLoop input output
    | _ -> ()

let rec PlayGame (input : IInput) (output : IOutput) (playing : bool) : bool =
    if playing then
        UI.DisplayGreeting output
        SetupGameState input output |> GameLoop input output
        ContinueOrQuit input output
    else true

and ContinueOrQuit (input : IInput) (output : IOutput) : bool =
    UI.DisplayContinueMessage output

    match Input.GetYesOrNo input with
    | Yes -> PlayGame input output true
    | _ -> PlayGame input output false

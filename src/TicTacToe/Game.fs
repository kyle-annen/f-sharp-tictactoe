module TicTacToe.Game

open Types

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
        Setup.SetupGameState input output |> GameLoop input output
        ContinueOrQuit input output
    else true

and ContinueOrQuit (input : IInput) (output : IOutput) : bool =
    UI.DisplayContinueMessage output

    match Input.GetYesOrNo input with
    | Yes -> PlayGame input output true
    | _ -> PlayGame input output false

module TicTacToe.Game

open Types

let SetupGameState
    (input : InputFn) (output : OutputFn) =
    output Dialog.GameOptions
    match Input.GetGameVersion input with
    | ComputerVsComputer -> 'a'
    | HumanVsComputer ->'a'
    | HumanVsHuman -> 'a'

let GetMove
    (input : InputFn) (gameState : GameState) : Move =
    match gameState.CurrentPlayer.PlayerType with
    | Human -> Input.GetHumanMove input gameState
    | Computer -> AI.GetAIMove gameState

let rec GameLoop
    (input : InputFn) (output : OutputFn) (gameState : GameState) =
    gameState
    |> UI.FormatBoard UI.Template3x3
    |> output

    match gameState.Result with
    | Win -> output Dialog.Win
    | Tie -> output Dialog.Tie
    | _ ->
        output Dialog.TurnPrompt
        gameState
        |> GetMove input
        |> GameState.ProgressGameState gameState
        |> GameLoop input output

let rec PlayGame
    (input : InputFn) (output : OutputFn) (playing : bool) : unit =
    if playing then
        output UI.ClearScreen
        output Dialog.Greeting
        GameLoop input output MockGameState.GetInitialGameState
        ContinueOrQuit input output

and ContinueOrQuit (input : InputFn) (output : OutputFn) : unit =
    output Dialog.ContinuePlaying

    match Input.GetYesOrNo input with
    | Yes -> PlayGame input output true
    | _ -> PlayGame input output false

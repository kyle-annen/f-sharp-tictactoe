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

let TurnAnnouncement (output : OutputFn) (gameState : GameState) =
    match gameState.CurrentPlayer.PlayerType with
    | Computer ->
        output Dialog.VerticalPadding
        output Dialog.ComputerAnnounce
        output Dialog.ComputerThinking
        System.Threading.Thread.Sleep(1000)
    | Human  ->
        output Dialog.VerticalPadding
        match gameState.CurrentPlayer.Space with
        | A -> output Dialog.Player1Announce
        | _ -> output Dialog.Player2Announce
        output Dialog.MoveEntryPrompt

let DisplayBoard (output : OutputFn) (gameState : GameState) =
    gameState |> UI.FormatBoard UI.Template3x3 |> output

let DisplayHeading (output : OutputFn) =
    output Dialog.VerticalPadding
    output Dialog.Greeting
    output Dialog.VerticalPadding

let DisplayMessageWithPadding (output : OutputFn) (message : string) =
        output Dialog.VerticalPadding
        output message
        output Dialog.VerticalPadding

let DisplayGameMessages (output : OutputFn) (gameState : GameState) =
    match gameState.Result with
    | Win ->
        DisplayMessageWithPadding output Dialog.GameOver
        match gameState.NextPlayer.Space with
        | A -> Dialog.Player1Win
        | _ -> Dialog.Player2Win
        |> output
    | Tie ->
        DisplayMessageWithPadding output Dialog.GameOver
        output Dialog.Tie
    | _ -> TurnAnnouncement output gameState

let DisplayUI (output : OutputFn) (gameState : GameState) =
    UI.ClearScreen()
    DisplayHeading output
    DisplayBoard output gameState
    DisplayGameMessages output gameState


let rec GameLoop (input : InputFn) (output : OutputFn) (gameState : GameState) =
    DisplayUI output gameState

    match gameState.Result with
    | None ->
        gameState
        |> GetMove input
        |> GameState.ProgressGameState gameState
        |> GameLoop input output
    | _ -> ()

let rec PlayGame
    (input : InputFn) (output : OutputFn) (playing : bool) : unit =
    if playing then
        UI.ClearScreen()
        output Dialog.Greeting
        GameLoop input output MockGameState.GetInitialGameState
        ContinueOrQuit input output

and ContinueOrQuit (input : InputFn) (output : OutputFn) : unit =
    output Dialog.ContinuePlaying

    match Input.GetYesOrNo input with
    | Yes -> PlayGame input output true
    | _ -> PlayGame input output false

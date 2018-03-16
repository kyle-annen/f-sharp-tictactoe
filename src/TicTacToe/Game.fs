module TicTacToe.Game

open Types
open System.Runtime.InteropServices

let basePlayer = { PlayerType = Computer; Difficulty = Easy; Space = A }

let SetComputerDifficulty (input : InputFn) (output : OutputFn) (playerNum : int) : Difficulty =
    UI.ClearScreen()
    output Dialog.Greeting
    output Dialog.NewLine
    output (Dialog.SelectDifficulty + (sprintf "%i" playerNum))
    output Dialog.NewLine
    output Dialog.DifficultyOptions
    Input.GetDifficulty input

let SetupGameState (input : InputFn) (output : OutputFn) : GameState =
    output Dialog.SelectGameType
    output Dialog.NewLine
    output Dialog.GameOptions
    let players =
        match Input.GetGameVersion input with
        | ComputerVsComputer ->
            let player1 = { basePlayer with Difficulty = SetComputerDifficulty input output 1}
            let player2 = { basePlayer with Difficulty = SetComputerDifficulty input output 2; Space = B }
            (player1, player2)
        | HumanVsComputer ->
            let player1 = { basePlayer with PlayerType = Human; }
            let player2 = { basePlayer with Difficulty = SetComputerDifficulty input output 2; Space = B }
            (player1, player2)
        | HumanVsHuman ->
            let player1 = { basePlayer with PlayerType = Human; }
            let player2 = { basePlayer with PlayerType = Human; Space = B }
            (player1, player2)

    { CurrentPlayer = players |> fst;
        NextPlayer = players |> snd;
        Board = Board.InitBoard 3;
        Result = None}

let GetMove (input : InputFn) (gameState : GameState) : Move =
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

        SetupGameState input output |> GameLoop input output

        ContinueOrQuit input output

and ContinueOrQuit (input : InputFn) (output : OutputFn) : unit =
    output Dialog.ContinuePlaying

    match Input.GetYesOrNo input with
    | Yes -> PlayGame input output true
    | _ -> PlayGame input output false

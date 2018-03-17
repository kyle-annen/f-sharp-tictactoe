module TicTacToe.UI

open Types
open Logger

let private xToken = "X"
let private oToken = "O"

let ConsoleRender : OutputFn = Log LogLevel.Game (printf "%s \n")

let ClearScreen (output : OutputFn) =
    let consoleRenderIdentifier = ConsoleRender.GetType().FullName
    match output.GetType().FullName with
    | consoleRenderIdentifier -> System.Console.Clear()
    | _ -> ()

let Template3x3<'a> =
    Printf.StringFormat<'a>" %s | %s | %s \n---+---+---\n %s | %s | %s \n---+---+---\n %s | %s | %s "

let private applyTemplate template board : string =
    match board with
    | [a;b;c;d;e;f;g;h;i] -> sprintf template a b c d e f g h i
    | _ -> ""


let FormatBoard template (gameState : GameState) =
    gameState.Board
    |> List.mapi (
        fun i space ->
            match space with
            | A -> xToken
            | B -> oToken
            | _ -> sprintf "%i" (i + 1))
    |> applyTemplate template

let DisplayDifficultyPrompt (output : OutputFn) (playerNumber : int) =
    ClearScreen output
    output Dialog.Greeting
    output Dialog.NewLine
    output (Dialog.SelectDifficulty + (sprintf "%i" playerNumber))
    output Dialog.NewLine
    output Dialog.DifficultyOptions

let DisplayGameTypePrompt (output : OutputFn) =
    output Dialog.SelectGameType
    output Dialog.NewLine
    output Dialog.GameOptions

let DisplayTurnPrompt (output : OutputFn) (gameState : GameState) =
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
    gameState |> FormatBoard Template3x3 |> output

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

    | _ -> DisplayTurnPrompt output gameState

let DisplayUI (output : OutputFn) (gameState : GameState) =
    ClearScreen output
    DisplayHeading output
    DisplayBoard output gameState
    DisplayGameMessages output gameState

let DisplayGreeting (output : OutputFn) =
        ClearScreen output
        output Dialog.Greeting

let DisplayContinueMessage (output : OutputFn) =
    output Dialog.ContinuePlaying
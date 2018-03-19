module TicTacToe.UI

open Types
open Logger

let private xToken = "X"
let private oToken = "O"

let private padRow = "          "

let Template3x3<'a> =
    Printf.StringFormat<'a>"%s %s ┃ %s ┃ %s \n%s━━━╋━━━╋━━━\n%s %s ┃ %s ┃ %s \n%s━━━╋━━━╋━━━\n%s %s ┃ %s ┃ %s "

let ConsoleRender : IOutput = Log LogLevel.Game (printf "%s \n")

let ClearScreen (output : IOutput) =
    LanguagePrimitives.PhysicalEquality ConsoleRender output
    |> function
    | true ->
        System.Console.Clear()
        true
    | _ -> false

let private applyTemplate template board : string =
    match board with
    | [a;b;c;d;e;f;g;h;i] ->
        sprintf template padRow a b c padRow padRow d e f padRow padRow g h i
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

let DisplayDifficultyPrompt (output : IOutput) (playerNumber : int) =
    ClearScreen output |> ignore
    output Dialog.Greeting
    output Dialog.NewLine
    output (Dialog.SelectDifficulty + (sprintf "%i" playerNumber))
    output Dialog.NewLine
    output Dialog.DifficultyOptions

let DisplayGameTypePrompt (output : IOutput) =
    output Dialog.SelectGameType
    output Dialog.NewLine
    output Dialog.GameOptions

let DisplayTurnPrompt (output : IOutput) (gameState : GameState) =
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

let DisplayBoard (output : IOutput) (gameState : GameState) =
    gameState |> FormatBoard Template3x3 |> output

let DisplayHeading (output : IOutput) =
    output Dialog.VerticalPadding
    output Dialog.Greeting
    output Dialog.VerticalPadding

let DisplayMessageWithPadding (output : IOutput) (message : string) =
    output Dialog.VerticalPadding
    output message
    output Dialog.VerticalPadding

let private displayGameMessages (output : IOutput) (gameState : GameState) =
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

let DisplayUI (output : IOutput) (gameState : GameState) =
    ClearScreen output |> ignore
    DisplayHeading output
    DisplayBoard output gameState
    displayGameMessages output gameState

let DisplayGreeting (output : IOutput) =
    ClearScreen output |> ignore
    output Dialog.Greeting

let DisplayContinueMessage (output : IOutput) =
    output Dialog.ContinuePlaying
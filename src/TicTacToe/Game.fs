module TicTacToe.Game

open TicTacToe.Types

let yesNoOptions = ['y';'n']
let oneTwoOptions = ['1';'2']

let GetDialogSet language =
    match language with
    | Types.English -> UI.Dialog.English
    | Types.Mandarin -> UI.Dialog.Mandarin

let MatchLanguage selection =
    match selection with
    | '1' -> Types.English
    | _ -> Types.Mandarin

let GetLaguageSelection outputFn (dialog : Types.DialogSet) =
    outputFn dialog.SelectLang
    outputFn UI.EmptyLine
    outputFn UI.Dialog.LanguageOption.English
    outputFn UI.Dialog.LanguageOption.Mandarin
    outputFn UI.EmptyLine
    Input.GetInput oneTwoOptions |> MatchLanguage

let GetMoveOptions (gameState : Types.GameState) =
    gameState.Board
    |> Board.getOpenMoves 
    |> List.map (string >> char)

let GetHumanMove (gameState : Types.GameState) : int = 
    gameState
    |> GetMoveOptions
    |> Input.GetInput
    |> string
    |> int

let GetMove (gameState : Types.GameState) : int =
    match gameState.CurrentPlayer.PlayerType with
    | Types.Human -> GetHumanMove gameState
    | Types.Computer -> AI.GetAIMove gameState

let rec GameLoop outputFn (dialogSet : Types.DialogSet) (gameState : Types.GameState) = 
    outputFn UI.ClearScreen
    gameState |> UI.FormatBoard UI.Template3x3 |> outputFn 
    match gameState.Result with
    | Win -> outputFn dialogSet.Win 
    | Tie -> outputFn dialogSet.Tie
    | _ -> 
        outputFn dialogSet.TurnPrompt
        gameState
        |> GetMove
        |> GameState.ProgressGameState gameState
        |> GameLoop outputFn dialogSet 

let rec PlayGame outputFn (playing : bool) (language: Types.Language) =
    if playing then
        outputFn UI.ClearScreen 
        let startDialogSet = GetDialogSet language 
        outputFn startDialogSet.Greeting
        let dialogSet = GetLaguageSelection outputFn startDialogSet |> GetDialogSet
        GameLoop outputFn dialogSet MockGameState.GetInitialGameState
        ContinueOrQuit outputFn dialogSet

and ContinueOrQuit outputFn (dialogSet : DialogSet) =
    outputFn dialogSet.ContinuePlaying

    match Input.GetInput yesNoOptions with
    | 'y' -> PlayGame outputFn true dialogSet.LanguageType
    | _ -> PlayGame outputFn false dialogSet.LanguageType

let Run = PlayGame UI.ConsoleRender true Types.English
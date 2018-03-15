module TicTacToe.Game
open TicTacToe.Types

let yesNoOptions = ['y';'n']
let oneTwoOptions = ['1';'2']

let GetLanguage language =
    match language with
    | Types.English -> UI.Dialog.English
    | Types.Mandarin -> UI.Dialog.Mandarin

let MatchLanguage selection =
    match selection with
    | '1' -> Types.English
    | _ -> Types.Mandarin

let GetLaguageSelection (dialog : Types.DialogSet) =
    UI.ConsoleRender dialog.SelectLang
    UI.ConsoleRender UI.EmptyLine
    UI.ConsoleRender UI.Dialog.LanguageOption.English
    UI.ConsoleRender UI.Dialog.LanguageOption.Mandarin
    UI.ConsoleRender UI.EmptyLine

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

let rec GameLoop (language : Types.DialogSet) (gameState : Types.GameState) = 
    UI.ClearScreen
    gameState |> UI.FormatBoard UI.Template3x3 |> UI.ConsoleRender
    match gameState.Result with
    | Win -> UI.ConsoleRender language.Win 
    | Tie -> UI.ConsoleRender language.Tie
    | _ -> 
        UI.ConsoleRender language.TurnPrompt
        gameState |> GetMove |> GameState.ProgressGameState gameState |> GameLoop language

let rec PlayGame (playing : bool) (startLanguage: Types.Language) =
    if playing then
        UI.ClearScreen
        let startDialogSet = GetLanguage startLanguage 
        UI.ConsoleRender startDialogSet.Greeting
        let language = GetLaguageSelection startDialogSet |> GetLanguage
        GameLoop language MockGameState.GetInitialGameState

and ContinueOrQuit (dialogSet : DialogSet) =
    UI.ConsoleRender dialogSet.ContinuePlaying
    match Input.GetInput yesNoOptions with
    | 'y' -> PlayGame true dialogSet.LanguageType
    | _ -> PlayGame false dialogSet.LanguageType

let Run = PlayGame true Types.English
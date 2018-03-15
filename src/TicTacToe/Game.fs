module TicTacToe.Game
open TicTacToe

let yesNoOptions = ['y';'n']

let oneTwoOptions = ['1';'2']

let GetLanguage language =
    match language with
    | Types.English -> UI.Dialog.English
    | Types.Mandarin -> UI.Dialog.Mandarin

let GetLaguageSelection (dialog : Types.DialogSet) =
    UI.ConsoleRender dialog.SelectLang
    UI.ConsoleRender ""
    UI.ConsoleRender UI.Dialog.LanguageOption.English
    UI.ConsoleRender UI.Dialog.LanguageOption.Mandarin
    UI.ConsoleRender ""

    let selection = Input.GetInput oneTwoOptions

    match selection with
    | '1' -> Types.English
    | _ -> Types.Mandarin

let rec PlayGame (playing : bool) (startLanguage: Types.Language) =
    if playing then
        let initialLanguage = GetLanguage startLanguage 
        UI.ClearScreen
        UI.ConsoleRender initialLanguage.Greeting

        let language = GetLaguageSelection initialLanguage |> GetLanguage
        
        UI.ClearScreen

        MockGameState.GetInitialGameState
        |> UI.FormatBoard UI.Template3x3
        |> UI.ConsoleRender

        UI.ConsoleRender language.GameOver

        UI.ConsoleRender language.ContinuePlaying

        match Input.GetInput yesNoOptions with
        | 'y' -> PlayGame true language.LanguageType
        | _ -> PlayGame false language.LanguageType

let Run = PlayGame true Types.English


        



        



        

        


    
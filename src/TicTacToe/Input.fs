module TicTacToe.Input

let private yesNoOptions = ['y';'n']

let ConsoleInput (surpress : bool) : Types.Option =
    System.Console.ReadKey(surpress).KeyChar

let CheckInput (option : Types.Option) (validOptions : Types.Options) =
    List.contains option validOptions

let rec GetInput
    (input : Types.InputFn) (validOptions : Types.Options) : Types.Option =

    let option : Types.Option = input true
    match option with
    | _ when CheckInput option validOptions -> option
    | _ -> GetInput input validOptions

let GetHumanMove
    (input : Types.InputFn)
    (gameState : Types.GameState) : Types.Move =

    gameState
    |> GameState.GetMoveOptions
    |> GetInput input
    |> string
    |> int

let GetYesOrNo (input : Types.InputFn) : Types.Response =
    match GetInput input yesNoOptions with
    | 'y' -> Types.Response.Yes
    | _ -> Types.Response.No

let GetGameVersion (input : Types.InputFn) : Types.GameVersion =
    match GetInput input ['1';'2';'3'] with
    | '1' -> Types.GameVersion.ComputerVsComputer
    | '2' -> Types.GameVersion.HumanVsComputer
    | _ -> Types.GameVersion.HumanVsHuman
module TicTacToe.Input

let private yesNoOptions = ['y';'n']

let ConsoleInput (surpress : bool) : Types.Option =
    System.Console.ReadKey(surpress).KeyChar

let CheckInput (option : Types.Option) (validOptions : Types.Options) =
    List.contains option validOptions

let rec GetInput
    (input : Types.IInput) (validOptions : Types.Options) : Types.Option =

    let option : Types.Option = input true
    match option with
    | _ when CheckInput option validOptions -> option
    | _ -> GetInput input validOptions

let GetHumanMove
    (input : Types.IInput)
    (gameState : Types.GameState) : Types.Move =

    gameState
    |> GameState.GetMoveOptions
    |> GetInput input
    |> string
    |> int

let GetYesOrNo (input : Types.IInput) : Types.Response =
    match GetInput input yesNoOptions with
    | 'y' -> Types.Response.Yes
    | _ -> Types.Response.No

let GetGameVersion (input : Types.IInput) : Types.GameVersion =
    match GetInput input ['1';'2';'3'] with
    | '1' -> Types.GameVersion.ComputerVsComputer
    | '2' -> Types.GameVersion.HumanVsComputer
    | _ -> Types.GameVersion.HumanVsHuman

let GetDifficulty (input : Types.IInput) : Types.Difficulty =
    match GetInput input ['1';'2';'3'] with
    | '1' -> Types.Easy
    | '2' -> Types.Medium
    | _ -> Types.Hard

let GetMove (input : Types.IInput) (gameState : Types.GameState) : Types.Move =
    match gameState.CurrentPlayer.PlayerType with
    | Types.Human -> GetHumanMove input gameState
    | Types.Computer -> AI.GetAIMove gameState
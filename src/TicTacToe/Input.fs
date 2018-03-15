module TicTacToe.Input

let yesNoOptions = ['y';'n']
let rec GetInput (validCharacters : char list) : char =
    let charValue = System.Console.ReadKey(true).KeyChar
    match charValue with
    | _ when List.contains charValue validCharacters -> charValue
    | _ -> GetInput validCharacters

let GetMoveOptions (gameState : Types.GameState) =
    gameState.Board
    |> Board.getOpenMoves
    |> List.map (string >> char)

let GetHumanMove (gameState : Types.GameState) : int =
    gameState
    |> GetMoveOptions
    |> GetInput
    |> string
    |> int

let GetYesOrNo =
    match GetInput yesNoOptions with
    | 'y' -> Types.Response.Yes
    | _ -> Types.Response.No

let GetGameVersion =
    match GetInput ['1';'2';'3'] with
    | '1' -> Types.GameVersion.ComputerVsComputer
    | '2' -> Types.GameVersion.HumanVsComputer
    | _ -> Types.GameVersion.HumanVsHuman
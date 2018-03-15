module TicTacToe.Game

open TicTacToe.Types
let GameOptions = "1 - Computer Vs Computer\n2 - Human Vs Computer\n3 - Human Vs Human"
let SetupGameState (outputFn : string -> unit) =
    outputFn GameOptions
    let gameVersion = Input.GetGameVersion
    let basePlayer = { PlayerType = Computer; Difficulty = Easy; Space = A}

    let players =
        match gameVersion with
        | ComputerVsComputer ->
            ({ basePlayer }, {basePlayer with Space = B})
        | HumanVsComputer ->
            ({ basePlayer with PlayerType = Human },
                {basePlayer with Space = B})
        | HumanVsHuman ->
            ({ basePlayer with PlayerType = Human },
                {basePlayer with PlayerType = Human; Space = B})







let GetMove (gameState : Types.GameState) : int =
    match gameState.CurrentPlayer.PlayerType with
    | Types.Human -> Input.GetHumanMove gameState
    | Types.Computer -> AI.GetAIMove gameState

let rec GameLoop outputFn (gameState : Types.GameState) =
    gameState
    |> UI.FormatBoard UI.Template3x3
    |> outputFn

    match gameState.Result with
    | Win -> outputFn Dialog.Win
    | Tie -> outputFn Dialog.Tie
    | _ ->
        outputFn Dialog.TurnPrompt
        gameState
        |> GetMove
        |> GameState.ProgressGameState gameState
        |> GameLoop outputFn

let rec PlayGame outputFn (playing : bool) =
    if playing then
        outputFn UI.ClearScreen
        outputFn Dialog.Greeting
        let gameState = SetupGameState outputFn
        GameLoop outputFn MockGameState.GetInitialGameState
        ContinueOrQuit outputFn

and ContinueOrQuit outputFn =
    outputFn Dialog.ContinuePlaying

    match Input.GetYesOrNo with
    | Yes -> PlayGame outputFn true
    | _ -> PlayGame outputFn false

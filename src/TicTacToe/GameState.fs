module TicTacToe.GameState

let SwapPlayers (state : Types.GameState) : Types.GameState =
    { state with
        CurrentPlayer = state.NextPlayer;
        NextPlayer = state.CurrentPlayer;}

let UpdateBoard
    (state : Types.GameState) (newBoard : Types.Board) : Types.GameState =
    { state with Board = newBoard; Result = Board.GetResult newBoard }

let ProgressGameState
    (gameState : Types.GameState) (location : int) : Types.GameState =
    gameState.Board
    |> Board.PlaceMove location gameState.CurrentPlayer.Space
    |> UpdateBoard gameState
    |> SwapPlayers

let GetMoveOptions (gameState : Types.GameState) : Types.Options =
    gameState.Board
    |> Board.getOpenMoves
    |> List.map (string >> char)
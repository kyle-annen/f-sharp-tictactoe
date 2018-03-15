module TicTacToe.GameState
open TicTacToe


let SwapPlayers (state : Types.GameState) =
    { state with 
        CurrentPlayer = state.NextPlayer;
        NextPlayer = state.CurrentPlayer;}

let UpdateBoard (state : Types.GameState) (newBoard : Types.Board) =
    { state with 
        Board = newBoard; 
        Result = (Board.GetResult newBoard) }

let ProgressGameState (gameState : Types.GameState) (location : int) =
    gameState.Board
    |> Board.PlaceMove location gameState.CurrentPlayer.Space 
    |> UpdateBoard gameState
    |> SwapPlayers

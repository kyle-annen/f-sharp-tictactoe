module TicTacToe.AI

open Types
open Board

let SwapCurrentSpace (state : NegamaxState) : NegamaxState =
    let nextSpace =
        match state.CurrentSpace with
        | A -> B
        | _ -> A
    { state with CurrentSpace = nextSpace }

let UpdateNegamaxBoard
    (newBoard:IndexedBoard) (state:NegamaxState) : NegamaxState =
    { state with IndexedBoard = newBoard }

let IncreaseDepth (state:NegamaxState) : NegamaxState =
    { state with Depth = (state.Depth + 1) }

let ProgressState
    (newBoard:IndexedBoard) (state:NegamaxState) : NegamaxState =
    state
    |> SwapCurrentSpace
    |> UpdateNegamaxBoard newBoard
    |> IncreaseDepth

let ScoreBoard depth : int =  100 - depth

let FlipScore (score : Score) : Score = score * -1

let Unindex (list:('a * 'b) list) = List.map (fun (_, v) -> v) list

let EmptySpaces = List.filter (fun (_, space) -> space = Empty)

let GetStartState (gameState:GameState) : NegamaxState = {
    MaxSpace = gameState.CurrentPlayer.Space;
    CurrentSpace = gameState.CurrentPlayer.Space;
    Depth = 1;
    IndexedBoard = List.indexed gameState.Board;
}

let GetScoreStrategy state =
    match state.CurrentSpace = state.MaxSpace with
    | true -> Max
    | _ -> Min

let Increment  = (+) 1

let MakeMoveIndexedBoard
    (state : NegamaxState) (move : Move) : IndexedBoard =
    state.IndexedBoard
    |> List.map (
        fun (i, v) ->
            if i = move then (i, state.CurrentSpace)
            else (i, v))

let private getBestMove
    (state : NegamaxState)
    (scoreStrategy : ScoreStrategy)
    (scoreList: MoveScore list) : MoveScore =
    let reindexList = scoreList |> Unindex |> List.indexed

    let bestMoveIndex =
        match scoreStrategy with
        | Max -> List.maxBy snd reindexList |> fst
        | Min -> List.minBy snd reindexList |> fst

    let board = state.IndexedBoard |> EmptySpaces
    let move : Move = board.[bestMoveIndex] |> fst
    let score : Score = reindexList.[bestMoveIndex] |> snd

    (move, score)

let Minimax (depthLimit : Depth) (gameState : GameState) : Move =
    let rec go (negamaxState:NegamaxState) : Move * Score =
        let scoreStrategy = GetScoreStrategy negamaxState

        negamaxState.IndexedBoard
        |> EmptySpaces
        |> List.map (
            fun (move, _) ->
                let newBoard = MakeMoveIndexedBoard negamaxState move
                let score = ScoreBoard negamaxState.Depth
                let result = newBoard |> List.map (fun (_, v) -> v) |> GetResult
                match (result, scoreStrategy) with
                | (Win, Max)  -> (move, score)
                | (Win, _)    -> (move, score |> FlipScore)
                | (Tie, _)    -> (move, 0)
                | (_, Max) when negamaxState.Depth > depthLimit -> (move, score - 2)
                | (_, Min) when negamaxState.Depth > depthLimit -> (move, score + 2)
                | (None, _)   -> negamaxState |> ProgressState newBoard |> go)
        |> getBestMove negamaxState scoreStrategy

    go (GetStartState gameState) |> fst |> Increment

let RandomMove (gameState : GameState) : Move =
    let openMoves = getOpenMoves gameState.Board
    let index = System.Random().Next(openMoves.Length)
    openMoves.[index]

let GetAIMove (gameState:GameState) : Move =
    let mediumDepth = 3
    let hardDepth = 10
    match gameState.CurrentPlayer.Difficulty with
    | Easy -> RandomMove gameState
    | Medium -> Minimax mediumDepth gameState
    | Hard ->  Minimax hardDepth gameState

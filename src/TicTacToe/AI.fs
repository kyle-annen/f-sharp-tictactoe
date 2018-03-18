module TicTacToe.AI

open Types
open Board

let MEDIUM_DEPTH = 4
let HARD_DEPTH = 10

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

let private unindex = List.map (fun (_, v) -> v)

let private emptySpaces = List.filter (fun (_, space) -> space = Empty)

let GetStartState (gameState:GameState) : NegamaxState = {
    MaxSpace = gameState.CurrentPlayer.Space;
    CurrentSpace = gameState.CurrentPlayer.Space;
    Depth = 1;
    IndexedBoard = List.indexed gameState.Board;
}

let GetScoreStrategy (state : NegamaxState) : ScoreStrategy =
    match state.CurrentSpace = state.MaxSpace with
    | true -> Max
    | _ -> Min

let private increment  = (+) 1

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
    let reindexList = scoreList |> unindex |> List.indexed

    let bestMoveIndex =
        match scoreStrategy with
        | Max -> List.maxBy snd reindexList |> fst
        | Min -> List.minBy snd reindexList |> fst

    let board = state.IndexedBoard |> emptySpaces
    let move : Move = board.[bestMoveIndex] |> fst
    let score : Score = reindexList.[bestMoveIndex] |> snd

    (move, score)

let Minimax (depthLimit : Depth) (gameState : GameState) : Move =
    let rec go (negamaxState:NegamaxState) : Move * Score =
        let scoreStrategy = GetScoreStrategy negamaxState

        negamaxState.IndexedBoard
        |> emptySpaces
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
                | (Playing, _)   -> negamaxState |> ProgressState newBoard |> go)
        |> getBestMove negamaxState scoreStrategy

    go (GetStartState gameState) |> fst |> increment

let RandomMove (gameState : GameState) : Move =
    let openMoves = GetOpenMoves gameState.Board
    let index = System.Random().Next(openMoves.Length)
    openMoves.[index]

let GetAIMove (gameState:GameState) : Move =
    match gameState.CurrentPlayer.Difficulty with
    | Easy -> RandomMove gameState
    | Medium -> Minimax MEDIUM_DEPTH gameState
    | Hard ->  Minimax HARD_DEPTH gameState

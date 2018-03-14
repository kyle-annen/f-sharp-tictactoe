module TicTacToe.AI

open Types
open Board

let swapCurrentSpace (state:NegamaxState) =
    let nextSpace = 
        match state.CurrentSpace with
        | A -> B
        | _ -> A
    { state with CurrentSpace = nextSpace }

let updateNegamaxBoard (newBoard:IndexedBoard) (state:NegamaxState) =
    { state with IndexedBoard = newBoard }

let increaseDepth (state:NegamaxState) =
    { state with Depth = (state.Depth + 1) }

let progressState (newBoard:IndexedBoard) (state:NegamaxState) =
    state |> swapCurrentSpace |> updateNegamaxBoard newBoard |> increaseDepth

let scoreBoard depth : int =  100 - depth

let flipScore = (*) -1

let unindex (list:('a * 'b) list) = 
    List.map (fun (_, v) -> v) list

let emptySpaces = List.filter (fun (_, space) -> space = Empty) 

let getStartState (gameState:GameState) = {
    MaxSpace = gameState.CurrentPlayer.Space;
    CurrentSpace = gameState.CurrentPlayer.Space;
    Depth = 1;
    IndexedBoard = List.indexed gameState.Board;
}

let getScoreStrategy state = 
    match state.CurrentSpace = state.MaxSpace with
    | true -> Max
    | _ -> Min

let inc  = (+) 1

let makeMoveIndexedBoard state move : IndexedBoard = 
    state.IndexedBoard
    |> List.map (
        fun (i, v) -> if i = move then (i, state.CurrentSpace) else (i, v))

let private getBestMove state scoreStrategy (scoreList: (int * int) list) =
    let reindexList = scoreList |> unindex |> List.indexed

    let bestMoveIndex =
        match scoreStrategy with
        | Max -> List.maxBy snd reindexList |> fst
        | Min -> List.minBy snd reindexList |> fst

    let board = state.IndexedBoard |> emptySpaces 
    let moveIndex = board.[bestMoveIndex] |> fst
    let moveScore = reindexList.[bestMoveIndex] |> snd

    (moveIndex, moveScore)

let minimax (depthLimit:int) (gameState:GameState) : int = 
    let rec go (state:NegamaxState) : int * int =
        let scoreStrategy = getScoreStrategy state

        let scores = 
            state.IndexedBoard
            |> emptySpaces
            |> List.map (
                fun (move, _) ->
                    let newBoard = makeMoveIndexedBoard state move
                    let score = scoreBoard state.Depth
                    let result = newBoard |> List.map (fun (_, v) -> v) |> getResult
                    match (result, scoreStrategy) with
                    | (Win, Max)  -> (move, score)
                    | (Win, _)    -> (move, score |> flipScore)
                    | (Tie, _)    -> (move, 0)
                    | (_, Max) when state.Depth > depthLimit -> (move, score - 2)
                    | (_, Min) when state.Depth > depthLimit -> (move, score + 2)
                    | (None, _)   -> state |> progressState newBoard |> go)
        getBestMove state scoreStrategy scores

    go (getStartState gameState) |> fst |> inc

let randomMove (gameState : GameState) =
    let openMoves = getOpenMoves gameState.Board
    let index = System.Random().Next(openMoves.Length)
    openMoves.[index]


let getAIMove (difficulty:Difficulty) (gameState:GameState) =
    let mediumDepth = 3
    let hardDepth = 10
    match difficulty with
    | Easy -> randomMove gameState
    | Medium -> minimax mediumDepth gameState
    | Hard ->  minimax hardDepth gameState

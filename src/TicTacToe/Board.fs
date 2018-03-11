module TicTacToe.Board

type Space = 
    | A 
    | B
    | Empty

type Board = Space list

let initBoard (dimension:int) : Board =
    [ for i in 1..(dimension * dimension) do yield Empty]

let getPreceding (loc:int) (board:Board) : Board =
    board
    |> Seq.take (loc - 1)
    |> Seq.toList

let getFollowing (loc:int) (board:Board) : Board =
    if loc = board.Length then
        []
    else 
        board
        |> Seq.skip (loc)
        |> Seq.toList

let getMove space : Board = [space;]

let placeMove (loc:int) (space:Space) (board:Board) :Board = 
    (getFollowing loc board)
    |> List.append (getMove space)
    |> List.append (getPreceding loc board)

let private openSpace (i, v) = v = Empty

let private indexToLocation (i, v) = i + 1

let getOpenMoves (board:Board) = 
    board
    |> List.indexed
    |> List.filter openSpace
    |> List.map indexToLocation

let checkSeq (board:Board) : bool =  
    match board.Head with
    | Empty -> false
    | _ -> List.forall (fun elem -> elem = board.Head) board

let intSqrt (n:int) : int = 
    n |> float |> sqrt |> int
    
let rows (board:Board) : Board list = 
    board |> List.chunkBySize (intSqrt board.Length)
module TicTacToe.Board

type Space = 
    | A 
    | B
    | Empty
    
type Board = Space list

let initBoard (dimension:int) :Board =
    [ for i in 1..(dimension * dimension) do yield Empty]

let getPreceding (loc:int) (board:Board) :Board =
    board
    |> Seq.take (loc - 1)
    |> Seq.toList

let getFollowing (loc:int) (board:Board) :Board =
    if loc = board.Length then
        []
    else 
        board
        |> Seq.skip (loc)
        |> Seq.toList

let getMove space :Board = [space;]

let placeMove (loc:int) (space:Space) (board:Board) :Board = 
    (getFollowing loc board)
    |> List.append (getMove space)
    |> List.append (getPreceding loc board)

let getOpenMoves (board:Board) = 
    board
    |> List.toArray
    |> Array.indexed
    |> Array.filter (fun (i, v) -> v = Empty)
    |> Array.map (fun (i, b) -> i + 1)
    |> Array.toList
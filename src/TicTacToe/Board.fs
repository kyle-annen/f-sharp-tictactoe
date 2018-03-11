module TicTacToe.Board

type Space = 
    | A 
    | B
    | Empty
    
type Board = Space list

let initBoard (n:int) :Board =
    [ for i in 1..(n * n) do yield Empty]

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

module TicTacToe.Board

type Space = 
    | A 
    | B
    | Empty

type Board = Space list

type Result =
    | Win
    | Tie
    | None

let initBoard (dimension:int) : Board =
    [ for _ in 1..(dimension * dimension) do yield Empty]

let getPreceding (loc:int) (board:Board) : Board =
    board
    |> Seq.take (loc - 1)
    |> Seq.toList

let getFollowing (loc:int) (board:Board) : Board =
    if loc = board.Length then []
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

let private indexToLocation (index, value) = index + 1

let getOpenMoves (board:Board) = 
    board
    |> List.indexed
    |> List.filter openSpace
    |> List.map indexToLocation

let private checkEmpty (board:Board) : bool =
    List.exists (fun elem -> elem = Empty) board

let checkSeq (board:Board) : bool =  
    match checkEmpty board with
    | true -> false
    | _ -> List.forall (fun elem -> elem = board.Head) board

let intSqrt (n:int) : int = 
    n |> float |> sqrt |> int
    
let rows (board:Board) : Board list = 
    board |> List.chunkBySize (intSqrt board.Length)

let private unIndexed (_, v) = v 

let private unIndexNestedList elem = List.map unIndexed elem

let private colomn dimension = fun (i, _) -> (i + 1) % dimension

let columns (board:Board) : Board list =
    let dimension = intSqrt board.Length
    board
    |> List.indexed
    |> List.groupBy (colomn dimension)
    |> List.map (unIndexed >> unIndexNestedList)

let diagonals (board:Board) : Board list =
    let dimension = (intSqrt board.Length)

    let diagonal1 = 
        [for i in 1..board.Length do
            if (i - 1) % (dimension + 1) = 0 then yield i]

    let diagonal2 = 
        [for i in dimension..(board.Length - 1) do
            if (i + 1) % (dimension - 1) = 0 then yield i]

    [ diagonal1; diagonal2 ]
    |> List.map (fun elem -> List.map (fun v -> board.[v - 1]) elem)

let checkWin (board:Board) : bool =
    (rows board)
    |> List.append (columns board)
    |> List.append (diagonals board)
    |> List.fold (fun acc boardSeq -> acc || (checkSeq boardSeq)) false

let checkTie (board:Board) : bool = 
    let emptySpaces = List.exists (fun elem -> elem = Empty) board
    (not emptySpaces) && (not (checkWin board))

let getResult (board:Board) : Result =
    match (checkWin board, checkTie board) with
        | (true, _) -> Win
        | (_ , true) -> Tie
        | (_ , _) -> None
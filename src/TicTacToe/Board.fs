module TicTacToe.Board

open Types

let InitBoard (dimension:int) : Board =
    [ for _ in 1..(dimension * dimension) do yield Empty]

let GetPreceding (move : Move) (board:Board) : Board =
    board
    |> Seq.take (move - 1)
    |> Seq.toList

let GetFollowing (move : Move) (board:Board) : Board =
    match (move, board) with
    | (_, _) when board.Length = move -> []
    | _ -> board |> Seq.skip move |> Seq.toList

let GetMove space : Board = [space;]

let PlaceMove (move : Move) (space : Space) (board : Board) : Board =
    GetFollowing move board
    |> List.append (GetMove space)
    |> List.append (GetPreceding move board)

let private openSpace (_, space: Space) = space = Empty

let private indexToMove (index, _) : Move = index + 1

let getOpenMoves (board : Board) : Moves =
    board
    |> List.indexed
    |> List.filter openSpace
    |> List.map indexToMove

let private checkEmpty (board : Board) : bool =
    board |> List.exists (fun elem -> elem = Empty)

let CheckList(board : Board) : bool =
    match checkEmpty board with
    | true -> false
    | _ -> List.forall (fun elem -> elem = board.Head) board

let IntSqrt (n:int) : int =
    n |> float |> sqrt |> int

let Rows (board:Board) : Board list =
    board |> List.chunkBySize (IntSqrt board.Length)

let private unIndex (_, value) = value

let private unIndexNestedList elem = List.map unIndex elem

let Colomn dimension = fun (i, _) -> (i + 1) % dimension

let Columns (board : Board) : Board list =
    let dimension = IntSqrt board.Length
    board
    |> List.indexed
    |> List.groupBy (Colomn dimension)
    |> List.map (unIndex >> unIndexNestedList)

let Diagonals (board : Board) : Board list =
    let dimension = (IntSqrt board.Length)

    let diagonal1 =
        [for i in 1..board.Length do
            if (i - 1) % (dimension + 1) = 0 then yield i]

    let diagonal2 =
        [for i in dimension..(board.Length - 1) do
            if (i + 1) % (dimension - 1) = 0 then yield i]

    [ diagonal1; diagonal2 ]
    |> List.map (fun elem -> List.map (fun v -> board.[v - 1]) elem)

let CheckWin (board : Board) : bool =
    (Rows board)
    |> List.append (Columns board)
    |> List.append (Diagonals board)
    |> List.fold (fun acc boardSeq -> acc || (CheckList boardSeq)) false

let CheckTie (board : Board) : bool =
    let emptySpaces = List.exists (fun elem -> elem = Empty) board
    (not emptySpaces) && (not (CheckWin board))

let GetResult (board : Board) : Result =
    match (CheckWin board, CheckTie board) with
        | (true, _) -> Win
        | (_ , true) -> Tie
        | (_ , _) -> None
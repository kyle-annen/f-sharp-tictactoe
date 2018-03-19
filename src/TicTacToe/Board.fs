module TicTacToe.Board

open Types

let InitBoard (dimension : int) : Board =
    [ for _ in 1..(dimension * dimension) do yield Empty]

let PrecedingSpaces (move : Move) (board:Board) : Board =
    board
    |> Seq.take (move - 1)
    |> Seq.toList

let FollowingSpaces (move : Move) (board:Board) : Board =
    match (move, board) with
    | (_, _) when board.Length = move -> []
    | _ -> board |> Seq.skip move |> Seq.toList

let PlaceMove (move : Move) (space : Space) (board : Board) : Board =
    FollowingSpaces move board
    |> List.append [space]
    |> List.append (PrecedingSpaces move board)

let private openSpace (_, space: Space) = space = Empty

let private indexToMove (index, _) : Move = index + 1

let GetOpenMoves (board : Board) : Moves =
    board
    |> List.indexed
    |> List.filter openSpace
    |> List.map indexToMove

let private checkEmpty (board : Board) : bool =
    board |> List.exists (fun space -> space = Empty)

let private checkCombo (board : Board) =
    List.forall (fun space -> space = board.Head) board

let CheckCombo (board : Board) : bool =
    let openMovePresent = checkEmpty board
    match openMovePresent with
    | true -> false
    | _ -> checkCombo board

let private intSqrt (n:int) : int =
    n |> float |> sqrt |> int

let Rows (board:Board) : Board list =
    board |> List.chunkBySize (intSqrt board.Length)

let private unIndex (_, value) = value

let private unIndexNestedList elem = List.map unIndex elem

let private colomn dimension =
    fun (i, _) -> (i + 1) % dimension

let Columns (board : Board) : Board list =
    let dimension = intSqrt board.Length
    board
    |> List.indexed
    |> List.groupBy (colomn dimension)
    |> List.map (unIndex >> unIndexNestedList)

let private getBoardValuesFromIndex (board : Board) =
    fun indexList -> List.map (fun index -> board.[index - 1]) indexList

let Diagonals (board : Board) : Board list =
    let dimension = (intSqrt board.Length)

    let diagonal1 =
        [for i in 1..board.Length do
            if (i - 1) % (dimension + 1) = 0 then yield i]

    let diagonal2 =
        [for i in dimension..(board.Length - 1) do
            if (i + 1) % (dimension - 1) = 0 then yield i]

    [ diagonal1; diagonal2 ]
    |> List.map (getBoardValuesFromIndex board)

let private checkAllCombos = fun acc boardCombo -> acc || (CheckCombo boardCombo)

let private checkWin (board : Board) : bool =
    (Rows board)
    |> List.append (Columns board)
    |> List.append (Diagonals board)
    |> List.fold checkAllCombos false

let private checkTie (board : Board) : bool =
    let emptySpaces = List.exists (fun elem -> elem = Empty) board
    let win = checkWin board
    (not emptySpaces) && (not win)

let GetResult (board : Board) : Result =
    let win = checkWin board
    let tie = checkTie board

    match (win, tie) with
        | (true, _) -> Win
        | (_ , true) -> Tie
        | (_ , _) -> Playing
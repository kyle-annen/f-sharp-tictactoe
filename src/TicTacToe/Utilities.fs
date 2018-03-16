module TicTacToe.Utilities

let IntSqrt (n:int) : int =
    n |> float |> sqrt |> int

let private unIndex (_, value) = value
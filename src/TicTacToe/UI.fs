module TicTacToe.UI

open Types
open Logger

let private template3x3 = 
    Printf.StringFormat<'a>" %s | %s | %s \n---+---+---\n %s | %s | %s \n---+---+---\n %s | %s | %s "

let consoleRenderer = Log LogLevel.Game (printf "%s")

let RenderBoard outputFn (gameState : GameState) = 
    let board = 
        gameState.Board
        |> List.mapi (
            fun i space -> 
                match space with 
                | A -> "X"
                | B -> "O"
                | _ -> sprintf "%i" (i + 1))
    match board with 
    | [a;b;c;d;e;f;g;h;i] -> sprintf template3x3 a b c d e f g h i
    | _ -> ""
    |> outputFn
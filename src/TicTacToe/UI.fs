module TicTacToe.UI

open Types
open Logger

let private xToken = "X"
let private oToken = "O"
let EmptyLine = ""

let ClearScreen() = System.Console.Clear()

let Template3x3<'a> =
    Printf.StringFormat<'a>" %s | %s | %s \n---+---+---\n %s | %s | %s \n---+---+---\n %s | %s | %s "

let private applyTemplate template board : string =
    match board with
    | [a;b;c;d;e;f;g;h;i] -> sprintf template a b c d e f g h i
    | _ -> ""

let ConsoleRender = Log LogLevel.Game (printf "%s \n")

let FormatBoard template (gameState : GameState) =
    gameState.Board
    |> List.mapi (
        fun i space ->
            match space with
            | A -> xToken
            | B -> oToken
            | _ -> sprintf "%i" (i + 1))
    |> applyTemplate template

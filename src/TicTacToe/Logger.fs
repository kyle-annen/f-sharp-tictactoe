module TicTacToe.Logger

open Types

let private addContent content header = header + content

let Log (level:LogLevel) outputFn (content:string) = 
    let info, danger, debug, game = "Info", "Danger", "Debug", ""
    let template = Printf.StringFormat<string->string>"F# TicTacToe [%s]: "

    match level with
    | Info -> sprintf template info
    | Debug -> sprintf template debug
    | Danger -> sprintf template danger
    | Game -> game 
    |> addContent content
    |> outputFn
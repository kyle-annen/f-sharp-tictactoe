// Learn more about F# at http://fsharp.org

open System
open TicTacToe

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    UI.RenderBoard UI.consoleRenderer MockGameState.GetInitialGameState
    0 // return an integer exit code

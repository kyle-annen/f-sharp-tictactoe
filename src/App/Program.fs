open TicTacToe

[<EntryPoint>]
let main argv =
    Game.PlayGame Input.ConsoleInput UI.ConsoleRender true
    0

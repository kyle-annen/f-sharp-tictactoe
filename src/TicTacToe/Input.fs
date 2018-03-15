module TicTacToe.Input

let rec GetInput (validCharacters : char list) : char = 
    let charValue = System.Console.ReadKey(true).KeyChar
    match charValue with
    | _ when List.contains charValue validCharacters -> charValue
    | _ -> GetInput validCharacters




namespace TicTacToe.Tests.GameTests

open TicTacToe
open Types
open NUnit.Framework


[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.GameLoopCanPlayHumanVsHuman() =
        let mutable inputCount = 0
        let mockInput (surpress: bool) =
            inputCount <- (inputCount + 1)

            match inputCount with
                | 1 -> '1'
                | 2 -> '5'
                | 3 -> '2'
                | 4 -> '6'
                | _ -> '3'

        let mockOutput (message : string) = ()

        let gameState = {
            CurrentPlayer = { PlayerType = Human; Difficulty = Hard; Space = A };
            NextPlayer = { PlayerType = Human; Difficulty = Hard; Space = B };
            Board = Board.InitBoard 3;
            Result = Playing }

        Game.GameLoop mockInput mockOutput gameState

    [<Test>]
    member this.GameLoopCanPlayComputerVsComputer() =
        let mockInput (surpress: bool) = 'n'
        let mockOutput (message : string) = ()

        let gameState = {
            CurrentPlayer = { PlayerType = Computer; Difficulty = Hard; Space = A };
            NextPlayer = { PlayerType = Computer; Difficulty = Hard; Space = B };
            Board = Board.InitBoard 3;
            Result = Playing }

        Game.GameLoop mockInput mockOutput gameState

    [<Test>]
    member this.GameLoopCanPlayHumanVsComputer() =
        let mutable inputCount = 0
        let mockInput (surpress: bool) =
            inputCount <- (inputCount + 1)

            match inputCount with
                | 1 -> '1'
                | 2 -> '9'
                | _ -> '6'

        let mockOutput (message : string) = ()

        let gameState = {
            CurrentPlayer = { PlayerType = Human; Difficulty = Hard; Space = A };
            NextPlayer = { PlayerType = Computer; Difficulty = Hard; Space = B };
            Board = Board.InitBoard 3;
            Result = Playing }

        Game.GameLoop mockInput mockOutput gameState

    [<Test>]
    member this.PlayGameCanPlayComputerVsComputer() =
        let mutable inputCount = 0
        let mockInput (surpress: bool) =
            inputCount <- (inputCount + 1)

            match inputCount with
                | 1 -> '1'
                | 2 -> '3'
                | 3 -> '3'
                | _ -> 'n'

        let mockOutput (message : string) = ()

        let outcome = Game.PlayGame mockInput mockOutput true
        Assert.AreEqual(true, outcome)

    [<Test>]
    member this.PlayGameCanPlayHumanVsComputer() =
        let mutable inputCount = 0
        let mockInput (surpress: bool) =
            inputCount <- (inputCount + 1)

            match inputCount with
                | 1 -> '2'
                | 2 -> '3'
                | 3 -> '1'
                | 4 -> '9'
                | 5 -> '6'
                | _ -> 'n'

        let mockOutput (message : string) = ()

        let outcome = Game.PlayGame mockInput mockOutput true
        Assert.AreEqual(true, outcome)

    [<Test>]
    member this.PlayGameCanPlayHumanVsHuman() =
        let mutable inputCount = 0
        let mockInput (surpress: bool) =
            inputCount <- (inputCount + 1)

            match inputCount with
                | 1 -> '3'
                | 2 -> '1'
                | 3 -> '6'
                | 4 -> '2'
                | 5 -> '7'
                | 6 -> '3'
                | _ -> 'n'

        let mockOutput (message : string) = ()

        let outcome = Game.PlayGame mockInput mockOutput true
        Assert.AreEqual(true, outcome)
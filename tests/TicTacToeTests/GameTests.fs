namespace TicTacToe.Tests.GameTests

open TicTacToe
open Types
open NUnit.Framework

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.GetComputerDifficultySetsEasyDifficulty() =
        let mockInput (surpress : bool) : Types.Option = '1'
        let mockOutput (message : string) : unit = ()

        let difficulty = Game.GetComputerDifficulty mockInput mockOutput 1

        Assert.AreEqual(Types.Easy, difficulty)

    [<Test>]
    member this.GetComputerDifficultySetsMediumDifficulty() =
        let mockInput (surpress : bool) : Types.Option = '2'
        let mockOutput (message : string) : unit = ()

        let difficulty = Game.GetComputerDifficulty mockInput mockOutput 1

        Assert.AreEqual(Types.Medium, difficulty)

    [<Test>]
    member this.GetComputerDifficultySetsHardDifficulty() =
        let mockInput (surpress : bool) : Types.Option = '3'
        let mockOutput (message : string) : unit = ()

        let difficulty = Game.GetComputerDifficulty mockInput mockOutput 1

        Assert.AreEqual(Types.Hard, difficulty)

    [<Test>]
    member this.SetupGameStateSetsUpComputerVsComputer() =
        let mockInput (surpress : bool) = '1'
        let mockOutput (message : string) = ()

        let gameState = Game.SetupGameState mockInput mockOutput

        Assert.AreEqual(Types.Computer, gameState.CurrentPlayer.PlayerType)
        Assert.AreEqual(Types.Computer, gameState.NextPlayer.PlayerType)

        Assert.AreEqual(Types.Easy, gameState.CurrentPlayer.Difficulty)
        Assert.AreEqual(Types.Easy, gameState.NextPlayer.Difficulty)

        Assert.AreEqual(Types.A, gameState.CurrentPlayer.Space)
        Assert.AreEqual(Types.B, gameState.NextPlayer.Space)

        CollectionAssert.AreEqual(Board.InitBoard 3, gameState.Board)
        Assert.AreEqual(Types.Playing, gameState.Result)


    [<Test>]
    member this.SetupGameStateSetsUpHumanVsComputer() =
        let mockInput (surpress : bool) = '2'
        let mockOutput (message : string) = ()

        let gameState = Game.SetupGameState mockInput mockOutput

        Assert.AreEqual(Types.Human, gameState.CurrentPlayer.PlayerType)
        Assert.AreEqual(Types.Computer, gameState.NextPlayer.PlayerType)

        Assert.AreEqual(Types.Easy, gameState.CurrentPlayer.Difficulty)
        Assert.AreEqual(Types.Medium, gameState.NextPlayer.Difficulty)

        Assert.AreEqual(Types.A, gameState.CurrentPlayer.Space)
        Assert.AreEqual(Types.B, gameState.NextPlayer.Space)

        CollectionAssert.AreEqual(Board.InitBoard 3, gameState.Board)
        Assert.AreEqual(Types.Playing, gameState.Result)

    [<Test>]
    member this.SetupGameStateSetsUpHumanVsHuman() =
        let mockInput (surpress : bool) = '3'
        let mockOutput (message : string) = ()

        let gameState = Game.SetupGameState mockInput mockOutput

        Assert.AreEqual(Types.Human, gameState.CurrentPlayer.PlayerType)
        Assert.AreEqual(Types.Human, gameState.NextPlayer.PlayerType)

        Assert.AreEqual(Types.Easy, gameState.CurrentPlayer.Difficulty)
        Assert.AreEqual(Types.Easy, gameState.NextPlayer.Difficulty)

        Assert.AreEqual(Types.A, gameState.CurrentPlayer.Space)
        Assert.AreEqual(Types.B, gameState.NextPlayer.Space)

        CollectionAssert.AreEqual(Board.InitBoard 3, gameState.Board)
        Assert.AreEqual(Types.Playing, gameState.Result)

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
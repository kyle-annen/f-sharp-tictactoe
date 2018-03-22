namespace TicTacToe.Tests.SetupTests

open TicTacToe
open Types
open NUnit.Framework

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.GetComputerDifficultySetsEasyDifficulty() =
        let mockInput (surpress : bool) : Option = '1'
        let mockOutput (message : string) : unit = ()

        let difficulty = Setup.GetComputerDifficulty mockInput mockOutput 1

        Assert.AreEqual(Types.Easy, difficulty)

    [<Test>]
    member this.GetComputerDifficultySetsMediumDifficulty() =
        let mockInput (surpress : bool) : Option = '2'
        let mockOutput (message : string) : unit = ()

        let difficulty = Setup.GetComputerDifficulty mockInput mockOutput 1

        Assert.AreEqual(Types.Medium, difficulty)

    [<Test>]
    member this.GetComputerDifficultySetsHardDifficulty() =
        let mockInput (surpress : bool) : Option = '3'
        let mockOutput (message : string) : unit = ()

        let difficulty = Setup.GetComputerDifficulty mockInput mockOutput 1

        Assert.AreEqual(Types.Hard, difficulty)

    [<Test>]
    member this.SetupGameStateSetsUpComputerVsComputer() =
        let mockInput (surpress : bool) = '1'
        let mockOutput (message : string) = ()

        let gameState = Setup.SetupGameState mockInput mockOutput

        Assert.AreEqual(Computer, gameState.CurrentPlayer.PlayerType)
        Assert.AreEqual(Computer, gameState.NextPlayer.PlayerType)

        Assert.AreEqual(Easy, gameState.CurrentPlayer.Difficulty)
        Assert.AreEqual(Easy, gameState.NextPlayer.Difficulty)

        Assert.AreEqual(A, gameState.CurrentPlayer.Space)
        Assert.AreEqual(B, gameState.NextPlayer.Space)

        CollectionAssert.AreEqual(Board.InitBoard 3, gameState.Board)
        Assert.AreEqual(Playing, gameState.Result)


    [<Test>]
    member this.SetupGameStateSetsUpHumanVsComputer() =
        let mockInput (surpress : bool) = '2'
        let mockOutput (message : string) = ()

        let gameState = Setup.SetupGameState mockInput mockOutput

        Assert.AreEqual(Human, gameState.CurrentPlayer.PlayerType)
        Assert.AreEqual(Computer, gameState.NextPlayer.PlayerType)

        Assert.AreEqual(Easy, gameState.CurrentPlayer.Difficulty)
        Assert.AreEqual(Medium, gameState.NextPlayer.Difficulty)

        Assert.AreEqual(A, gameState.CurrentPlayer.Space)
        Assert.AreEqual(B, gameState.NextPlayer.Space)

        CollectionAssert.AreEqual(Board.InitBoard 3, gameState.Board)
        Assert.AreEqual(Playing, gameState.Result)

    [<Test>]
    member this.SetupGameStateSetsUpHumanVsHuman() =
        let mockInput (surpress : bool) = '3'
        let mockOutput (message : string) = ()

        let gameState = Setup.SetupGameState mockInput mockOutput

        Assert.AreEqual(Human, gameState.CurrentPlayer.PlayerType)
        Assert.AreEqual(Human, gameState.NextPlayer.PlayerType)

        Assert.AreEqual(Easy, gameState.CurrentPlayer.Difficulty)
        Assert.AreEqual(Easy, gameState.NextPlayer.Difficulty)

        Assert.AreEqual(A, gameState.CurrentPlayer.Space)
        Assert.AreEqual(B, gameState.NextPlayer.Space)

        CollectionAssert.AreEqual(Board.InitBoard 3, gameState.Board)
        Assert.AreEqual(Playing, gameState.Result)

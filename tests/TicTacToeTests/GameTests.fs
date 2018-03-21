namespace TicTacToe.Tests.GameTests

open TicTacToe
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

    

namespace TicTacToe.Tests.GameStateTests

open NUnit.Framework
open TicTacToe
open TicTacToe.Types

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.SwapPlayersSwapsPlayers() =
        let initial = MockGameState.GetInitialGameState
        let actual = initial |> GameState.SwapPlayers

        Assert.AreEqual(actual.CurrentPlayer, initial.NextPlayer)

    [<Test>]
    member this.UpdateBoardUpdatesBoard() =
        let initialState = MockGameState.GetInitialGameState
        let newBoard : Board = [A; A; A; A; A; A; A; A; A;]

        let updatedState = GameState.UpdateBoard initialState newBoard

        CollectionAssert.AreEqual(updatedState.Board, newBoard)

    [<Test>]
    member this.ProgressGameStateProgressesTheGameState() =
        let initialGameState = MockGameState.GetInitialGameState
        let expectedBoard : Board = [A;Empty;Empty;Empty;Empty;Empty;Empty;Empty;Empty]
        let moveLocation = 1

        let nextGameState = GameState.ProgressGameState initialGameState moveLocation

        CollectionAssert.AreEqual(nextGameState.Board, expectedBoard)
        Assert.AreEqual(initialGameState.CurrentPlayer, nextGameState.NextPlayer)

    [<Test>]
    member this.ProgressGameStateProgressesTheGameStateWithWinResult() =
        let initialGameState =
            { MockGameState.GetInitialGameState with
                Board = [A;A;Empty;Empty;Empty;Empty;Empty;Empty;Empty] }
        let expectedBoard : Board = [A;A;A;Empty;Empty;Empty;Empty;Empty;Empty]
        let moveLocation = 3

        let nextGameState = GameState.ProgressGameState initialGameState moveLocation

        CollectionAssert.AreEqual(nextGameState.Board, expectedBoard)
        Assert.AreEqual(initialGameState.CurrentPlayer, nextGameState.NextPlayer)
        Assert.AreEqual(nextGameState.Result, Win)

    [<Test>]
    member this.GetMoveOptionsReturnsTheValidMoveOptions() =
        let initialGameState = MockGameState.GetInitialGameState
        let actual = GameState.GetMoveOptions initialGameState

        CollectionAssert.AreEqual(['1';'2';'3';'4';'5';'6';'7';'8';'9'], actual)
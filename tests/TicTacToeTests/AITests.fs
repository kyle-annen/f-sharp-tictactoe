namespace TicTacToe.Tests.AITests

open NUnit.Framework
open TicTacToe.AI
open TicTacToe.Board
open TicTacToe.GameState
open TicTacToe.MockGameState
open TicTacToe.Types

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.SwapCurrentSpaceAlternatesCurrentSpace() =
        let startState = GetStartState GetInitialGameState
        let actual = SwapCurrentSpace startState

        Assert.AreEqual(actual.CurrentSpace, B)
        let actual2 = SwapCurrentSpace actual
        Assert.AreEqual(actual2.CurrentSpace, A)

    [<Test>]
    member this.UpdateNegamaxBoardUpdatesTheNegamaxStateBoard() =
        let startState = GetStartState GetInitialGameState
        let newBoard =
            startState.IndexedBoard
            |> List.map (fun (_, v) -> v)
            |> PlaceMove 3 B
            |> List.indexed
        let updatedState = UpdateNegamaxBoard newBoard startState

        Assert.AreEqual(newBoard, updatedState.IndexedBoard)

    [<Test>]
    member this.IncreaseDepthIncreasesNegamaxStateDetph() =
        let startState = GetStartState GetInitialGameState
        let nextState = IncreaseDepth startState

        Assert.AreEqual(nextState.Depth, startState.Depth + 1)

    [<Test>]
    member this.ProgressStateProgressesTheNegamaxState() =
        let startState = GetStartState GetInitialGameState
        let newBoard =
            startState.IndexedBoard
            |> List.map (fun (_,v) -> v)
            |> PlaceMove 3 B
            |> List.indexed

        let nextState = ProgressState newBoard startState

        Assert.AreEqual(startState.CurrentSpace, A)
        Assert.AreEqual(nextState.CurrentSpace, B)

        Assert.AreEqual(startState.IndexedBoard.[2] |> snd, Empty)
        Assert.AreEqual(nextState.IndexedBoard.[2] |> snd, B)

        Assert.That(startState.Depth, Is.EqualTo(1))
        Assert.That(nextState.Depth, Is.EqualTo(2))

    [<Test>]
    member this.ScoreBoardScoresTheBoard() =
        let depth = 5
        let score = ScoreBoard depth

        Assert.That(score, Is.EqualTo(95))

    [<Test>]
    member this.FlipScoreFlipsTheScore() =
        let score = 99
        let result = FlipScore score

        Assert.That(result, Is.EqualTo(-99))

    [<Test>]
    member this.GetStartStateReturnsStartNegamaxState() =
        let state = GetStartState GetInitialGameState

        Assert.That(state.MaxSpace, Is.EqualTo(A))
        Assert.That(state.CurrentSpace, Is.EqualTo(A))
        Assert.That(state.Depth, Is.EqualTo(1))
        Assert.That(
            state.IndexedBoard,
            Is.EqualTo(List.indexed GetInitialGameState.Board))

    [<Test>]
    member this.GetScoreStrategyGetsTheScoreStrategy() =
        let state = GetStartState GetInitialGameState

        let actual = GetScoreStrategy state

        Assert.AreEqual(Max, actual)

        let state2 = SwapCurrentSpace state

        let actual2 = GetScoreStrategy state2

        Assert.AreEqual(Min, actual2)

    [<Test>]
    member this.MakeMoveIndexedBoard() =
        let state = GetStartState GetInitialGameState
        let result = MakeMoveIndexedBoard state 0

        Assert.AreEqual(result.[0], (0, A))

        let state2 = state |> ProgressState result
        let result2 = MakeMoveIndexedBoard state2 1

        Assert.AreEqual(result2.[0], (0, A))
        Assert.AreEqual(result2.[1], (1, B))


        let state3 = state2 |> ProgressState result2
        let result3 = MakeMoveIndexedBoard state3 2

        Assert.AreEqual(result3.[0], (0, A))
        Assert.AreEqual(result3.[1], (1, B))
        Assert.AreEqual(result3.[2], (2, A))


    [<Test>]
    member this.NegamaxWinsInAllCircumstances3x3() =

        let mutable failures : int = 0

        let assertOrRecur depth recurFn (gameState:GameState) =
            match (gameState.Result, gameState.CurrentPlayer.PlayerType, recurFn) with
            | (Win, _, _) -> recurFn depth Win gameState
            | (Tie, _, _) -> recurFn depth Tie gameState
            | (Playing, _, _) -> recurFn depth Playing (SwapPlayers gameState)

        let rec go (depth : int) (result:Result) (gameState:GameState) =

            match (result, gameState) with
            | (Win, _) when gameState.CurrentPlayer.PlayerType = Human ->
                failures <- (failures + 1)
            | (Win, _) -> ()
            | (Tie, _) -> ()
            | _ ->
                let p = gameState.CurrentPlayer
                match p.PlayerType with
                | Computer ->
                        let move = Minimax depth gameState
                        PlaceMove move p.Space gameState.Board
                        |> UpdateBoard gameState
                        |> assertOrRecur depth go
                | Human ->
                    GetOpenMoves gameState.Board
                    |> List.iter (
                        fun move ->
                            PlaceMove move p.Space gameState.Board
                            |> UpdateBoard gameState
                            |> assertOrRecur depth go)
        go 100 Playing GetInitialGameState

        Assert.That(failures < 1)

    [<Test>]
    member this.RandomMoveGetsARandomMove() =
        let initialGameState = GetInitialGameState
        let oneMoveBoard = PlaceMove 1 A initialGameState.Board
        let gameState = UpdateBoard initialGameState (oneMoveBoard)

        for _ in [1..100] do
            let move = RandomMove gameState

            Assert.That(move, Is.GreaterThan(1))
            Assert.That(move, Is.LessThan(10))
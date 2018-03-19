namespace TicTacToe.Tests.BoardTests

open NUnit.Framework
open TicTacToe.Board
open TicTacToe.Types

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.InitBoardInitializesAnEmptyBoard () =
        let board = InitBoard 3
        Assert.AreEqual(board.Length, 9)
        CollectionAssert.AreEqual( board, [Empty; Empty; Empty; Empty; Empty; Empty; Empty; Empty; Empty] )

        let board2 = InitBoard 2
        Assert.AreEqual(board2.Length, 4)

    [<Test>]
    member this.GetPrecedingReturnsBoardSpaceBeforeLocation() =
        let actual : Board = InitBoard 3 |> PrecedingSpaces 3

        CollectionAssert.AreEqual(actual, [Empty; Empty;])

    [<Test>]
    member this.GetPrecedingReturnsEmptyListOnFirstIndex() =
        let actual : Board = InitBoard 3 |> PrecedingSpaces 1

        CollectionAssert.AreEqual(actual, [])

    [<Test>]
    member this.GetFollowingReturnsBoardSpaceAfterLocation() =
        let actual : Board = InitBoard 3 |> FollowingSpaces 7

        CollectionAssert.AreEqual(actual, [Empty; Empty])

    [<Test>]
    member this.PlaceMovePlacesAMoveAtFirstLocation() =
        let board : Board = InitBoard 3 |> PlaceMove 1 A
        let expected : Board = [ A; Empty; Empty; Empty; Empty; Empty; Empty; Empty; Empty; ]

        CollectionAssert.AreEqual(board, expected)

    [<Test>]
    member this.PlaceMovePlacesAMoveAtLastLocation() =
        let board : Board = InitBoard 3 |> PlaceMove 9 B

        Assert.AreEqual(board.[8], B)

    [<Test>]
    member this.PlaceMovePlacesAMoveInMiddleOfBoard() =
        let board : Board = InitBoard 3 |> PlaceMove 5 B

        Assert.AreEqual(board.[4], B)

    [<Test>]
    member this.GetOpenMoveReturnsEmptyLocations() =
        let actual =
            InitBoard 3
            |> PlaceMove 1 A
            |> PlaceMove 2 B
            |> GetOpenMoves

        let expected = [3;4;5;6;7;8;9]

        CollectionAssert.AreEqual(actual, expected)

    [<Test>]
    member this.CheckListReturnsTrueIfAllValuesAreTheSame() =
        let actual = [A;A;A] |> CheckCombo

        Assert.AreEqual(actual, true)

    [<Test>]
    member this.CheckSeqReturnsFalseIfFirstValueIsEmpty() =
        let actual = [Empty;A;A] |> CheckCombo

        Assert.AreEqual(actual, false)

    [<Test>]
    member this.RowsReturnsListOfRows() =
        let actual : Board list =
            [ A; A; A; Empty; Empty; Empty; B; B; B ] |> Rows

        let expected : Board list =
            [ [ A; A; A ]; [ Empty; Empty; Empty ]; [ B; B; B ] ]

        CollectionAssert.AreEqual(actual, expected)

    [<Test>]
    member this.ColumsReturnsListOfColumns() =
        let actual : Board list =
            [ A; A; A; Empty; Empty; Empty; B; B; B ] |> Columns

        let expected : Board list =
            [ [ A; Empty; B ]; [ A; Empty; B ]; [ A; Empty; B ] ]

        CollectionAssert.AreEqual(actual, expected)

    [<Test>]
    member this.DiagonalsReturnsListOfDiagonals() =
        let actual : Board list =
            [ A; A; A; Empty; Empty; Empty; B; B; B ]
            |> Diagonals

        let expected : Board list =
            [ [ A; Empty; B ]; [ A; Empty; B ] ]

        CollectionAssert.AreEqual(actual, expected)

    [<Test>]
    member this.GetResultReturnsWinIfThereIsAWinner() =
        let result : Result =
            [A; A; A; Empty; Empty; Empty; Empty; Empty; Empty ]
            |> GetResult

        Assert.AreEqual(result, Win)

        let result2 : Result =
            [A; Empty; Empty; A; Empty; Empty; A; Empty; Empty ]
            |> GetResult

        Assert.AreEqual(result2, Win)

    [<Test>]
    member this.GetResultReturnsPlayingIfThereIsNoWinner() =
        let result : Result =
            [ A; Empty; A; Empty; A; Empty; Empty; Empty; Empty ]
            |> GetResult

        Assert.AreEqual(result, Playing)

    [<Test>]
    member this.GetResultReturnsPlayingIfThereIsNoWinnerAndOpenMoves() =
        let result : Result =
            [ Empty; Empty; Empty; Empty; Empty; Empty; Empty; Empty; Empty ]
            |> GetResult

        Assert.AreEqual(result, Playing)

    [<Test>]
    member this.GetResultReturnsFalseIfThereIsNoWinNoTie() =
        let result : Result =
            [ A; Empty; A; Empty; A; Empty; Empty; Empty; Empty ]
            |> GetResult

        Assert.AreEqual(result, Playing)

    [<Test>]
    member this.GetResultReturnsTieIfThereIsNoWinnerNoEmpty() =
        let result : Result =
            [ A; B; A; B; A; B; B; A; B ]
            |> GetResult

        Assert.AreEqual(result, Tie)

    [<Test>]
    member this.GetResultReturnsWinIfThereIsAWinnerNoEmpty() =
        let result : Result =
            [ A; A; A; B; A; B; B; B; B ]
            |> GetResult

        Assert.AreEqual(result, Win)

    [<Test>]
    member this.GetResultReturnsTieIfNoWinAndNoTie() =
        let result : Result =
            [A; B; A; Empty; Empty; Empty; Empty; Empty; Empty ]
            |> GetResult

        Assert.AreEqual(result, Playing)

    [<Test>]
    member this.GetResultReturnsTieIfNoWinAndTie() =
        let result : Result =
            [ A; B; A; B; A; B; B; A; B ]
            |> GetResult

        Assert.AreEqual(result, Tie)
module TicTacToe.Tests.UITests

open NUnit.Framework
open TicTacToe
open TicTacToe

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.ClearScreenDoesNotClearScreenIfOutputDiffersFromConsoleRender() =
        let mockOutput (message : string) = ()
        Assert.AreEqual(false, UI.ClearScreen mockOutput)

    [<Test>]
    member this.FormatBoardFormatsBoardBoard() =
        let expected =
            "           1 ┃ 2 ┃ 3 \n" +
            "          ━━━╋━━━╋━━━\n" +
            "           4 ┃ 5 ┃ 6 \n" +
            "          ━━━╋━━━╋━━━\n" +
            "           7 ┃ 8 ┃ 9 "

        let actual = UI.FormatBoard UI.Template3x3 MockGameState.GetInitialGameState

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayDifficultyPromptDisplaysPrompt() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        UI.DisplayDifficultyPrompt mockOutput 1

        let expected =
            [Dialog.Greeting;
                Dialog.NewLine;
                Dialog.SelectDifficulty + "1";
                Dialog.NewLine;
                Dialog.DifficultyOptions;]
            |> String.concat ""

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayGameTypePromptDisplaysTheGameTypePrompt() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        UI.DisplayGameTypePrompt mockOutput

        let expected =
            [Dialog.SelectGameType;
                Dialog.NewLine;
                Dialog.GameOptions;]
            |> String.concat ""

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayTurnPromptDisplaysTheTurnPrompltForHuman() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        let gameState = MockGameState.GetInitialGameState

        UI.DisplayTurnPrompt mockOutput gameState

        let expected =
            [Dialog.VerticalPadding;
                Dialog.Player1Announce;
                Dialog.MoveEntryPrompt;]
            |> String.concat ""

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayTurnPromptDisplaysTheTurnPromptForComputer() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        let gameState =
            MockGameState.GetInitialGameState
            |> GameState.SwapPlayers

        UI.DisplayTurnPrompt mockOutput gameState

        let expected =
            [Dialog.VerticalPadding;
                Dialog.ComputerAnnounce;
                Dialog.ComputerThinking;]
            |> String.concat ""

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayBoardDisplaysTheBoard() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        let gameState = MockGameState.GetInitialGameState

        UI.DisplayBoard mockOutput gameState

        let expected =
            "           1 ┃ 2 ┃ 3 \n" +
            "          ━━━╋━━━╋━━━\n" +
            "           4 ┃ 5 ┃ 6 \n" +
            "          ━━━╋━━━╋━━━\n" +
            "           7 ┃ 8 ┃ 9 "

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayHeadingDisplaysTheHeading() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        UI.DisplayHeading mockOutput

        let expected =
            [Dialog.VerticalPadding;
                Dialog.Greeting;
                Dialog.VerticalPadding;]
            |> String.concat ""

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayMessageWithPaddingDisplaysTheMessageWithPadding() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        UI.DisplayMessageWithPadding mockOutput Dialog.Player2Win

        let expected =
            [Dialog.VerticalPadding;
                Dialog.Player2Win;
                Dialog.VerticalPadding;]
            |> String.concat ""

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayUIDisplaysTheGameMessages() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        let gameState = MockGameState.GetInitialGameState

        UI.DisplayUI mockOutput gameState

        let expected =
            "\n\n\n\nLet's play TicTacToe!\n\n\n\n" +
            "           1 ┃ 2 ┃ 3 \n" +
            "          ━━━╋━━━╋━━━\n" +
            "           4 ┃ 5 ┃ 6 \n" +
            "          ━━━╋━━━╋━━━\n" +
            "           7 ┃ 8 ┃ 9 " +
            "\n\n\n\nPlayer 1 it is your turn.Enter the number of an open space."

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayGreetingDisplaysTheGreeting() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        UI.DisplayGreeting mockOutput

        let expected = Dialog.Greeting

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.DisplayContinuteMessageDisplaysTheContinueMessage() =
        let mutable actual = ""
        let mockOutput (message : string) = actual <- (actual + message)

        UI.DisplayContinueMessage mockOutput

        let expected = Dialog.ContinuePlaying

        Assert.AreEqual(expected, actual)
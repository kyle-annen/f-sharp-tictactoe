module TicTacToe.Tests.LoggerTests

open NUnit.Framework
open TicTacToe

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.LoggerLogsInfoLevel() =
        let mutable actual : string =""
        let message = "test message"
        let mockPrint string = actual <- string

        Logger.Log Types.Info mockPrint message

        Assert.That(actual, Is.EqualTo("F# TicTacToe [Info]: test message"))

    [<Test>]
    member this.LoggerLogsDebugLevel() =
        let mutable actual : string =""
        let message = "test message"
        let mockPrint string = actual <- string

        Logger.Log Types.Debug mockPrint message

        Assert.That(actual, Is.EqualTo("F# TicTacToe [Debug]: test message"))

    [<Test>]
    member this.LoggerLogsDangerLevel() =
        let mutable actual : string =""
        let message = "test message"
        let mockPrint string = actual <- string

        Logger.Log Types.Danger mockPrint message

        Assert.That(actual, Is.EqualTo("F# TicTacToe [Danger]: test message"))

    [<Test>]
    member this.LoggerLogsGameLevel() =
        let mutable actual : string =""
        let message = "test message"
        let mockPrint string = actual <- string

        Logger.Log Types.Game mockPrint message

        Assert.That(actual, Is.EqualTo("test message"))
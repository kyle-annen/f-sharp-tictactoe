namespace TicTacToe.Tests.InputTests

open NUnit.Framework
open TicTacToe
open Types

[<TestFixture>]
type TestClass() =

    [<Test>]
    member this.CheckInputReturnsFalesIfInputInvalid() =
        let options : Options = ['y';'n']
        let option : Option = '5'

        let actual = Input.CheckInput option options

        Assert.AreEqual(false, actual)

    [<Test>]
    member this.CheckInputReturnsTrueIfInputValid() =
        let options : Options = ['y';'n']
        let option : Option = 'y'

        let actual = Input.CheckInput option options

        Assert.AreEqual(true, actual)

    [<Test>]
    member this.GetInputGetsTheInput() =
        let mutable inputCount = 0
        let validOptions : Options = ['1']

        let mockInput (surpressCursor : bool) : Option =
            inputCount <- (inputCount + 1)
            match inputCount with
            | 1 -> '1'
            | _ -> '2'

        let actual = Input.GetInput mockInput validOptions

        Assert.AreEqual('1', actual)

    [<Test>]
    member this.GetInputGetsTheInputAfterBadInput() =
        let mutable inputCount = 0
        let validOptions : Options = ['2']

        let mockInput (surpressCursor : bool) : Option =
            inputCount <- (inputCount + 1)
            match inputCount with
            | _ when inputCount < 10 -> '1'
            | _ -> '2'

        let actual = Input.GetInput mockInput validOptions

        Assert.AreEqual('2', actual)
        Assert.AreEqual(10, inputCount)

    [<Test>]
    member this.GetHumanMoveGetsHumanMove() =
        let mutable inputCount = 0
        let gameState = MockGameState.GetInitialGameState

        let mockInput (surpressCursor : bool) : Option =
            inputCount <- (inputCount + 1)
            match inputCount with
            | _ when inputCount < 10 -> '0'
            | _ -> '2'

        let actual : Move = Input.GetHumanMove mockInput gameState

        Assert.AreEqual(2, actual)
        Assert.AreEqual(10, inputCount)

    [<Test>]
    member this.GetYesOrNoGetsYes() =
        let mutable inputCount = 0

        let mockInput (surpressCursor : bool) : Option =
            inputCount <- (inputCount + 1)
            match inputCount with
            | _ when inputCount < 10 -> '0'
            | _ -> 'y'

        let actual : Response = Input.GetYesOrNo mockInput

        Assert.AreEqual(Yes, actual)
        Assert.AreEqual(10, inputCount)

    [<Test>]
    member this.GetYesOrNoGetsNo() =
        let mutable inputCount = 0

        let mockInput (surpressCursor : bool) : Option =
            inputCount <- (inputCount + 1)
            match inputCount with
            | _ when inputCount < 10 -> '0'
            | _ -> 'n'

        let actual : Response = Input.GetYesOrNo mockInput

        Assert.AreEqual(No, actual)
        Assert.AreEqual(10, inputCount)

    [<Test>]
    member this.GetGameVersionGetsTheGameVersion() =
        let mutable inputCount = 0

        let mockInput (surpressCursor : bool) : Option =
            inputCount <- (inputCount + 1)
            match inputCount with
            | _ when inputCount < 10 -> '0'
            | 10 -> '1'
            | 11 -> '2'
            | _ -> '3'

        let actual : GameVersion = Input.GetGameVersion mockInput

        Assert.AreEqual(ComputerVsComputer, actual)
        Assert.AreEqual(10, inputCount)

        let actual2 : GameVersion = Input.GetGameVersion mockInput

        Assert.AreEqual(HumanVsComputer, actual2)
        Assert.AreEqual(11, inputCount)

        let actual3 : GameVersion = Input.GetGameVersion mockInput

        Assert.AreEqual(HumanVsHuman, actual3)
        Assert.AreEqual(12, inputCount)

    [<Test>]
    member this.GetDifficultyGetsTheDifficulty() =
        let mutable inputCount = 0

        let mockInput (surpressCursor : bool) =
            inputCount <- (inputCount + 1)
            match inputCount with
            | 1 -> '1'
            | 2 -> '2'
            | _ -> '3'

        let actual1 = Input.GetDifficulty mockInput
        let actual2 = Input.GetDifficulty mockInput
        let actual3 = Input.GetDifficulty mockInput

        Assert.AreEqual(Easy, actual1)
        Assert.AreEqual(Medium, actual2)
        Assert.AreEqual(Hard, actual3)

    [<Test>]
    member this.GetMoveGetsTheHumanMove() =
        let mockInput (surpressCursor : bool) = '1'
        let mockOutput (message : string) = ()

        let move = Input.GetMove mockInput MockGameState.GetInitialGameState

        Assert.AreEqual(1, move)

    [<Test>]
    member this.GetMoveWontAcceptMoveSmallerThanBoard() =
        let mutable inputCount = 0
        let mockInput (surpressCursor : bool) =
            inputCount <- (inputCount + 1 )
            match inputCount with
            | 1 -> '0'
            | _ -> '1'

        let mockOutput (message : string) = ()

        let move = Input.GetMove mockInput MockGameState.GetInitialGameState

        Assert.AreEqual(1, move)

    [<Test>]
    member this.GetMoveWontAcceptMoveLargerThanBoard() =
        let mutable inputCount = 0
        let mockInput (surpressCursor : bool) =
            inputCount <- (inputCount + 1 )
            match inputCount with
            | 1 -> 'A'
            | _ -> '1'

        let mockOutput (message : string) = ()

        let move = Input.GetMove mockInput MockGameState.GetInitialGameState

        Assert.AreEqual(1, move)
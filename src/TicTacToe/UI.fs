module TicTacToe.UI

open Types
open Logger

let xToken = "X"

let oToken = "O"

let private template3x3 = 
    Printf.StringFormat<'a>" %s | %s | %s \n---+---+---\n %s | %s | %s \n---+---+---\n %s | %s | %s "

let ConsoleRenderer = Log LogLevel.Game (printf "%s")

let RenderBoard outputFn (gameState : GameState) = 
    let board = 
        gameState.Board
        |> List.mapi (
            fun i space -> 
                match space with 
                | A -> xToken
                | B -> oToken
                | _ -> sprintf "%i" (i + 1))
    match board with 
    | [a;b;c;d;e;f;g;h;i] -> sprintf template3x3 a b c d e f g h i
    | _ -> ""
    |> outputFn

let RenderMessage outputFn (message : string) = outputFn message 

module Dialog = 
    let English = {
        Greeting = "Let's play TicTacToe!";
        SelectLang = "Please select a language.";
        SelectPlayerType = "Choose computer or human player.";
        SelectDifficulty = "Select the computer difficulty.";
        PTypeHuman = "1 - Human";
        PTypeComputer = "2 - Computer";
        PickBoardSize = "Select the board size.";
        TurnPrompt = "It's your turn.";
        PlayerAnnounce = "Player ";
        GameOver = "Game Over";
        Win = "You have won the game!";
        Tie = "The game is a tie.";
        InvalidPlay = "That selection is invalid.";
        InputPrompt = "Please input the number of an open space.";
        ContinuePlaying = "Do you want to play again? Input [y] for yes, [n] to exit";
        Easy = "easy";
        Medium = "medium";
        Hard = "hard";
    }
    let Mandarin = {
        Greeting = "我们玩TicTacToe！";
        SelectLang = "请选择语言。";
        SelectPlayerType = "选择电脑或人手。";
        SelectDifficulty = "选择电脑难度。";
        PTypeHuman = "1 - 人的";
        PTypeComputer = "2 - 电脑";
        PickBoardSize = "选择板的大小。";
        TurnPrompt = "现在轮到你了";
        PlayerAnnounce = "播放机 ";
        GameOver = "游戏结束";
        Win = "你赢了游戏！";
        Tie = "游戏是一个领带。";
        InvalidPlay = "该选择无效。";
        InputPrompt = "请输入一个开放空间的数量。";
        ContinuePlaying = "你想再玩吗？输入[y] 为是，[n] 退出。";
        Easy = "傻";
        Medium = "中等";
        Hard = "难以";
    } 
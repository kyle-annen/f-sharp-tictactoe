# F-Sharp TicTacToe

## Setup and suggested tools

Installing F# is straight forward as it is included in Mono.
```bash
brew install mono
```

In terms of IDEs, Visual Studio Mac, Visual Studio Code, and JetBrains Rider are all good.

I prefer Visual Studio Code with the Ionide packages installed, as the type annotations and introspection are top notch.

Visual Studio Code can be installed with brew casks.

```bash
brew cask install visual-studio-code
```

Ionid-fsharp can be installed from the Visual Studio Code command palette.

Launch Command Pallete `⌘ + p`
```bash
ext install Ionide-fsharp
```

## Playing the game

To play the TicTacToe, from the home directory run:
```bash
./play.sh
```

## Testing

NUnit is used for testing. From the root directory of the project run:

```bash
./test.sh
```

## Dependencies

There are five dependencies, all used for test driven development, four for NUnit, and one test watcher.

## Functional Vs Object Oriented

As with Scala and Ocaml, F# can be written with classes or without, and can have mutation. I have chosen to only use mutation in testing in order to mock user input.

F# is also similar to Scala in that it has seamless interop with the other dotnet languages, which allows for a very mature and robust API ecosystem.
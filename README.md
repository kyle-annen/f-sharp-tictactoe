# F-Sharp TicTacToe

## Setup and suggested tools

Installing F# is straight forward as it is included in Mono. Assuming `brew` is installed:

```bash
brew install mono
```

Dotnet CLI is used for building, dependency management, and for testing. It can be downloaded with Brew/Casks.

```bash
brew update
brew tap caskroom/cask
brew cask install dotnet-sdk
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

As with Scala and Ocaml, F# can be written with classes or without, and can have mutation.

I have chosen to only use mutation in testing for mocks. Types and Functions are favored over classes, with name spacing provided by modules.

Where ever possible pattern matching and pipes are favored over if/else blocks, and partial application is kept to a minimum to help convey intent.
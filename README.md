# Simple Fable Python demo

Demonstration for https://github.com/fable-compiler/Fable.Python, showing how to start an F# project with Fable and run Python code

## Prerequisites

1. Install Fable (which should also install dotnet/.NET)

## Writing

Start with `Program.fs`, the main code, and the python code to call (`module.py`)

Make an `fp.fsproj` like in the repo or use something like `dotnet new console --language F#` to generate it

Those three files are you need. Run `fable-py` to compile the F# code into a python file called `program.py`. Then run `python program.py` to run the code

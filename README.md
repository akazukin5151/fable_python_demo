# Simple Fable Python demo

Demonstration for https://github.com/fable-compiler/Fable.Python, showing how to start an F# project with Fable and run Python code

## Prerequisites

Install [Fable](https://fable.io/index.html) (which should also install dotnet/.NET)

## Writing

Start with `Program.fs`, the main code, and the python code to call (`module.py`)

Make an `fp.fsproj` like in the repo or use something like `dotnet new console --language F#` to generate it

Those three files are you need. Run `fable-py` to compile the F# code into a python file called `program.py`. Then run `python program.py` to run the code

## Why would I want to do this?

Basically, Javascript has Typescript, but Python has nothing. Mypy isn't a solution because it is just a static type checker and not a separate language. I think the next best alternative are transpilers to/from statically typed and preferably functional languages. Languages that meet the criteria are:

- Haskell
- Purescript
- OCaml
- F#

Haskell is too tightly coupled to GHC to make a transpiler. Purescript is good but [the purescript-python project is barely maintained](https://github.com/purescript-python/purescript-python/issues/37). OCaml's ecosystem looked pretty dead to me, to be honest. F#, like Purescript, can transpile to Python via Fable, and there are no bugs.

### What's wrong with Python?

- Dynamic typing is frustrating
- A compiler is far better than any linter (flake8) or retrofitted static type checker (mypy) or runtime type checker (pydantic)
- The most widely used dynamic programming languages have eventually retrofitted some static analysis
    - Python: mypy and type hints
    - Javascript: Typescript, Purescript (, Elm)
    - Ruby: sorbet
- Many unit tests are needed because of the lack of static guarantees. Dynamic typing shifts the burden of static analysis by the compiler to poorly written unit tests by humans.
- Haskell libraries might lack extensive documentation compared to Python, but it doesn't matter when you can click on the types and figure out how to construct them.
    - For example [pandas.DataFrame.apply](https://pandas.pydata.org/pandas-docs/stable/reference/api/pandas.DataFrame.apply.html) accepts a function, but does not say how many arguments that function should have, and what it should return. If multiple kind of functions are accepted, there should be multiple different `apply` functions. If this was Haskell, a type signature of, say `DataFrame a -> (a -> b) -> DataFrame b`, would let us know immediately what the function needs to be: accept a dtype of the DataFrame and return something else. Reducing (fold) would look like `DataFrame a -> (a -> b -> b) -> DataFrame a`
- Package management. It is essentially impossible to package consistently. Every module can take a different method. One time, pacman-mirrors suddenly stopped working because it was installed in `/usr/lib/python3.10` not `/usr/lib/python3.9`. It takes dynamic linking to its extreme - nothing matters until you actually run it.
    - Yes, dynamic linking in C is also messed up. How many times did I have to symlink a lib in `/usr/lib/` from `x.so` to `x.so.5` or `x.so.6`? Just use static linking, people.

### But I am too dependant on Python!

And that's why you can use a transpiler that enables interop instead of committing to using another language immediately.

### F# is too reliant on dotnet

I agree, but that's better than the wild west of dynamic typing. At least compiling F# to Python means this one is reliant on Python instead? Not sure if that's an improvement...

### Why not just commit to using another language?

There's a reason why some people are using Python even when they don't like it - the ecosystem. Personally I like how matplotlib charts look more than the alternatives (Python or not), but the API is horrible. There has to be a way to plot matplotlib-like charts with static guarantees.

Talking about matplotlib, it's also because [FSharp Charting](https://github.com/fslaborg/FSharp.Charting) is Windows-first, and the UNIX alternative [XPlot](https://fslab.org/XPlot/) uses plotly and Google Charts (ew). Come on, I just want to get a simple png, and I'm sure a lot of non-CS academics agree.

### Why not use X language?

- Julia: still dynamic, slow compile time/time-to-first-plot. Despite being a dynamic, interpreted language, Julia can only run compiled code.
- R: not really a general purpose programming language
- C: quick, what does `char (*(*x())[5])()` mean?
- C++: https://en.wikipedia.org/wiki/Most_vexing_parse
- PHP: https://news-web.php.net/php.internals/70691
- Javascript: actually not too bad, especially if you use typescript, or even better, purescript
- Rust: not functional enough, lifetimes more complicated than category theory (not really rust's fault though, manual memory management of any kind is (too) hard)

### Biggest problem with transpiling?

The point of functional programming is that you don't have to worry about performance, just write in a declarative way and the compiler will optimize the code. This works for Haskell, OCaml and F#, which compiles to binaries. However, Purescript compiles to Javascript (and even worse, aims to be readable), losing the advantage of having an optimized, compiled binary. Transpiling F# to Python has the same problem.

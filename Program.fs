module Main

open Fable.Core
open Fable.Core.PyInterop

// from module import hi
[<ImportMember("module")>]
let hi () = nativeOnly

type IAlert =
    abstract triggerAlert: message:string -> unit
    abstract someString: string

// Apparently this converts all camelcases from F# to snakecase in python
// import module
[<ImportAll("module")>]
let lib: IAlert = nativeOnly

[<EntryPoint>]
let main argv =
    hi()
    lib.triggerAlert ("Hi from F# > " + lib.someString)
    let v = Some(99)
    let n = None
    match v with
     | Some i -> printfn "%d"i
     | None -> printfn "impossible"
    match n with
     | Some i -> printfn "%d"i
     | None -> printfn "yes"
    0

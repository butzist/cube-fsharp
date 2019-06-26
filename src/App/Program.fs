// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    argv
    |> Array.iter CubeModel.Say.hello

    0 // return an integer exit code

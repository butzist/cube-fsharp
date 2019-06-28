// Learn more about F# at http://fsharp.org
open System
open CubeModel

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let cube = Cube.Cube 3
    cube.RotateUp 2 1
    cube.Format |> Seq.iter Console.WriteLine
    0 // return an integer exit code

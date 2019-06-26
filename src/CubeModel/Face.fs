namespace CubeModel

module Face =
    type Face(dim : int, colors : Colors.Color [,]) =
        member this.Dim = dim
        member this.Colors = colors
        member this.Get(y, x) = colors.[y, x]
        member this.Set(y, x, color) = colors.[y, x] <- color
        member this.GetRow(y) = colors.[y, *]
        member this.SetRow(y : int, newColors : Colors.Color []) =
            newColors |> Seq.iteri (fun i c -> colors.[y, i] <- c)
        member this.GetCol(x) = colors.[*, x]
        member this.SetCol(x : int, newColors : Colors.Color []) =
            newColors |> Seq.iteri (fun i c -> colors.[i, x] <- c)

        member this.Rotate(n) =
            let rec loop n =
                match n with
                | 0 -> ()
                | _ ->
                    colors
                    |> Array2D.mapi (fun y x _ -> colors.[dim - 1 - x, y])
                    |> (fun self -> Array2D.blit self 0 0 colors 0 0 dim dim)
                    loop (n - 1)
            loop n

        static member Single(dim : int, color : Colors.Color) =
            let colors = Array2D.create dim dim color
            Face(dim, colors)

        member this.Clone = Face(this.Dim, this.Colors)

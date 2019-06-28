namespace CubeModel

open Face
open Colors

module Cube =
    type Cube(dim : int) =
        let up = Face.Single dim White
        let down = Face.Single dim Yellow
        let front = Face.Single dim Green
        let back = Face.Single dim Blue
        let left = Face.Single dim Orange
        let right = Face.Single dim Red

        member this.RotateLeft (layer : int) (n : int) =
            match layer with
            | 0 -> left.Rotate(n) // clock-wise
            | n when n = (dim - 1) -> right.Rotate(4 - n) // anti-clock-wise
            | _ -> ()
            let rec rotateSlice n =
                match n with
                | 0 -> ()
                | _ ->
                    let slice = front.GetCol layer
                    up.GetCol layer |> front.SetCol layer
                    back.GetCol(dim - 1 - layer)
                    |> Array.rev
                    |> up.SetCol layer
                    down.GetCol layer
                    |> Array.rev
                    |> back.SetCol(dim - 1 - layer)
                    slice |> down.SetCol layer
                    rotateSlice (n - 1)
            rotateSlice n

        member this.RotateFront (layer : int) (n : int) =
            match layer with
            | 0 -> front.Rotate(n) // clock-wise
            | n when n = (dim - 1) -> back.Rotate(4 - n) // anti-clock-wise
            | _ -> ()
            let rec rotateSlice n =
                match n with
                | 0 -> ()
                | _ ->
                    let slice = up.GetRow(dim - 1 - layer)
                    left.GetCol(dim - 1 - layer)
                    |> Array.rev
                    |> up.SetRow(dim - 1 - layer)
                    down.GetRow layer |> left.SetCol(dim - 1 - layer)
                    right.GetCol layer
                    |> Array.rev
                    |> down.SetRow layer
                    slice |> right.SetCol layer
                    rotateSlice (n - 1)
            rotateSlice n

        member this.RotateUp (layer : int) (n : int) =
            match layer with
            | 0 -> up.Rotate(n) // clock-wise
            | n when n = (dim - 1) -> down.Rotate(4 - n) // anti-clock-wise
            | _ -> ()
            let rec rotateSlice n =
                match n with
                | 0 -> ()
                | _ ->
                    let slice = front.GetRow layer
                    right.GetRow layer |> front.SetRow layer
                    back.GetRow layer |> right.SetRow layer
                    left.GetRow layer |> back.SetRow layer
                    slice |> left.SetRow layer
                    rotateSlice (n - 1)
            rotateSlice n

        member this.Serialize =
            up.Serialize + down.Serialize + front.Serialize + back.Serialize
            + left.Serialize + right.Serialize
        member this.GetHashCode = this.Serialize.GetHashCode()
        member this.Format =
            let makerow a =
                a
                |> Seq.cast<Colors.Color>
                |> Seq.map (Colors.serialize >> string)
                |> String.concat ""

            let makeblock getter =
                [ 0..(dim - 1) ]
                |> Seq.map
                       (fun i ->
                       String.replicate dim " " + (getter i |> makerow)
                       + String.replicate dim " ")
            Seq.concat
                [ makeblock up.GetRow
                  [ 0..(dim - 1) ]
                  |> Seq.map (fun i ->
                         ([ left.GetRow i
                            front.GetRow i
                            right.GetRow i ]
                          |> Seq.map makerow
                          |> String.concat ""))
                  makeblock down.GetRow
                  makeblock (fun i -> Array.rev (back.GetRow(dim - 1 - i))) ]

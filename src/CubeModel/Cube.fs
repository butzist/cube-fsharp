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
            let slice = front.GetCol layer |> Array.copy
            up.GetCol layer |> front.SetCol layer
            back.GetCol(dim - 1 - layer)
            |> Array.rev
            |> up.SetCol layer
            down.GetCol layer
            |> Array.rev
            |> back.SetCol(dim - 1 - layer)
            slice |> down.SetCol layer

        member this.RotateFront (layer : int) (n : int) =
            match layer with
            | 0 -> front.Rotate(n) // clock-wise
            | n when n = (dim - 1) -> back.Rotate(4 - n) // anti-clock-wise
            | _ -> ()
            let slice = up.GetRow(dim - 1 - layer) |> Array.copy
            left.GetCol(dim - 1 - layer)
            |> Array.rev
            |> up.SetRow(dim - 1 - layer)
            down.GetRow layer |> left.SetCol(dim - 1 - layer)
            right.GetCol layer
            |> Array.rev
            |> down.SetRow layer
            slice |> right.SetCol layer

        member this.RotateUp (layer : int) (n : int) =
            match layer with
            | 0 -> up.Rotate(n) // clock-wise
            | n when n = (dim - 1) -> down.Rotate(4 - n) // anti-clock-wise
            | _ -> ()
            let slice = front.GetRow layer |> Array.copy
            left.GetRow layer |> front.SetRow layer
            back.GetRow layer |> left.SetRow layer
            right.GetRow layer |> back.SetRow layer
            slice |> right.SetRow layer

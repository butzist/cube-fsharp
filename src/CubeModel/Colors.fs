namespace CubeModel

module Colors =
    type Color =
        | White
        | Yellow
        | Green
        | Red
        | Orange
        | Blue

    let serialize color =
        match color with
        | White -> 'w'
        | Yellow -> 'y'
        | Green -> 'g'
        | Red -> 'r'
        | Orange -> 'o'
        | Blue -> 'b'

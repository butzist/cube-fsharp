namespace Tests

open NUnit.Framework
open CubeModel.Face
open CubeModel.Colors

[<TestFixture>]
type TestFace() =

    [<SetUp>]
    member this.Setup() = ()

    [<Test>]
    member this.TestSingleColorWhite() =
        let face = Face.Single 2 White
        Assert.That(face.Dim, Is.EqualTo(2))
        Assert.That(face.Colors, Is.All.EqualTo(White))

    [<Test>]
    member this.TestSingleColorRed() =
        let face = Face.Single 3 Red
        Assert.That(face.Dim, Is.EqualTo(3))
        Assert.That(face.Colors, Is.All.EqualTo(Red))

    [<Test>]
    member this.TestSetGetSingle() =
        let face = Face.Single 2 White
        face.Set 1 1 Green
        Assert.That(face.Get 0 0, Is.EqualTo(White))
        Assert.That(face.Get 0 1, Is.EqualTo(White))
        Assert.That(face.Get 1 0, Is.EqualTo(White))
        Assert.That(face.Get 1 1, Is.EqualTo(Green))

    [<Test>]
    member this.TestGetRowCol() =
        let face = Face.Single 2 White
        face.Set 0 1 Green
        Assert.That(face.GetRow 0, Is.Not.All.EqualTo(White))
        Assert.That(face.GetRow 1, Is.All.EqualTo(White))
        Assert.That(face.GetCol 0, Is.All.EqualTo(White))
        Assert.That(face.GetCol 1, Is.Not.All.EqualTo(White))

    [<Test>]
    member this.TestSetRowCol() =
        let face = Face.Single 2 White
        face.SetRow 0 [| Red; Red |]
        Assert.That(face.Colors, Is.Not.All.EqualTo(Red))
        Assert.That(face.Colors, Is.Not.All.EqualTo(White))
        face.SetCol 1 [| Red; Red |]
        Assert.That(face.Colors, Is.Not.All.EqualTo(Red))
        face.SetCol 0 [| Red; Red |]
        Assert.That(face.Colors, Is.All.EqualTo(Red))

    [<Test>]
    member this.TestRotate1() =
        let face =
            Face(2,
                 array2D [ [ Red; Green ]
                           [ Blue; Orange ] ])
        face.Rotate 1
        Assert.That(face.Colors,
                    Is.EqualTo(array2D [ [ Blue; Red ]
                                         [ Orange; Green ] ]))

    [<Test>]
    member this.TestRotate2() =
        let face =
            Face(2,
                 array2D [ [ Red; Green ]
                           [ Blue; Orange ] ])
        face.Rotate 2
        Assert.That(face.Colors,
                    Is.EqualTo(array2D [ [ Orange; Blue ]
                                         [ Green; Red ] ]))

    [<Test>]
    member this.TestRotate3() =
        let face =
            Face(2,
                 array2D [ [ Red; Green ]
                           [ Blue; Orange ] ])
        face.Rotate 3
        Assert.That(face.Colors,
                    Is.EqualTo(array2D [ [ Green; Orange ]
                                         [ Red; Blue ] ]))

    [<Test>]
    member this.TestRotate3x3() =
        let face = Face.Single 3 White
        face.Set 0 0 Red
        face.Set 1 1 Yellow
        face.Rotate 1
        Assert.That(face.Get 0 0, Is.EqualTo(White))
        Assert.That(face.Get 0 2, Is.EqualTo(Red))
        Assert.That(face.Get 1 1, Is.EqualTo(Yellow))

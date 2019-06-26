namespace Tests

open NUnit.Framework

[<TestFixture>]
type TestClass () =

    [<SetUp>]
    member this.Setup () =
        ()

    [<Test>]
    member this.Test1 () =
        Assert.Pass()

    [<Test>]
    member this.TestMethodPassing() =
        Assert.True(true)

    [<Test>]
     member this.FailEveryTime() = Assert.True(false)

    [<Test>]
    member this.TestEqual() =
        Assert.That(42, Is.EqualTo(42))
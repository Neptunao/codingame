namespace ThereIsNoSpoon.Tests.Tests

open System
module ``There Is No Spoon Part 1`` =
    open NUnit.Framework
    open FsUnit
    open ThereIsNoSpoon

    [<Test>]
    let ``Getting valid result with simple input`` () =
        let grid = [|
            [| Cell(Some{x=0; y=0}); Cell(None) |];
            [| Cell(Some{x=0; y=1}); Cell(Some{x=1; y=1}) |];
        |]
        let res = findPoints grid
        res |> should equal 23

    [<Test>]
    let ``Getting valid result with islands`` () =
        let grid = [|
            [| Cell(Some{x=0; y=0}); Cell(Some{x=1; y=0}); |];
            [| Cell(Some{x=0; y=1}); Cell(None) |];
        |]
        let res =
            findPoints grid |>
            Array.map (fun p -> sprintf "%s %s %s" (cellToStr p.self) (cellToStr p.bottom)  (cellToStr p.right)) |>
            String.concat " "
        res |> should equal 23
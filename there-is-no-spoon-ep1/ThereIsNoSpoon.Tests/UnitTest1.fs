namespace ThereIsNoSpoon.Tests.Tests

open System

module ``There Is No Spoon Part 1`` =
    open NUnit.Framework
    open FsUnit
    open ThereIsNoSpoon

    let findPointsStr grid =
        findPoints grid |>
        Array.map (fun p -> sprintf "%s %s %s" (cellToStr p.self) (cellToStr p.right) (cellToStr p.bottom)) |>
        String.concat " "

    [<Test>]
    let ``Getting valid result with simple example`` () =
        let grid = [|
            [| Cell(Some{x=0; y=0}); Cell(Some{x=1; y=0}); |];
            [| Cell(Some{x=0; y=1}); Cell(None) |];
        |]
        findPointsStr grid |> should equal "0 0 1 0 0 1 0 1 -1 -1 -1 -1 1 0 -1 -1 -1 -1"

    [<Test>]
    let ``Getting valid result with nodes separated by empty nodes`` () =
        let grid = [|
            [| Cell(Some{x=0; y=0}); Cell(None); Cell(Some{x=2; y=0}); |];
        |]
        findPointsStr grid |> should equal "0 0 2 0 -1 -1 2 0 -1 -1 -1 -1"
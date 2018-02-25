namespace MimeType.Test.Tests

module ``Mime Type`` =
    open NUnit.Framework
    open FsUnit
    open Mimer

    [<Test>]
    let ``Getting valid result with one entry`` () =
        let m = Map.empty |> Map.add "txt" "text/text"
        getMimeType m "234.txt" |> should equal "text/text"

    [<Test>]
    let ``Getting UNKNOWN with zero input`` () =
        getMimeType Map.empty "234.txt" |> should equal "UNKNOWN"

    [<Test>]
    let ``Resulting mime type preserve original case`` () =
        let m = Map.empty |> Map.add "txt" "text/TeXt"
        getMimeType m "234.txt" |> should equal "text/TeXt"

    [<Test>]
    let ``Getting valid result with complex extension`` () =
        let m = Map.empty |> Map.add "log" "text/log"
        getMimeType m "234.txt.log" |> should equal "text/log"

    [<Test>]
    let ``Getting UNKNOWN without extension`` () =
        let m = Map.empty |> Map.add "log" "text/log"
        getMimeType m "234" |> should equal "UNKNOWN"


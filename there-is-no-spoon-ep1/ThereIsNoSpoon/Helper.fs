module Helper

open ThereIsNoSpoon;

let buildGameGrid width height (readLine: (unit -> string)) =
    let grid = Array.zeroCreate height
    for i in 0 .. height - 1 do
        Array.set grid i <| Array.zeroCreate width
        let c = readLine().ToCharArray() (* width characters, each either 0 or . *)
        for j in 0 .. width - 1 do
            match c.[j] with
            | '0' -> Array.set grid.[i] j <| Cell(Some{x=j; y=i})
            | '.' -> Array.set grid.[i] j <| Cell(None)
            | a -> invalidArg "c" (a.ToString())
    grid
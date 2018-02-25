// Learn more about F# at http://fsharp.org

open System
open Mimer

[<EntryPoint>]
let main argv =
    let N = int(Console.In.ReadLine()) (* Number of elements which make up the association table. *)
    let Q = int(Console.In.ReadLine()) (* Number Q of file names to be analyzed. *)
    let a = Array.zeroCreate N
    for i in 0 .. N - 1 do
        (* EXT: file extension *)
        (* MT: MIME type. *)
        let token = (Console.In.ReadLine()).Split [|' '|]
        let EXT = token.[0].ToLowerInvariant()
        let MT = token.[1].ToLowerInvariant()
        a.[i] <- (EXT, MT)
        ()

    let mime = a |> Map.ofArray |> getMimeType
    for i in 0 .. Q - 1 do
        let FNAME = Console.In.ReadLine() (* One file name per line. *)
        (* For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN. *)
        printfn "%s" (mime FNAME)
        ()
    0


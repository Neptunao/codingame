module Mimer

let getMimeType dict (filename: string) : string =
    let getMt =
        let s = filename.Split([|'.'|])
        match s with
        | [||] -> None
        | [|_|] -> None
        | _ -> dict |> Map.tryFind(s.[s.Length - 1].ToLowerInvariant())
    getMt |> Option.defaultValue "UNKNOWN"

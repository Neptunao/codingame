(* Don't let the machines win. You are humanity's last hope... *)
module ThereIsNoSpoon

open System;

type Point = { x: int; y: int; }
type Cell = Cell of Point option
type GameGrid = Cell[][]
type PointWithNeighbours = { self: Point; right: Point; bottom: Point  }

let cellToStr cell =
    cell.x.ToString() + " " +  cell.y.ToString()

let cellToPoint cell =
    match cell with
    | None -> { x = -1; y= -1; }
    | Some(Cell(None)) -> { x = -1; y= -1; }
    | Some(Cell(Some(point))) -> point

let getCell (grid: GameGrid) x y =
    match (x, y) with
    | (_, b) when b >= grid.Length -> None
    | (a, b) when a >= grid.[b].Length -> None
    | (a, b) when grid.[b].[a] = Cell(None) -> None
    | (a, b) -> Some grid.[b].[a]

let rec getNeighbour (grid: GameGrid) x' y' next =
    let (x, y) = next x' y'
    match (x, y) with
    | (_, y) when y >= grid.Length -> { x = -1; y= -1; }
    | (x, y) when x >= grid.[y].Length -> { x = -1; y= -1; }
    | (x, y) -> 
        match grid.[y].[x] with
        | Cell(Some(p)) -> p
        | Cell(None) -> getNeighbour grid x y next

let getRNeighbour grid x y =
    getNeighbour grid x y (fun x y -> (x + 1, y))

let getBNeighbour grid x y =
    getNeighbour grid x y (fun x y -> (x, y + 1))

let getPointWithNeighbours grid x y =
    match getCell grid x y with
    | None -> None
    | Some point -> Some {
        self = Some point |> cellToPoint;
        right = getRNeighbour grid x y;
        bottom = getBNeighbour grid x y;
    }

let rec findPoints (grid: GameGrid) =
    let rec findPoints' x y (v: Map<(int * int), PointWithNeighbours>) =
        if v.ContainsKey (x,y) then
            v
        else
        match (x, y) with
        | (_, y) when y >= grid.Length -> v
        | (x, y) when x >= grid.[y].Length -> v
        | (x, y) ->
            let v' =
                match getPointWithNeighbours grid x y with
                | None -> v 
                | Some p -> v.Add ((x,y), p)
            v' |> 
            findPoints' (x + 1) y |>
            findPoints' x (y + 1)

    findPoints' 0 0 Map.empty |>
    Map.toArray |>
    Array.map (fun (_, v) -> v)
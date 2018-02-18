(* Don't let the machines win. You are humanity's last hope... *)
module ThereIsNoSpoon

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
    | (a, _) when a >= grid.Length -> None
    | (a, b) when b >= grid.[a].Length -> None
    | (a, b) when grid.[a].[b] = Cell(None) -> None 
    | (a, b) -> Some grid.[a].[b]

let getPointWithNeighbours grid x y =
    let getCell' = getCell grid
    let getCellPoint x' y' = getCell' x' y' |> cellToPoint
    match getCell' x y with
    | None -> None
    | Some point -> Some { 
        self = Some point |> cellToPoint; 
        right = getCellPoint (x + 1) y;
        bottom = getCellPoint x (y + 1); 
    }

let rec findPoints grid =
    let rec findPoints' x y (v: Map<(int * int), PointWithNeighbours>) =
        if v.ContainsKey (x,y) then 
            v 
        else
            match getPointWithNeighbours grid x y with
            | None -> v
            | Some p -> 
                v.Add ((x,y), p) |>
                findPoints' (x + 1) y |>
                findPoints' x (y + 1)

    let rec findPoints'' x y (v: Map<(int * int), PointWithNeighbours>) =
        if v.ContainsKey (x,y) then 
            v
        else
        match (x, y) with
        | (x, _) when x >= grid.Length -> v
        | (x, y) when y >= grid.[x].Length -> v
        | (x, y) ->
            v |>
            findPoints' x y |> 
            findPoints'' (x + 1) y |>
            findPoints'' x (y + 1)

    findPoints'' 0 0 Map.empty |>
    Map.toArray |>
    Array.map (fun (_, v) -> v)
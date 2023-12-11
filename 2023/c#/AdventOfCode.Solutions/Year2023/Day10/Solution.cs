namespace AdventOfCode.Solutions.Year2023.Day10;

class Solution : SolutionBase
{
    static Dictionary<Direction, GridMove> directionMove = new Dictionary<Direction, GridMove>{
        {Direction.UP, new(-1,0)},
        {Direction.DOWN, new(1,0)},
        {Direction.RIGHT, new(0,1)},
        {Direction.LEFT, new(0,-1)}
    };

    static Dictionary<Direction, Direction> directionFit = new Dictionary<Direction, Direction>{
        {Direction.UP, Direction.DOWN},
        {Direction.DOWN, Direction.UP},
        {Direction.RIGHT, Direction.LEFT},
        {Direction.LEFT, Direction.RIGHT}
    };

    static Dictionary<char, List<Direction>> symbolDirections = new Dictionary<char, List<Direction>>
    {
        {'|', new List<Direction> {Direction.UP, Direction.DOWN}},
        {'-', new List<Direction> {Direction.LEFT, Direction.RIGHT}},
        {'L', new List<Direction> {Direction.UP, Direction.RIGHT}},
        {'J', new List<Direction> {Direction.LEFT, Direction.UP}},
        {'7', new List<Direction> {Direction.LEFT, Direction.DOWN}},
        {'F', new List<Direction> {Direction.RIGHT, Direction.DOWN}},
        {'.', new List<Direction>()},
        {'S', new List<Direction> {Direction.RIGHT, Direction.LEFT, Direction.DOWN, Direction.UP}},
    };

    static Direction[] allDirections = (Direction[])Enum.GetValues(typeof(Direction));

    char[][] grid = [];
    bool[][] finalIsInner = [];
    GridPosition startPosition = new(-1, -1);
    List<GridPosition> pipe = new();

    public Solution() : base(10, 2023, "")
    {
        grid = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();
        startPosition = FindStartPosition(grid);

        PrintGraph(grid);
        Console.WriteLine("Start Position: " + startPosition);
    }

    protected override string SolvePartOne()
    {
        pipe = FindPipeInGraph(startPosition, grid);
        PrettyPrintPipe();
        return Math.Ceiling((double)(pipe.Count() - 2) / 2).ToString();
    }

    protected override string SolvePartTwo()
    {
        finalIsInner = FindInnerPositions();
        PrettyPrintPipe();
        return finalIsInner.Sum(row => row.Count(cell => cell)).ToString();
    }

    private bool[][] FindInnerPositions()
    {
        bool[][] innerPosition = new bool[grid.Length][];
        for (int i = 0; i < grid.Length; i++)
        {
            innerPosition[i] = new bool[grid[i].Length];
        }

        for (int i = 0; i < pipe.Count - 1; i++)
        {
            GridPosition pos1 = pipe[i];
            GridPosition pos2 = pipe[i + 1];
            GridMove move = pos1.CalculateGridMove(pos2);
            Direction direction = getDirectionByMove(move);
            Direction whereIsLeft = GetRelativeLeftDirection(direction);

            var possibleInner1 = pos1.Move(directionMove[whereIsLeft]);
            var possibleInner2 = pos2.Move(directionMove[whereIsLeft]);
            if (!pipe.Contains(possibleInner1))
            {
                innerPosition[possibleInner1.Row][possibleInner1.Col] = true;
            }
            if (!pipe.Contains(possibleInner2))
            {
                innerPosition[possibleInner2.Row][possibleInner2.Col] = true;
            }

           
            innerPosition = FloodFillInner(innerPosition, possibleInner1);
            innerPosition = FloodFillInner(innerPosition, possibleInner2);

            Console.WriteLine("Moved " + direction);
        }

        return innerPosition;
    }

    private bool[][] FloodFillInner(bool[][] innerPositions, GridPosition position)
    {
        if (pipe.Contains(position)) return innerPositions;
        foreach (var direction in allDirections)
        {
            GridPosition newPosition = position.Move(directionMove[direction]);
            if (newPosition.Row < 0 || newPosition.Row >= grid.Length 
                || newPosition.Col < 0 || newPosition.Col >= grid[0].Length)
            {
                continue;
            }
            if (innerPositions[newPosition.Row][newPosition.Col] == false)
            {
                if (!pipe.Contains(newPosition)){
                    innerPositions[newPosition.Row][newPosition.Col] = true;
                    innerPositions = FloodFillInner(innerPositions, newPosition);
                }
            }
        }
        return innerPositions;
    }

    private GridPosition FindStartPosition(char[][] graph)
    {
        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                if (graph[i][j] == 'S') return new GridPosition(i, j);
            }
        }
        return new(-1, -1);
    }

    private List<GridPosition> FindPipeInGraph(
        GridPosition startPosition,
        char[][] graph
    )
    {
        Direction lastMoveDirection = Direction.UP;
        List<GridPosition> pipe = [startPosition];
        foreach (var direction in symbolDirections['S'])
        {
            GridPosition newPosition = startPosition.Move(directionMove[direction]);
            char gridSymbol = graph[newPosition.Row][newPosition.Col];
            if (symbolDirections[gridSymbol].Contains(directionFit[direction]))
            {
                Console.WriteLine("Step " + direction);
                pipe.Add(newPosition);
                lastMoveDirection = direction;
                break;
            }
        }

        GridPosition position = pipe.Last();
        while (grid[pipe.Last().Row][pipe.Last().Col] != 'S')
        {
            char gridSymbol = graph[position.Row][position.Col];
            Console.WriteLine("At " + position + " - " + gridSymbol);
            Direction direction = symbolDirections[gridSymbol].Where(d => d != directionFit[lastMoveDirection]).First();
            Console.WriteLine("Step " + direction);
            position = position.Move(directionMove[direction]);
            pipe.Add(position);
            lastMoveDirection = direction;


            // PrettyPrintPipeAnimate(pipe, graph);
        }

        return pipe;
    }

    private Direction getDirectionByMove(GridMove move)
    {
        if (move.Equals(directionMove[Direction.UP])) return Direction.UP;
        else if (move.Equals(directionMove[Direction.DOWN])) return Direction.DOWN;
        else if (move.Equals(directionMove[Direction.LEFT])) return Direction.LEFT;
        else return Direction.RIGHT;
    }

    public Direction GetRelativeLeftDirection(Direction currentDirection)
    {
        switch (currentDirection)
        {
            case Direction.UP:
                return Direction.LEFT;
            case Direction.DOWN:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.DOWN;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, "Unknown direction");
        }
    }

    private void PrintGraph(char[][] graph) => graph.ToList().ForEach(line => Console.WriteLine(new string(line)));
    private void PrintPipe(List<GridPosition> pipe) =>
        Console.WriteLine("Pipe:\n" + string.Join("\n", pipe.Select(position => $"({position.Row}, {position.Col})")));

    private void PrettyPrintPipe()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                string output;
                if (pipe.Contains(new GridPosition(i, j)))
                {
                    if (grid[i][j] == 'L') output = "└";
                    else if (grid[i][j] == 'F') output = "┌";
                    else if (grid[i][j] == 'J') output = "┘";
                    else if (grid[i][j] == '7') output = "┐";
                    else if (grid[i][j] == '|') output = "|";
                    else if (grid[i][j] == '-') output = "-";
                    else if (grid[i][j] == 'S') output = "S";
                    else output = "?";
                }
                else if (finalIsInner.Length == grid.Length && finalIsInner[i][j]) 
                {
                    output = "X";
                }
                else {
                    output = "◙";
                }
                Console.Write($"{output,-1}"); // Adjust the width based on your needs
            }
            Console.WriteLine();
        }
    }

    private void PrettyPrintPipeAnimate(List<GridPosition> pipe, char[][] grid)
    {
        foreach (var position in pipe)
        {
            Console.SetCursorPosition(position.Col, position.Row); // Set cursor position to the current pipe position

            string output;
            if (grid[position.Row][position.Col] == 'L') output = "└";
            else if (grid[position.Row][position.Col] == 'F') output = "┌";
            else if (grid[position.Row][position.Col] == 'J') output = "┘";
            else if (grid[position.Row][position.Col] == '7') output = "┐";
            else if (grid[position.Row][position.Col] == '|') output = "|";
            else if (grid[position.Row][position.Col] == '-') output = "-";
            else if (grid[position.Row][position.Col] == 'S') output = "S";
            else output = "?";

            Console.Write(output);

            // Introduce a delay between each step (adjust the sleep duration as needed)
            Thread.Sleep(1); // Sleep for 500 milliseconds (half a second)
        }
    }
}

class GridPosition(int row, int col)
{
    public int Row = row;
    public int Col = col;
    public GridPosition Move(GridMove move) => new GridPosition(Row + move.Row, Col + move.Col);

    public GridMove CalculateGridMove(GridPosition other)
    {
        int rowDifference = other.Row - Row;
        int colDifference = other.Col - Col;
        return new GridMove(rowDifference, colDifference);
    }

    public override bool Equals(object obj) => obj is GridPosition other && Row == other.Row && Col == other.Col;
    public override string ToString() => $"GraphPosition(Row: {Row}, Col: {Col})";
}

class GridMove : GridPosition
{
    public GridMove(int row, int col) : base(row, col) { }
    public override string ToString() => $"GridMove(Row: {Row}, Col: {Col})";
}

public enum Direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}
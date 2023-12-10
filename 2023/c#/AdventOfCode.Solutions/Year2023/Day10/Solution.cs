namespace AdventOfCode.Solutions.Year2023.Day10;

class Solution : SolutionBase
{
    char[][] graphNodes = [];
    List<GridPosition>[][] graphVertices = [];
    GridPosition startPosition = new(-1, -1);

    List<GridPosition> pipe = new();

    bool?[][] graphNodeIsInner = [];

    Dictionary<char, List<GridMove>> symbolMoves = new Dictionary<char, List<GridMove>>{
        {'|', new List<GridMove> {new(-1,0), new(1, 0)}},
        {'-', new List<GridMove> {new(0,-1), new(0, 1)}},
        {'L', new List<GridMove> {new(-1,0), new(0, 1)}},
        {'J', new List<GridMove> {new(0,-1), new(-1, 0)}},
        {'7', new List<GridMove> {new(0,-1), new(1, 0)}},
        {'F', new List<GridMove> {new(0,1), new(1, 0)}},
        {'.', new List<GridMove>{}},
        {'S', new List<GridMove>{new(0,1), new(0,-1), new(1,0), new(-1,0)}},
    };

    List<GridMove> movesAround = new List<GridMove> {
        new GridMove(-1,0),
        new GridMove(1,0),
        new GridMove(0,-1),
        new GridMove(0,1)
    };

    public Solution() : base(10, 2023, "")
    {
        graphNodes = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();
        startPosition = FindStartPosition(graphNodes);
        graphVertices = FindAllVertices(graphNodes);

        PrintGraph(graphNodes);
        PrintGraphVertices(graphVertices);
        Console.WriteLine(startPosition);
    }

    protected override string SolvePartOne()
    {
        pipe = FindPipeInGraph(startPosition, graphNodes, graphVertices);
        PrintPipe(pipe);
        return Math.Ceiling((double)(pipe.Count() - 2) / 2).ToString();
    }

    protected override string SolvePartTwo()
    {
        int innerTileCount = 0;
        int totalCells = graphNodes.Length * graphNodes[0].Length;
        int visitedCells = 0;

        graphNodeIsInner = new bool?[graphNodes.Length][];

        for (int i = 0; i < graphNodes.Length; i++)
        {
            graphNodeIsInner[i] = new bool?[graphNodes[0].Length];
        }

        for (int i = 0; i < graphNodes.Length; i++)
        {
            for (int j = 0; j < graphNodes[i].Length; j++)
            {
                visitedCells++;

                if (!pipe.Contains(new GridPosition(i, j)))
                {
                    Console.WriteLine("Test for inner position " + new GridPosition(i, j));
                    // Possible Inner Tile, becausee it's not part of the loop
                    // Check every Tile around is part of the Pipe
                    if (CheckIsInnerPosition(new GridPosition(i, j), new()))
                    {
                        graphNodeIsInner[i][j] = true;
                        innerTileCount += 1;
                    }
                    else
                    {
                        graphNodeIsInner[i][j] = false;
                    }

                    double progressPercentage = (double)visitedCells / totalCells * 100;
                    Console.WriteLine($"Progress: {progressPercentage:F2}%\n");
                }
            }
        }

        PrintIsInnerGrid();

        int trueCount = 0;
        for (int i = 0; i < graphNodeIsInner.Length; i++)
        {
            for (int j = 0; j < graphNodeIsInner[i].Length; j++)
            {
                if (graphNodeIsInner[i][j] == true)
                {
                    trueCount++;
                }
            }
        }

        Console.WriteLine($"Number of true values: {trueCount}");

        return innerTileCount.ToString();
    }

    private bool CheckIsInnerPosition(GridPosition position, List<GridPosition> alreadyChecked)
    {
        // Console.WriteLine("Check (recursive) " + position);

        if (position.Row >= graphNodes.Length || position.Row < 0
            || position.Col >= graphNodes[0].Length || position.Col < 0)
        {
            return false;
        }

        if (graphNodeIsInner[position.Row][position.Col] != null)
        {
            return (bool)graphNodeIsInner[position.Row][position.Col];
        }


        for (int k = 0; k < movesAround.Count; k++)
        {
            var testPosition = new GridPosition(position.Row, position.Col).Move(movesAround[k]);
            if (!alreadyChecked.Contains(testPosition) && !pipe.Contains(testPosition))
            {
                alreadyChecked.Add(position);
                if (!CheckIsInnerPosition(testPosition, alreadyChecked))
                {
                    return false;
                }
            }
        }

        return true;
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

    private List<GridPosition>[][] FindAllVertices(char[][] graph)
    {
        List<GridPosition>[][] graphVertices = new List<GridPosition>[graph.Length][];

        for (int i = 0; i < graph.Length; i++)
        {
            graphVertices[i] = new List<GridPosition>[graph[i].Length];

            for (int j = 0; j < graph[i].Length; j++)
            {
                graphVertices[i][j] = new List<GridPosition>();
            }
        }

        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                List<GridPosition> vertices = new List<GridPosition>();
                for (int k = 0; k < symbolMoves[graph[i][j]].Count; k++)
                {
                    vertices.Add(new GridPosition(i, j).Move(symbolMoves[graph[i][j]][k]));
                }
                graphVertices[i][j] = vertices;
            }
        }

        return graphVertices;
    }

    private List<GridPosition> FindPipeInGraph(
        GridPosition startPosition,
        char[][] graph,
        List<GridPosition>[][] graphVertices
    )
    {
        List<GridPosition> pipe = [startPosition];
        Console.WriteLine("MoveThoughPipe: " + startPosition + " -> " + graphNodes[startPosition.Row][startPosition.Col]);
        foreach (var item in graphVertices[startPosition.Row][startPosition.Col])
        {
            Console.WriteLine("Check start in direction " + item);
            if (graphVertices[item.Row][item.Col].Contains(new GridPosition(startPosition.Row, startPosition.Col)))
            {
                pipe.Add(item);
                Console.WriteLine("MoveThoughPipe: " + item + " -> " + graphNodes[item.Row][item.Col]);
                break;
            }
        }

        return MoveThroughPipe(pipe.Last(), startPosition, 'S', pipe);
    }

    private List<GridPosition> MoveThroughPipe(
        GridPosition position,
        GridPosition lastPosition,
        char breakChar,
        List<GridPosition> pipe
    )
    {
        if (graphNodes[position.Row][position.Col] == breakChar) return pipe;
        GridPosition nextPosition = graphVertices[position.Row][position.Col].Where(x => !x.Equals(lastPosition)).First();
        pipe.Add(nextPosition);
        Console.WriteLine("MoveThoughPipe: " + nextPosition + " -> " + graphNodes[nextPosition.Row][nextPosition.Col]);
        return MoveThroughPipe(nextPosition, position, breakChar, pipe);
    }

    private void PrintGraph(char[][] graph) => graph.ToList().ForEach(line => Console.WriteLine(new string(line)));
    private void PrintPipe(List<GridPosition> pipe) =>
        Console.WriteLine("Pipe:\n" + string.Join("\n", pipe.Select(position => $"({position.Row}, {position.Col})")));

    private void PrintGraphVertices(List<GridPosition>[][] graphVertices)
    {
        for (int i = 0; i < graphVertices.Length; i++)
        {
            for (int j = 0; j < graphVertices[i].Length; j++)
            {
                Console.Write($"[{i}][{j}]: ");

                foreach (var vertex in graphVertices[i][j])
                {
                    Console.Write($"({vertex.Row}, {vertex.Col}) ");
                }

                Console.WriteLine();
            }
        }
    }

    private void PrintIsInnerGrid()
    {
        for (int i = 0; i < graphNodeIsInner.Length; i++)
        {
            for (int j = 0; j < graphNodeIsInner[i].Length; j++)
            {
                bool? value = graphNodeIsInner[i][j];
                string output;
                if (graphNodeIsInner[i][j] == null) {
                    if (graphNodes[i][j] == 'L') output = "└";
                    else if (graphNodes[i][j] == 'F') output = "┌";
                    else if (graphNodes[i][j] == 'J') output = "┘";
                    else if (graphNodes[i][j] == '7') output = "┐";
                    else if (graphNodes[i][j] == '|') output = "|";
                    else if (graphNodes[i][j] == '-') output = "-";
                    else output = "?";
                }
                else if(graphNodeIsInner[i][j] == true) output = "I";
                else output = "O";
                Console.Write($"{output,-1}"); // Adjust the width based on your needs
            }
            Console.WriteLine();
        }
    }
}

class GridPosition(int row, int col)
{
    public int Row = row;
    public int Col = col;
    public GridPosition Move(GridMove move) => new GridPosition(Row + move.Row, Col + move.Col);
    public override bool Equals(object obj) => obj is GridPosition other && Row == other.Row && Col == other.Col;
    public override string ToString() => $"GraphPosition(Row: {Row}, Col: {Col})";
}

class GridMove : GridPosition
{
    public GridMove(int row, int col) : base(row, col) { }
    public override string ToString() => $"GridMove(Row: {Row}, Col: {Col})";
}
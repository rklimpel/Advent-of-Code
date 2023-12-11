namespace AdventOfCode.Solutions.Year2023.Day11;

class Solution : SolutionBase
{
    char[][] grid;
    List<Tuple<int, int>> galaxyPositions;

    public Solution() : base(11, 2023, "")
    {
        var gridTmp = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();

        grid = ExpandGrid(gridTmp);
        galaxyPositions = GetGalaxyPositions(grid);
        PrintGrid(grid);
        Console.WriteLine(string.Join(", ", galaxyPositions));
    }

    protected override string SolvePartOne()
    {
        int[][] shortestPaths = FindShortestPaths(galaxyPositions, grid);

        Console.WriteLine(string.Join(Environment.NewLine,
            shortestPaths.Select(row => string.Join(" ", row.Select(distance =>
                distance == int.MaxValue ? "?" : distance.ToString().PadLeft(3)))))
        );

        int sum = 0;
        for (int i = 0; i < shortestPaths.Length; i++)
        {
            for (int j = 0; j <= i - 1; j++)
            {
                Console.WriteLine($"Add Distance from {i + 1}->{j + 1} = {shortestPaths[i][j]}");
                sum += shortestPaths[i][j];
            }
        }

        PrintGrid(grid, addGalaxyNumbers: true);

        return sum.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    private char[][] ExpandGrid(char[][] originalGrid)
    {
        List<int> emptyRows = new List<int>();
        List<int> emptyColumns = new List<int>();

        for (int i = 0; i < originalGrid.Length; i++)
        {
            if (originalGrid[i].All(c => c != '#'))
            {
                emptyRows.Add(i);
            }

            if (originalGrid.All(row => row[i] != '#'))
            {
                emptyColumns.Add(i);
            }
        }

        Console.WriteLine("Empty Rows: " + string.Join(", ", emptyRows));
        Console.WriteLine("Empty Columns: " + string.Join(", ", emptyColumns));

        int expandedRows = originalGrid.Length + emptyRows.Count * 2;
        int expandedColumns = originalGrid[0].Length + emptyColumns.Count * 2;

        List<List<char>> expandedGrid = new();

        for (int i = 0; i < originalGrid.Length; i++)
        {
            List<char> row = new();

            for (int j = 0; j < originalGrid[0].Length; j++)
            {
                row.Add(originalGrid[i][j]);
                if (emptyColumns.Contains(j))
                {
                    row.Add('.');
                }
            }

            expandedGrid.Add(row);
            if (emptyRows.Contains(i))
            {
                expandedGrid.Add(row);
            }
        }

        return expandedGrid
        .Select(row => row.ToArray())
        .ToArray();
    }
    public int[][] FindShortestPaths(
        List<Tuple<int, int>> galaxyPositions, char[][] grid
    )
    {
        List<int> allNodes = Enumerable.Range(0, grid.Length * grid[0].Length).ToList();
        int nodeCount = allNodes.Count();
        int[,] distances = new int[nodeCount, nodeCount];

        // Set Max Value for all Distances
        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                distances[i, j] = int.MaxValue;
            }
        }

        //Init with inital neigbour distance values
        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                if (i == j)
                {
                    distances[i, j] = 0;
                }
                else
                {
                    Tuple<int, int> gridPositionI = GetGridPosition(i, grid.Length - 1, grid[0].Length - 1);
                    Tuple<int, int> gridPositionJ = GetGridPosition(j, grid.Length - 1, grid[0].Length - 1);
                    distances[i, j] = AreAdjacentInGrid(gridPositionI, gridPositionJ) ? 1 : int.MaxValue;
                }
            }
        }

        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                if (distances[i, j] == int.MaxValue)
                {
                    Console.Write("-");
                }
                else
                {
                    Console.Write(distances[i, j].ToString());
                }

            }
            Console.WriteLine();
        }

        // Floyd-Warshall Stuff
        for (int k = 0; k < nodeCount; k++)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    if (distances[i, k] != int.MaxValue && distances[k, j] != int.MaxValue &&
                        distances[i, k] + distances[k, j] < distances[i, j])
                    {
                        distances[i, j] = distances[i, k] + distances[k, j];
                    }
                }
            }
        }

        // Print or process the result distances array
        /*for (int i = 0; i < nodeCount; i++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                Console.Write(distances[i, j].ToString().PadLeft(3));
            }
            Console.WriteLine();
        }*/

        int[][] galaxyDistancesArray = new int[galaxyPositions.Count][];

        for (int i = 0; i < galaxyPositions.Count; i++)
        {
            galaxyDistancesArray[i] = new int[galaxyPositions.Count];
            for (int j = 0; j < galaxyPositions.Count; j++)
            {
                var gridPositionGalaxyI = galaxyPositions[i];
                var allNodePositionI = gridPositionGalaxyI.Item1 * grid[0].Length + gridPositionGalaxyI.Item2;
                var gridPositionGalaxyJ = galaxyPositions[j];
                var allNodePositionJ = gridPositionGalaxyJ.Item1 * grid[0].Length + gridPositionGalaxyJ.Item2;
                Console.WriteLine($"Distance between galaxy {i} and {j}: {distances[allNodePositionI, allNodePositionJ]}");
                galaxyDistancesArray[i][j] = distances[allNodePositionI, allNodePositionJ];
            }
        }
        return galaxyDistancesArray;
    }

    private bool AreAdjacentInGrid(Tuple<int, int> posA, Tuple<int, int> posB)
    {
        int rowDiff = Math.Abs(posA.Item1 - posB.Item1);
        int colDiff = Math.Abs(posA.Item2 - posB.Item2);
        return (rowDiff == 0 && colDiff == 1) || (rowDiff == 1 && colDiff == 0);
    }

    private Tuple<int, int> GetGridPosition(int node, int numRows, int numCols)
    {
        Console.WriteLine("GetGridPosition for Node " + node);
        int row = node / numRows;
        int col = node % numCols;
        var result = Tuple.Create(row, col);
        Console.WriteLine(result);
        return result;
    }


    private List<Tuple<int, int>> GetGalaxyPositions(char[][] grid)
    {
        List<Tuple<int, int>> hashPositions = new List<Tuple<int, int>>();
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '#')
                {
                    hashPositions.Add(Tuple.Create(i, j));
                }
            }
        }
        return hashPositions;
    }

    private void PrintGrid(char[][] grid, bool addGalaxyNumbers = false)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (!addGalaxyNumbers)
                {
                    Console.Write(grid[i][j]);
                }
                else
                {
                    if (grid[i][j] == '#')
                    {
                        int galaxyNumber = galaxyPositions.FindIndex(tuple => tuple.Item1 == i && tuple.Item2 == j);
                        Console.Write(galaxyNumber + 1);
                    }
                    else
                    {
                        Console.Write(grid[i][j]);
                    }
                }
            }
            Console.WriteLine();
        }

    }

    private void PrintDistances(Tuple<int, int> start, Dictionary<Tuple<int, int>, int> distances)
    {
        Console.WriteLine($"Shortest paths from ({start.Item1},{start.Item2}):");

        foreach (var kvp in distances)
        {
            Console.WriteLine($"To ({kvp.Key.Item1},{kvp.Key.Item2}): {kvp.Value} steps");
        }

        Console.WriteLine();
    }
}

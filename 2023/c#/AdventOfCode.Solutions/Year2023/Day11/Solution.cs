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
        PrintGrid(grid, addGalaxyNumbers: true);

        int[,] distances = new int[galaxyPositions.Count, galaxyPositions.Count];
        for (int i = 0; i < galaxyPositions.Count; i++)
        {
            for (int j = 0; j < galaxyPositions.Count; j++)
            {
                distances[i, j] = Math.Abs(galaxyPositions[i].Item1 - galaxyPositions[j].Item1)
                    + Math.Abs(galaxyPositions[i].Item2 - galaxyPositions[j].Item2);
            }
        }

        int sum = 0;
        for (int i = 0; i < galaxyPositions.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                sum += distances[i, j];
            }
        }

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

}

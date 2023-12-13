namespace AdventOfCode.Solutions.Year2023.Day13;

class Solution : SolutionBase
{
    private List<char[][]> grids = [];

    private List<int> horizontalMirrorLines = [];

    private int task1Result = 0;

    public Solution() : base(13, 2023, "")
    {
        var splitInput = Input.Split("\n\n");
        foreach (var input in splitInput)
        {
            var grid = input
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Select(c => c).ToArray())
                .ToArray();
            grids.Add(grid);
        }

        foreach (var grid in grids)
        {
            var gridResult = 0;

            Console.WriteLine("Check next grid:");
            PrintGrid(grid);

            // Check for mirr lines in rows
            for (int i = 0; i < grid.Length - 1; i++)
            {
                if (string.Join("", grid[i]) == string.Join("", grid[i + 1]))
                {
                    if (CheckIsMirrorRow(grid, i, i + 1))
                    {
                        Console.WriteLine($"There is a mirror line between row {i} and {i + 1}");
                        gridResult = 100 * (i + 1);
                    }
                }
            }

            var turnedGrid = TransposeGrid(grid);

            for (int i = 0; i < turnedGrid.Length - 1; i++)
            {
                if (string.Join("", turnedGrid[i]) == string.Join("", turnedGrid[i + 1]))
                {
                    if (CheckIsMirrorRow(turnedGrid, i, i + 1))
                    {
                        Console.WriteLine($"There is a mirror line between column {i} and {i + 1}");
                        gridResult = +(i + 1);
                    }
                }
            }

            Console.WriteLine("Grid Result: " + gridResult);
            task1Result += gridResult;
        }
    }

    private char[][] TransposeGrid(char[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        char[][] turnedGrid = new char[cols][];
        for (int i = 0; i < cols; i++)
        {
            turnedGrid[i] = new char[rows];
            for (int j = 0; j < rows; j++)
            {
                turnedGrid[i][j] = grid[j][i];
            }
        }

        return turnedGrid;
    }

    private bool CheckIsMirrorRow(char[][] grid, int rowA, int rowB)
    {
        while (rowA >= 0 && rowB < grid.Length)
        {
            if (string.Join("", grid[rowA]) == string.Join("", grid[rowB]))
            {
                rowA -= 1;
                rowB += 1;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void PrintGrid(char[][] grid) => grid.ToList().ForEach(line => Console.WriteLine(new string(line)));

    protected override string SolvePartOne()
    {
        return $"{task1Result}";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}

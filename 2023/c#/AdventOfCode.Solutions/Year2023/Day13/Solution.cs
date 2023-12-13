namespace AdventOfCode.Solutions.Year2023.Day13;

class Solution : SolutionBase
{
    private List<char[][]> grids = [];

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
    }

    protected override string SolvePartOne()
    {
        Console.WriteLine("Solve Part 1");
        int result = 0;
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
            result += gridResult;
        }
        return $"{result}";
    }

    protected override string SolvePartTwo()
    {
        Console.WriteLine("Solve Part 2");
        int result = 0;
        foreach (var grid in grids)
        {
            var gridResult = 0;

            Console.WriteLine("Check next grid:");
            PrintGrid(grid);

            // Check for mirr lines in rows
            for (int i = 0; i < grid.Length - 1; i++)
            {
                Console.WriteLine($"Check {i} and {i + 1}");
                Console.WriteLine(AreTheseStupidStringsDifferentByExactlyOneChar(
                    string.Join("", grid[i]),
                    string.Join("", grid[i + 1])
                ));
                if (AreTheseStupidStringsDifferentByExactlyOneChar(
                    string.Join("", grid[i]),
                    string.Join("", grid[i + 1])
                ) || string.Join("", grid[i]) == string.Join("", grid[i + 1]))
                {
                    var isOkayToBeAlsoOneStupidDifference = !AreTheseStupidStringsDifferentByExactlyOneChar(
                        string.Join("", grid[i]),
                        string.Join("", grid[i + 1])
                    );
                    if (CheckIsMirrorRow(grid, i, i + 1, true))
                    {
                        Console.WriteLine($"There is a mirror line between row {i} and {i + 1}");
                        gridResult = 100 * (i + 1);
                    }
                }
            }

            var turnedGrid = TransposeGrid(grid);

            for (int i = 0; i < turnedGrid.Length - 1; i++)
            {
                if (AreTheseStupidStringsDifferentByExactlyOneChar(
                    string.Join("", turnedGrid[i]),
                    string.Join("", turnedGrid[i + 1])
                ) || string.Join("", turnedGrid[i]) == string.Join("", turnedGrid[i + 1]))
                {
                    var isOkayToBeAlsoOneStupidDifference = !AreTheseStupidStringsDifferentByExactlyOneChar(
                        string.Join("", turnedGrid[i]),
                        string.Join("", turnedGrid[i + 1])
                    );
                    if (CheckIsMirrorRow(turnedGrid, i, i + 1, true))
                    {
                        Console.WriteLine($"There is a mirror line between column {i} and {i + 1}");
                        gridResult += (i + 1);
                    }
                }
            }

            Console.WriteLine("Grid Result: " + gridResult);
            result += gridResult;
        }
        return $"{result}";
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

    private bool CheckIsMirrorRow(char[][] grid, int rowA, int rowB, bool isOkayToBeAlsoOneStupidDifference = false)
    {
        Console.WriteLine("CheckIsMirrorRow is okay to be also " + isOkayToBeAlsoOneStupidDifference);

        while (rowA >= 0 && rowB < grid.Length)
        {
            if (isOkayToBeAlsoOneStupidDifference)
            {
                if (string.Join("", grid[rowA]) == string.Join("", grid[rowB]))
                {
                    rowA -= 1;
                    rowB += 1;
                }
                else if (AreTheseStupidStringsDifferentByExactlyOneChar(
                    string.Join("", grid[rowA]),
                    string.Join("", grid[rowB])
                ))
                {
                    rowA -= 1;
                    rowB += 1;
                    isOkayToBeAlsoOneStupidDifference = false;
                }
                else
                {
                    return false;
                }
            }
            else
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
        }

        if (isOkayToBeAlsoOneStupidDifference && rowA <= 0 && rowB >= grid.Length - 1)
        {
            return false;
        }

        return true;
    }

    private bool AreTheseStupidStringsDifferentByExactlyOneChar(string str1, string str2)
    {
        int differences = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            if (str1[i] != str2[i])
            {
                differences++;
                if (differences > 1)
                {
                    return false; // More than one difference found
                }
            }
        }

        return differences == 1;
    }

    private void PrintGrid(char[][] grid) => grid.ToList().ForEach(line => Console.WriteLine(new string(line)));
}

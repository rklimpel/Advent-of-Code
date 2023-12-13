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
        /*int result = 0;
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
        return $"{result}";*/

        int result = 0;
        int id = 0;

        foreach (var data in grids)
        {
            id += 1;
            int res = FindMirror(data);
            if (res == -1)
            {
                var transposedData = TransposeGrid(data);
                res = FindMirror(transposedData);

                if (res == -1)
                {
                    Console.WriteLine($"No solution found for line {id}");
                }
                else
                {
                    Console.WriteLine($"Line {id} found vertical solution at {res}");
                }

                result += res;
            }
            else
            {
                Console.WriteLine($"Line {id} found horizontal solution at {res}");
                result += res * 100;
            }
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

    private int CheckSmudgeLines(int i, int j, char[][] data)
    {
        var first = data[i];
        var second = data[j];
        var thisIsTheDiffCount = 0;

        for (int k = 0; k < first.Length; k++)
        {
            if (first[k] != second[k])
            {
                thisIsTheDiffCount += 1;
            }
            if (thisIsTheDiffCount > 1)
            {
                return -1;
            }
        }

        return thisIsTheDiffCount == 1 ? 1 : 0;
    }

    private int CheckSame(int k, int n, char[][] lines, ref int myAwesomeSmugCounter)
    {
        if (k >= 0 && n < lines.Length)
        {
            var wasSmug = CheckSmudgeLines(k, n, lines);
            if (wasSmug == -1)
            {
                return -1;
            }

            if (wasSmug == 1)
            {
                if (myAwesomeSmugCounter > 0)
                {
                    return -1;
                }
                else
                {
                    myAwesomeSmugCounter += 1;
                    return 1;
                }
            }

            return lines[k].SequenceEqual(lines[n]) ? 1 : -1;
        }

        return 0;
    }

    private int LoopThrough(int i, char[][] data, ref int myAwesomeSmugCounter)
    {
        int idx = 0;
        for (int j = i; j > 0; j--)
        {
            idx += 1;
            var cellRes = CheckSame(i - idx, i + idx + 1, data, ref myAwesomeSmugCounter);
            if (cellRes == -1)
            {
                return -1;
            }
            else if (cellRes == 0)
            {
                return i + 1;
            }
        }

        return i + 1;
    }

    private int FindMirror(char[][] data)
    {
        int myAwesomeSmugCounter = 0;

        for (int i = 0; i < data.Length - 1; i++)
        {
            myAwesomeSmugCounter = 0;
            myAwesomeSmugCounter = CheckSmudgeLines(i, i + 1, data);

            if (myAwesomeSmugCounter == -1)
            {
                continue;
            }

            var res = CheckSame(i, i + 1, data, ref myAwesomeSmugCounter);

            if (res < 1 && myAwesomeSmugCounter == 0)
            {
                continue;
            }
            else if (res == 1 || myAwesomeSmugCounter == 1)
            {
                if (i == 0 && myAwesomeSmugCounter == 1)
                {
                    return i + 1;
                }

                var val = LoopThrough(i, data, ref myAwesomeSmugCounter);

                if (val == -1 || myAwesomeSmugCounter == 0)
                {
                    continue;
                }

                if (myAwesomeSmugCounter == 1)
                {
                    return val;
                }
            }
        }

        return -1;
    }
}

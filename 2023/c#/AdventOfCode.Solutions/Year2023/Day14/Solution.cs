namespace AdventOfCode.Solutions.Year2023.Day14;

class Solution : SolutionBase
{
    private char[][] grid = [];

    public Solution() : base(14, 2023, "")
    {
        grid = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();
    }

    protected override string SolvePartOne()
    {
        var task1Grid = moveStones(grid, "north");
        var result = CalcLoad(task1Grid);
        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        var task2Grid = grid;
        var gridVariants = new Dictionary<int, List<int>>();
        for (int i = 0; i < 350; i++)
        {
            moveStones(task2Grid, "north");
            //Console.WriteLine("Moved north");
            //PrintGrid(task2Grid);
            moveStones(task2Grid, "west");
            //Console.WriteLine("Moved west");
            //PrintGrid(task2Grid);
            moveStones(task2Grid, "south");
            //Console.WriteLine("Moved south");
            //PrintGrid(task2Grid);
            moveStones(task2Grid, "east");
            //Console.WriteLine("Moved east");
            //PrintGrid(task2Grid);
            if (i % 1 == 0)
            {
                Console.WriteLine($"Finished Cycle {i}.");
            }

            //PrintGrid(task2Grid);
            //Console.WriteLine("---------------");

            int gridKey = CalcLoad(task2Grid);
            if (gridVariants.TryGetValue(gridKey, out var cycles))
            {
                cycles.Add(i);
                //PrintGrid(task2Grid);
                //Console.WriteLine($"Calc Load repeated after {i} cycles. Previous appearance at cycles: {string.Join(", ", cycles)}");
                //Console.WriteLine($"Grid Weight is: {CalcLoad(task2Grid)}");
                if (cycles.Count() == 1000)
                {
                    break;
                }
            }
            else
            {
                gridVariants[gridKey] = new List<int> { i };
            }

        }

        foreach (var item in gridVariants.Keys)
        {
            Console.WriteLine($"GridLoad {item} had appearance at cycles: {string.Join(", ", gridVariants[item])}");
        }

        var result = CalcLoad(task2Grid);
        return result.ToString();
    }

    private char[][] moveStones(char[][] grid, string direction)
    {
        if (direction == "north" || direction == "west")
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    if (grid[row][col] == 'O')
                    {
                        switch (direction)
                        {
                            case "north":
                                MoveUp(grid, row, col);
                                break;
                            case "west":
                                MoveLeft(grid, row, col);
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            for (int col = grid[0].Length - 1; col >= 0; col--)
            {
                for (int row = grid.Length - 1; row >= 0; row--)
                {
                    if (grid[row][col] == 'O')
                    {
                        switch (direction)
                        {
                            case "south":
                                MoveDown(grid, row, col);
                                break;
                            case "east":
                                MoveRight(grid, row, col);
                                break;
                        }
                    }
                }
            }
        }

        return grid;
    }

    private void MoveUp(char[][] grid, int row, int col)
    {
        for (int i = row; i > 0 && grid[i - 1][col] != '#' && grid[i - 1][col] != 'O'; i--)
        {
            Swap(grid, i, col, i - 1, col);
        }
    }

    private void MoveDown(char[][] grid, int row, int col)
    {
        for (int i = row; i < grid.Length - 1 && grid[i + 1][col] != '#' && grid[i + 1][col] != 'O'; i++)
        {
            Swap(grid, i, col, i + 1, col);
        }
    }

    private void MoveLeft(char[][] grid, int row, int col)
    {
        for (int j = col; j > 0 && grid[row][j - 1] != '#' && grid[row][j - 1] != 'O'; j--)
        {
            Swap(grid, row, j, row, j - 1);
        }
    }

    private void MoveRight(char[][] grid, int row, int col)
    {
        for (int j = col; j < grid[0].Length - 1 && grid[row][j + 1] != '#' && grid[row][j + 1] != 'O'; j++)
        {
            Swap(grid, row, j, row, j + 1);
        }
    }

    private void Swap(char[][] grid, int row1, int col1, int row2, int col2)
    {
        char temp = grid[row1][col1];
        grid[row1][col1] = grid[row2][col2];
        grid[row2][col2] = temp;
    }

    private int CalcLoad(char[][] grid)
    {
        var sum = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            var weight = grid.Length - i;
            var rowSum = grid[i].Where(c => c == 'O').Count() * weight;
            sum += rowSum;
        }
        return sum;
    }

    char[][] CopyGrid(char[][] sourceGrid)
    {
        char[][] copy = new char[sourceGrid.Length][];
        for (int i = 0; i < sourceGrid.Length; i++)
        {
            copy[i] = new char[sourceGrid[i].Length];
            Array.Copy(sourceGrid[i], copy[i], sourceGrid[i].Length);
        }
        return copy;
    }

    bool GridChanged(char[][] lastGrid, char[][] currentGrid)
    {
        for (int row = 0; row < lastGrid.Length; row++)
        {
            for (int col = 0; col < lastGrid[0].Length; col++)
            {
                if (lastGrid[row][col] != currentGrid[row][col])
                {
                    return true;
                }
            }
        }
        return false;
    }

    void PrintGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            Console.WriteLine(new string(row));
        }
        Console.WriteLine();
    }

    string GridToString(char[][] grid)
    {
        // Convert the 2D char array to a single string for using as a dictionary key
        return string.Join("", grid.Select(row => new string(row)));
    }

}

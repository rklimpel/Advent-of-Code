using AdventOfCode.Solutions.Year2023.Day10;

namespace AdventOfCode.Solutions.Year2023.Day11;

class Solution : SolutionBase
{
    char[][] grid;

    public Solution() : base(11, 2023, "")
    {
        var gridTmp = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();

        grid = ExpandGrid(gridTmp);
        PrintGrid(grid);
    }



    protected override string SolvePartOne()
    {
        return "";
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

    private void PrintGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var cell in row)
            {
                Console.Write(cell);
            }
            Console.WriteLine();
        }
    }
}

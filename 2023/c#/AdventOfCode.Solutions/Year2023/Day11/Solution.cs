namespace AdventOfCode.Solutions.Year2023.Day11;

class Solution : SolutionBase
{
    char[][] grid;
    List<Tuple<int, int>> galaxyPositions;

    List<int> emptyRows = new List<int>();
    List<int> emptyColumns = new List<int>();

    public Solution() : base(11, 2023, "")
    {
        grid = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();
        FindExpandRowsAndColumns(grid);
        galaxyPositions = GetGalaxyPositions(grid);
        Console.WriteLine(string.Join(", ", galaxyPositions));
    }

    protected override string SolvePartOne()
    {
        int[,] distances = new int[galaxyPositions.Count, galaxyPositions.Count];
        for (int i = 0; i < galaxyPositions.Count; i++)
        {
            for (int j = 0; j < galaxyPositions.Count; j++)
            {
                distances[i, j] = Math.Abs(galaxyPositions[i].Item1 - galaxyPositions[j].Item1)
                    + Math.Abs(galaxyPositions[i].Item2 - galaxyPositions[j].Item2);

                foreach (var emptyRow in emptyRows)
                {
                    if ((emptyRow > galaxyPositions[i].Item1 && emptyRow < galaxyPositions[j].Item1) ||
                        (emptyRow > galaxyPositions[j].Item1 && emptyRow < galaxyPositions[i].Item1))
                    {
                        distances[i, j] += 1;
                    }
                }

                foreach (var emptyColumn in emptyColumns)
                {
                    if ((emptyColumn > galaxyPositions[i].Item2 && emptyColumn < galaxyPositions[j].Item2) ||
                        (emptyColumn > galaxyPositions[j].Item2 && emptyColumn < galaxyPositions[i].Item2))
                    {
                        distances[i, j] += 1;
                    }
                }
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
        long[,] distances = new long[galaxyPositions.Count, galaxyPositions.Count];
        for (int i = 0; i < galaxyPositions.Count; i++)
        {
            for (int j = 0; j < galaxyPositions.Count; j++)
            {
                distances[i, j] = Math.Abs(galaxyPositions[i].Item1 - galaxyPositions[j].Item1)
                    + Math.Abs(galaxyPositions[i].Item2 - galaxyPositions[j].Item2);

                foreach (var emptyRow in emptyRows)
                {
                    if ((emptyRow > galaxyPositions[i].Item1 && emptyRow < galaxyPositions[j].Item1) ||
                        (emptyRow > galaxyPositions[j].Item1 && emptyRow < galaxyPositions[i].Item1))
                    {
                        distances[i, j] += 1000000 - 1;
                    }
                }

                foreach (var emptyColumn in emptyColumns)
                {
                    if ((emptyColumn > galaxyPositions[i].Item2 && emptyColumn < galaxyPositions[j].Item2) ||
                        (emptyColumn > galaxyPositions[j].Item2 && emptyColumn < galaxyPositions[i].Item2))
                    {
                        distances[i, j] += 1000000 - 1;
                    }
                }
            }
        }

        long sum = 0;
        for (int i = 0; i < galaxyPositions.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                sum += distances[i, j];
            }
        }

        return sum.ToString();
    }

    private void FindExpandRowsAndColumns(char[][] originalGrid)
    {
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
    }

    private List<Tuple<int, int>> GetGalaxyPositions(char[][] grid)
    {
        return grid
             .SelectMany((row, rowIndex) =>
                row.Select((cell, colIndex) => new { Cell = cell, Row = rowIndex, Col = colIndex })
             )
             .Where(item => item.Cell == '#')
             .Select(item => Tuple.Create(item.Row, item.Col))
             .ToList();
    }
}

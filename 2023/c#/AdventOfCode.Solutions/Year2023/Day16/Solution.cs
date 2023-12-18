namespace AdventOfCode.Solutions.Year2023.Day16;

class Solution : SolutionBase
{
    char[][] grid;

    public Solution() : base(16, 2023, "")
    {
        grid = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();
    }

    protected override string SolvePartOne()
    {
        return GetEnergyCount(grid, new(0, 0), Direction.RIGHT).ToString();
    }

    protected override string SolvePartTwo()
    {
        List<int> energyLevels = new();
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (y == 0)
                {
                    energyLevels.Add(GetEnergyCount(grid, new(x, y), Direction.DOWN));
                }
                else if (y == grid.Length - 1)
                {
                    energyLevels.Add(GetEnergyCount(grid, new(x, y), Direction.UP));
                }

                if (x == 0)
                {
                    energyLevels.Add(GetEnergyCount(grid, new(x, y), Direction.RIGHT));
                }
                else if (x == grid[0].Length - 1)
                {
                    energyLevels.Add(GetEnergyCount(grid, new(x, y), Direction.LEFT));
                }
            }
        }

        return energyLevels.Max().ToString();
    }

    public int GetEnergyCount(char[][] grid, Position startPosition, Direction startDirection)
    {
        List<Direction>[][] initEnergizedTiles = new List<Direction>[grid[0].Length][];
        for (int i = 0; i < grid[0].Length; i++)
        {
            initEnergizedTiles[i] = new List<Direction>[grid.Length];
            for (int j = 0; j < grid.Length; j++)
            {
                initEnergizedTiles[i][j] = new List<Direction>();
            }
        }

        List<Direction>[][] energizedTiles = FindEnergizedTiles(grid, initEnergizedTiles, startPosition, startDirection);

        // PrintGrid(energizedTiles);

        int count = energizedTiles.Sum(row => row.Count(col => col.Count > 0));
        return count;
    }

    public List<Direction>[][] FindEnergizedTiles(
        char[][] grid,
        List<Direction>[][] energizedTiles,
        Position beamPosition,
        Direction beamDirection
    )
    {
        // Console.WriteLine("Check " + beamPosition + " in direction " + beamDirection);

        // Check out of grid bounds
        if (beamPosition.X >= grid[0].Length || beamPosition.X < 0 || beamPosition.Y >= grid.Length || beamPosition.Y < 0)
        {
            return energizedTiles;
        }

        if (energizedTiles[beamPosition.Y][beamPosition.X].Contains(beamDirection))
        {
            return energizedTiles;
        }

        // increase energy level of current tile
        energizedTiles[beamPosition.Y][beamPosition.X].Add(beamDirection);

        // calc next Direcitons and move beam
        char currentTile = grid[beamPosition.Y][beamPosition.X];
        List<Direction> nextDirections = GetNextDirections(currentTile, beamDirection);
        foreach (var direction in nextDirections)
        {
            energizedTiles = FindEnergizedTiles(grid, energizedTiles, beamPosition.Move(direction), direction);
        }

        return energizedTiles;
    }

    private List<Direction> GetNextDirections(char currentTile, Direction beamDirection)
    {
        if (currentTile == '.')
        {
            return [beamDirection];
        }
        else if (currentTile == '-')
        {
            if (beamDirection == Direction.LEFT || beamDirection == Direction.RIGHT)
            {
                return [beamDirection];
            }
            else
            {
                return [Direction.LEFT, Direction.RIGHT];
            }
        }
        else if (currentTile == '|')
        {
            if (beamDirection == Direction.UP || beamDirection == Direction.DOWN)
            {
                return [beamDirection];
            }
            else
            {
                return [Direction.UP, Direction.DOWN];
            }
        }
        else if (currentTile == '/')
        {
            switch (beamDirection)
            {
                case Direction.UP: return [Direction.RIGHT];
                case Direction.DOWN: return [Direction.LEFT];
                case Direction.RIGHT: return [Direction.UP];
                case Direction.LEFT: return [Direction.DOWN];
                default: return [];
            }
        }
        else if (currentTile == '\\')
        {
            switch (beamDirection)
            {
                case Direction.UP: return [Direction.LEFT];
                case Direction.DOWN: return [Direction.RIGHT];
                case Direction.RIGHT: return [Direction.DOWN];
                case Direction.LEFT: return [Direction.UP];
                default: return [];
            }
        }
        else
        {
            Console.WriteLine($"Could not handle symbol '{currentTile}'.");
        }
        return [];
    }

    private void PrintGrid(List<Direction>[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j].Count() > 0) Console.Write('#');
                else Console.Write('.');
            }
            Console.WriteLine();
        }
    }
}

public enum Direction
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public class Position(int X, int Y)
{
    public int X = X;
    public int Y = Y;

    public Position Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return new(X, Y - 1);
            case Direction.DOWN: return new(X, Y + 1);
            case Direction.RIGHT: return new(X + 1, Y);
            case Direction.LEFT: return new(X - 1, Y);
            default: return this;
        }
    }

    public override string ToString()
    {
        return $"({Y}, {X})";
    }
}
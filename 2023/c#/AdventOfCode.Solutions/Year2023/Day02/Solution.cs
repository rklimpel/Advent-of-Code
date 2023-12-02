namespace AdventOfCode.Solutions.Year2023.Day02;

class Solution : SolutionBase
{
    private List<Game> games = new();
    private List<Game> possibleGames = new();
    private const int RED_CUBES = 12;
    private const int GREEN_CUBES = 13;
    private const int BLUE_CUBES = 14;
    public Solution() : base(02, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        
        foreach (var line in lines)
        {
            if (line.Contains(':'))
            {
                games.Add(Game.CreateGameFromString(line));

            }
        }

    }

    protected override string SolvePartOne()
    {
        foreach (var game in games)
        {
            bool possible = true;
            foreach (var set in game.sets)
            {
                if (!(set.Blue <= BLUE_CUBES && set.Green <= GREEN_CUBES && set.Red <= RED_CUBES))
                {
                    possible = false;
                }
            }

            if (possible)
            {
                possibleGames.Add(game);
            }
        }
        return possibleGames.Sum(g => g.ID).ToString();
    }

    protected override string SolvePartTwo()
    {
        List<int> cubeSetPower = new List<int>();
        foreach (var game in games)
        {
            int highestBlue = 0;
            int highestRed = 0;
            int highestGreen = 0;
            foreach (var set in game.sets)
            {
                if (set.Blue > highestBlue) highestBlue = set.Blue;
                if (set.Green > highestGreen) highestGreen = set.Green;
                if (set.Red > highestRed) highestRed = set.Red;
            }
            
            cubeSetPower.Add(highestBlue * highestGreen * highestRed);
        }
        return cubeSetPower.Sum().ToString();
    }
}

class Game
{
    public int ID;
    public List<GameSet> sets = new List<GameSet>();

    public static Game CreateGameFromString(string line)
    {
        
        Game game = new Game();
        string[] lineSplitted = line.Split(':');
        game.ID =Int32.Parse(new string(lineSplitted[0].Where(char.IsDigit).ToArray()));
        string[] setStrings = lineSplitted[1].Split(';');

        foreach (var setString in setStrings)
        {
            int blue = 0;
            int green = 0;
            int red = 0;
            
            string[] colorsString = setString.Split(',');
            foreach (var colorString in colorsString)
            {
                if (colorString.Contains("blue"))
                {
                    blue = Int32.Parse(new string(colorString.Where(char.IsDigit).ToArray()));
                } else if (colorString.Contains("green"))
                {
                    green = Int32.Parse(new string(colorString.Where(char.IsDigit).ToArray()));
                } else if (colorString.Contains("red"))
                {
                    red = Int32.Parse(new string(colorString.Where(char.IsDigit).ToArray()));
                }
            }
            
            game.sets.Add(new GameSet(blue, green, red));
        }
            
        return game;
    }
}

class GameSet(int blue, int green, int red)
{
    public int Blue = blue;
    public int Green = green;
    public int Red = red;
}

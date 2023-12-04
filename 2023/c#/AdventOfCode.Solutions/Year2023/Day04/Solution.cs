using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day04;

class Solution : SolutionBase
{
    private int score = 0;
    public Solution() : base(04, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        

        foreach (var line in lines)
        {
            if (!line.Contains(':')) return;
            string numberString = line.Split(':')[1];
            string[] split = numberString.Split('|');
            
            var winningNumbers = split[0].Split(" ")
                .Where(i => i != String.Empty)
                .Select(i => Int32.Parse(i)).ToList();
            var myNumberes = split[1].Split(" ")
                .Where(i => i != String.Empty)
                .Select(i => Int32.Parse(i)).ToList();

            int matches = 0;
            foreach (var winningNumber in winningNumbers)
            {
                if (myNumberes.Any(x => x == winningNumber))
                {
                    matches += 1;
                }
            }

            Console.WriteLine("matches: " + matches);
            Console.WriteLine("add: " + Math.Pow(2, matches - 1));
            score += (int)Math.Pow(2, matches - 1);
            
            Console.WriteLine(score);
        }
    }

    protected override string SolvePartOne()
    {
        return score.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}

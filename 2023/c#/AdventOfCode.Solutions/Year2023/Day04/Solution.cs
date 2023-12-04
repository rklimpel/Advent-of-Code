using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day04;

class Solution : SolutionBase
{
    private int score = 0;
    private int[] cardCopies;
    public Solution() : base(04, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        
        cardCopies = Enumerable.Repeat(1, lines.Length - 1).ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].Contains(':')) return;
            string card = lines[i].Split(':')[0];
            Console.WriteLine($"process {card}");
            Console.WriteLine($"Card copies {cardCopies[i]}");
            string numberString = lines[i].Split(':')[1];
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

            int max = i + matches > lines.Length ? lines.Length : i + matches;
            for (int j = i + 1; j <= max; j++)
            {
                cardCopies[j] += cardCopies[i];
                Console.WriteLine($"Increase CardCopies of Card {j+1} to {cardCopies[j]}");
            }
            
            Console.WriteLine(score);
        }
    }

    protected override string SolvePartOne()
    {
        return score.ToString();
    }

    protected override string SolvePartTwo()
    {
        return cardCopies.Sum().ToString();
    }
}

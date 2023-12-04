namespace AdventOfCode.Solutions.Year2023.Day04;

class Solution : SolutionBase
{
  private readonly int score = 0;
  private readonly int[] cardCopies;
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

      string[] split = lines[i]
          .Split(':')
          .Skip(1)
          .Select(s => s.Split('|'))
          .First();

      var winningNumbers = split[0].Split(" ")
          .Where(i => i != string.Empty)
          .Select(i => int.Parse(i)).ToList();
      var myNumbers = split[1].Split(" ")
          .Where(i => i != string.Empty)
          .Select(i => int.Parse(i)).ToList();

      int matches = winningNumbers
          .Count(winningNumber => myNumbers.Any(x => x == winningNumber));

      score += (int)Math.Pow(2, matches - 1);

      int max = i + matches > lines.Length ? lines.Length : i + matches;
      Enumerable.Range(i + 1, max - i).ToList().ForEach(j => cardCopies[j] += cardCopies[i]);
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
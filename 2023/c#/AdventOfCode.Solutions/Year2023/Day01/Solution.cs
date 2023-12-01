namespace AdventOfCode.Solutions.Year2023.Day01;

class Solution : SolutionBase
{
  public Solution() : base(01, 2023, "")
  {
  }

  protected override string SolvePartOne()
  {
    string[] lines = Input.Split(
      new string[] { Environment.NewLine },
      StringSplitOptions.None
    );

    int total = 0;
    
    foreach (var line in lines)
    {
      string numbers = new string(line.Where(char.IsDigit).ToArray());
      if (numbers.Length >= 1)
      {
        int result = Int32.Parse("" + numbers.First() + numbers.Last());
        total += result;
      }
     
    }
    return total.ToString();
  }

  protected override string SolvePartTwo()
  {
    string[] numberStrings = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    
    string[] lines = Input.Split(
      new [] { Environment.NewLine },
      StringSplitOptions.None
    );

    int total = 0;
    
    foreach (var line in lines)
    {
      List<int> numberList = new List<int>();
      for (int i = 0; i < line.Length; i++)
      {
        string substring = line.Substring(i, line.Length - i);
        string matchedNumber = numberStrings.FirstOrDefault(s => substring.StartsWith(s));

        if (matchedNumber != null)
        {
          numberList.Add(Array.IndexOf(numberStrings, matchedNumber) + 1);
        }
        else
        {
          if (int.TryParse(substring[0].ToString(), out _))
          {
            numberList.Add(Int32.Parse(substring[0].ToString()));
          }
        }
      }

      if (numberList.Count > 0)
      {
        int result = Int32.Parse(numberList.First().ToString() + numberList.Last().ToString());
        total += result;
      }
    }
    return total.ToString();
  }
}

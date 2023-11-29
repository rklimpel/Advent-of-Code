namespace AdventOfCode.Solutions.Year2022.Day01;

class Solution : SolutionBase
{
  readonly List<int> elfCalories = new();

  public Solution() : base(01, 2022, "")
  {
    string[] elfInventories = Input.Split("\n\n");
    foreach (var elfInventory in elfInventories)
    {
      string[] caloriesString = elfInventory.Split('\n');
      var validCalories = caloriesString.Where(s => !string.IsNullOrWhiteSpace(s));
      int totalCalories = validCalories.Select(int.Parse).Sum();
      elfCalories.Add(totalCalories);
    }
    elfCalories = elfCalories.OrderByDescending(i => i).ToList();
  }

  protected override string SolvePartOne()
  {
    int result = elfCalories.Take(1).Sum();
    return $"{result}";
  }

  protected override string SolvePartTwo()
  {
    int result = this.elfCalories.Take(3).Sum();
    return result.ToString();
  }

  private void PrintStringList(string[] stringList)
  {
    string formattedString = "[" + string.Join(", ", stringList) + "]";
    Console.WriteLine(formattedString);
  }
}

namespace AdventOfCode.Solutions.Year2023.Day15;

class Solution : SolutionBase
{
    List<int> hashValues = new();
    public Solution() : base(15, 2023, "")
    {
        string[] steps = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)[0]
            .Split(',').ToArray();

        foreach (var step in steps)
        {
            Console.WriteLine(step);
            var hashResult = Hash(step);
            hashValues.Add(hashResult);
            Console.WriteLine(hashResult);
        }

        // Console.WriteLine(Hash("HASH"));
    }

    protected override string SolvePartOne()
    {
        return hashValues.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    private int Hash(string step)
    {
        int currentValue = 0;
        for (int i = 0; i < step.Length; i++)
        {
            currentValue += (int)step[i];
            currentValue *= 17;
            currentValue = currentValue % 256;
        }
        return currentValue;
    }
}

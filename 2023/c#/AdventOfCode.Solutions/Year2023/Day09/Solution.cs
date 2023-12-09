using System.Windows.Markup;

namespace AdventOfCode.Solutions.Year2023.Day09;

class Solution : SolutionBase
{
    public EnvHistory[] History;

    public Solution() : base(09, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        History = lines
            .Where(l => l != "")
            .Select(l => new EnvHistory(l.Split(" ").Select(n => int.Parse(n)).ToArray()))
            .ToArray();

        foreach (var item in History) item.CalculateDifferences();
    }

    protected override string SolvePartOne()
    {
        return History.Select(h => h.CalculateFuture()).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return History.Select(h => h.CalculatePast()).Sum().ToString();
    }
}

class EnvHistory(int[] values)
{
    public int[] Values = values;
    private List<List<int>> differences = [];

    public void CalculateDifferences()
    {
        differences.Add(Values.Zip(Values.Skip(1), (a, b) => b - a).ToList());
        while (!differences.Last().All(v => v == 0))
        {
            differences.Add(differences.Last().Zip(differences.Last().Skip(1), (a, b) => b - a).ToList());
        }
    }

    public int CalculateFuture()
    {
        differences.Last().Add(0);
        for (int i = differences.Count - 2; i > -1; i--)
        {
            differences[i].Add(differences[i].Last() + differences[i + 1].Last());
        }

        Console.WriteLine(string.Join(", ", Values));
        PrintDifferences();
        Console.WriteLine("Future: " + (Values.Last() + differences[0].Last()));
        return Values.Last() + differences[0].Last();
    }

    public int CalculatePast()
    {
        Values = Values.Reverse().ToArray();
        foreach (var item in differences)
        {
            item.Reverse();
        }
        differences.Last().Add(0);
        for (int i = differences.Count - 2; i > -1; i--)
        {
            differences[i].Add(differences[i].Last() - differences[i + 1].Last());
        }
        Console.WriteLine(string.Join(", ", Values));
        PrintDifferences();
        Console.WriteLine("Future: " + (Values.Last() - differences[0].Last()));
        return Values.Last() - differences[0].Last();
    }

    private void PrintDifferences()
    {
        for (int i = 0; i < differences.Count; i++)
        {
            Console.WriteLine(string.Join(" ", differences[i].Select(x => x.ToString().PadLeft(2))));
        }
    }
}
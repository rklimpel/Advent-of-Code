namespace AdventOfCode.Solutions.Year2023.Day12;

class Solution : SolutionBase
{
    private List<Tuple<string, int[]>> springRows;
    private static Dictionary<string, long> cache = new();

    public Solution() : base(12, 2023, "")
    {
        springRows = Input
            .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => Tuple.Create(
                line.Split(" ")[0],
                line.Split(" ")[1].Split(",").Select(int.Parse).ToArray()
            ))
            .ToList();
    }

    protected override string SolvePartOne()
    {
        return springRows.Sum(springRow => CountSolutions($"{springRow.Item1}.", springRow.Item2)).ToString();
    }

    protected override string SolvePartTwo()
    {
        return springRows.Sum(springRow => CountSolutions(
                    $"{string.Join("?", Enumerable.Repeat(springRow.Item1, 5))}.",
                    Enumerable.Repeat(springRow.Item2, 5).SelectMany(x => x).ToArray())
                ).ToString();
    }

    private long CountSolutions(string springRow, int[] groupSizes, int currentGroup = 0)
    {
        string key = $"{springRow}{string.Join(",", groupSizes)}{currentGroup}";
        if (cache.TryGetValue(key, out long cachedResult)) return cachedResult;
        if (springRow == "") return groupSizes.Length == 0 && currentGroup == 0 ? 1 : 0;
        long branchSolutions = 0;
        string[] possible = springRow[0] == '?' ? new[] { ".", "#" } : new[] { springRow[0].ToString() };
        foreach (string symbol in possible)
        {
            if (symbol == "." && currentGroup == 0)
            {
                branchSolutions += CountSolutions(springRow.Substring(1), groupSizes);
            }
            else if (symbol == "." && currentGroup > 0 && groupSizes.Length > 0 && groupSizes[0] == currentGroup)
            {
                branchSolutions += CountSolutions(springRow.Substring(1), groupSizes.Skip(1).ToArray());
            }
            else if (symbol == "#")
            {
                branchSolutions += CountSolutions(springRow.Substring(1), groupSizes, currentGroup + 1);
            }
        }
        cache[key] = branchSolutions;
        return branchSolutions;
    }
}
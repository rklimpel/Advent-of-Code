namespace AdventOfCode.Solutions.Year2023.Day12;

class Solution : SolutionBase
{
    private List<Tuple<string, int[]>> springRows;

    public Solution() : base(12, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        springRows = lines
            .Where(l => l != "")
            .Select(l => Tuple.Create(
                l.Split(" ")[0],
                l.Split(" ")[1].Split(",").Select(int.Parse).ToArray())
            )
            .ToList();
    }

    protected override string SolvePartOne()
    {
        long sum = 0;
        foreach (var springRow in springRows)
        {
            long solutions = CountSolutions(springRow.Item1 + ".", springRow.Item2, 0);
            sum += solutions;
            //Console.WriteLine($"{springRow.Item1.ToString().PadRight(25)} -> {string.Join(", ", springRow.Item2).PadRight(20)} -> has {solutions.ToString().PadLeft(3)} solutions");
        }
        Console.WriteLine("Part 1 done.");
        return sum.ToString();
    }

    protected override string SolvePartTwo()
    {
        long sum = 0;
        foreach (var springRow in springRows)
        {
            string expandedSpringRow = string.Join("?", Enumerable.Repeat(string.Join(",", springRow.Item1), 5));
            int[] expandedGroupSizes = Enumerable.Repeat(springRow.Item2, 5).SelectMany(x => x).ToArray();
            //Console.WriteLine(expandedSpringRow);
            //Console.WriteLine(string.Join(", ", expandedGroupSizes));
            long solutions = CountSolutions(expandedSpringRow + ".", expandedGroupSizes, 0);
            sum += solutions;
            //Console.WriteLine($"{springRow.Item1.ToString().PadRight(25)} -> {string.Join(", ", springRow.Item2).PadRight(20)} -> has {solutions.ToString().PadLeft(3)} solutions");
        }
        return sum.ToString();
    }

    private static Dictionary<string, long> cache = new();

    private long CountSolutions(string springRow, int[] groupSizes, int currentGroup = 0)
    {
        string key = $"{springRow}_{string.Join(",", groupSizes)}_{currentGroup}";
        if (cache.TryGetValue(key, out long cachedResult))
        {
            return cachedResult;
        }

        //Wenn Symbol String leer, und keine Gruppe unfinished, dann biste fertig und hast ne Lösung
        if (springRow == "") return groupSizes.Length == 0 && currentGroup == 0 ? 1 : 0;

        long branchSolutions = 0;

        // Falls ? geht . und #, ansonsten nimm das was eh schon da steht
        string[] possible = springRow[0] == '?' ? new[] { ".", "#" } : new[] { springRow[0].ToString() };

        foreach (string symbol in possible)
        {
            // . Einlesen und es war keine Gruppe offen -> Einfach weiter machen
            if (symbol == "." && currentGroup == 0)
            {
                branchSolutions += CountSolutions(springRow.Substring(1), groupSizes);
            }
            // . Einlesen und Gruppe war noch offen -> Gruppe abschließen und entfernen und weiter machen
            else if (symbol == "." && currentGroup > 0 && groupSizes.Length > 0 && groupSizes[0] == currentGroup)
            {
                branchSolutions += CountSolutions(springRow.Substring(1), groupSizes.Skip(1).ToArray());
            }
            // # Einlesen -> Aktuelle Gruppe erweitern und weiter machen 
            else if (symbol == "#")
            {
                branchSolutions += CountSolutions(springRow.Substring(1), groupSizes, currentGroup + 1);
            }
        }

        cache[key] = branchSolutions;

        return branchSolutions;
    }

}


class TupleComparer<T1, T2, T3> : IEqualityComparer<Tuple<T1, T2, T3>>
{
    public bool Equals(Tuple<T1, T2, T3> x, Tuple<T1, T2, T3> y)
    {
        return EqualityComparer<T1>.Default.Equals(x.Item1, y.Item1)
            && EqualityComparer<T2>.Default.Equals(x.Item2, y.Item2)
            && EqualityComparer<T3>.Default.Equals(x.Item3, y.Item3);
    }

    public int GetHashCode(Tuple<T1, T2, T3> obj)
    {
        int hashCode = EqualityComparer<T1>.Default.GetHashCode(obj.Item1);
        hashCode = (hashCode * 397) ^ EqualityComparer<T2>.Default.GetHashCode(obj.Item2);
        hashCode = (hashCode * 397) ^ EqualityComparer<T3>.Default.GetHashCode(obj.Item3);
        return hashCode;
    }
}
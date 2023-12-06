namespace AdventOfCode.Solutions.Year2023.Day06;

class Solution : SolutionBase
{
    List<List<long>> WinStrategies = [];
    List<long> part2WinStrategies = [];

    public Solution() : base(06, 2023, "")
    {

        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );

        int[] times = lines[0]
            .Split(":")
            .Skip(1)
            .First()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        int[] distances = lines[1]
            .Split(":")
            .Skip(1)
            .First()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        Console.WriteLine("Time values: " + string.Join(", ", times));
        Console.WriteLine("Distance values: " + string.Join(", ", distances));

        WinStrategies = times.Select((time, index) => GetWinStrategies(time, distances[index])).ToList();


        long part2Time = long.Parse(string.Join("", times));
        long part2Distance = long.Parse(string.Join("", distances));

        Console.WriteLine("Part 2 Time: " + part2Time);
        Console.WriteLine("Part 2 Distnace: " + part2Distance);

        part2WinStrategies = GetWinStrategies(part2Time, part2Distance);
    }

    private List<long> GetWinStrategies(long raceTime, long raceDistance)
    {
        List<long> successfullPressDurations = new();
        for (long pressDuration = 0; pressDuration < raceTime; pressDuration++)
        {
            long speed = pressDuration;
            long remainingTime = raceTime - pressDuration;
            long distanceTraveled = remainingTime * speed;
            if (distanceTraveled > raceDistance)
            {
                successfullPressDurations.Add(pressDuration);
            }
        }
        return successfullPressDurations;
    }

    protected override string SolvePartOne()
    {
        int result = WinStrategies
            .Select(innerList => innerList.Count()).ToList()
            .Aggregate((currentProduct, nextSum) => currentProduct * nextSum);
        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        return part2WinStrategies.Count().ToString();
    }
}

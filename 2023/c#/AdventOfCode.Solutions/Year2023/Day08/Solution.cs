using System.Numerics;

namespace AdventOfCode.Solutions.Year2023.Day08;

class Solution : SolutionBase
{
    Dictionary<String, Node> nodes = new();
    string directions;

    public Solution() : base(08, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        directions = lines[0];
        nodes = ParseNodes(lines.Skip(2).Where(l => l != "").ToArray());
    }

    private Dictionary<string, Node> ParseNodes(string[] input)
    {
        return input
            .Select(line =>
            {
                var parts = line.Split('=');
                var id = parts[0].Trim();
                var values = parts[1]
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(',')
                    .Select(value => value.Trim())
                    .ToArray();
                return new Node(id, values[0], values[1]);
            })
            .ToDictionary(n => n.Id);
    }

    protected override string SolvePartOne()
    {
        string currentNode = "AAA";
        int directionPosition = 0;
        long steps = 0;
        while (currentNode != "ZZZ")
        {
            currentNode = directions[directionPosition] == 'L' ? nodes[currentNode].Left : nodes[currentNode].Right;
            steps += 1;
            directionPosition += 1;
            if (directionPosition > directions.Length - 1) directionPosition = 0;
        }
        return steps.ToString();
    }

    protected override string SolvePartTwo()
    {
        string[] currentNodes = nodes
            .Where(pair => pair.Key.EndsWith("A"))
            .Select(pair => pair.Key)
            .ToArray();

        long[] minimumSteps = currentNodes.Select((node, i) =>
            {
                int directionPosition = 0;
                return (long)Enumerable.Range(0, int.MaxValue)
                    .TakeWhile(_ => !node.EndsWith("Z"))
                    .Select(_ => node = directions[directionPosition++ % directions.Length] == 'L' ? 
                            nodes[node].Left : nodes[node].Right)
                    .Count();
            }).ToArray();

        return LCM(minimumSteps).ToString();
    }

    private long GCD(long a, long b) => (a == 0) ? b : GCD(b % a, a);
    private long LCM(long[] numbers) => numbers.Aggregate((a, b) => a * b / GCD(a, b));

}

class Node(string id, string left, string right)
{
    public string Id = id;
    public string Left = left;
    public string Right = right;

    public override string ToString()
    {
        return $"Node(Id: {Id}, Left: {Left}, Right: {Right})";
    }
}
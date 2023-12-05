namespace AdventOfCode.Solutions.Year2023.Day05;

class Solution : SolutionBase
{
    public List<TransformationInfo> transformationInfos = new();
    public List<Item> itemsTask1 = new();

    public List<(long, long)> itemsTask2 = new();

    public Solution() : base(05, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );

        itemsTask1 = lines[0]
          .Split(':')
          .Skip(1)
          .Select(s => s.Split(' '))
          .First()
          .Where(s => s != string.Empty)
          .Select(i => new Item("seed", long.Parse(i)))
          .ToList();

        var tmp = lines[0]
          .Split(':')
          .Skip(1)
          .Select(s => s.Split(' '))
          .First()
          .Where(s => s != string.Empty)
          .Select(long.Parse)
          .ToList();

        for (int i = 0; i < tmp.Count; i += 2)
        {
            itemsTask2.Add((tmp[i], tmp[i] + tmp[i + 1]));
        }


        TransformationInfo? transformationInfo = null;
        for (int i = 1; i < lines.Length; i++)
        {
            var currentLine = lines[i];
            Console.WriteLine($"process line: {currentLine}");

            if (currentLine == "\n" || currentLine == "")
            {
                if (transformationInfo != null)
                {
                    transformationInfos.Add(transformationInfo);
                    transformationInfo = null;
                }
            }
            else if (currentLine.Contains(':'))
            {
                var fromTo = currentLine
                    .Split(" ")
                    .First()
                    .Split('-')
                    .Where(s => s != "to")
                    .ToList();
                transformationInfo = new TransformationInfo(fromTo[0], fromTo[1]);
            }
            else
            {
                var numbers = currentLine
                    .Split(" ")
                    .Select(long.Parse)
                    .ToList();
                var destinationRangeStart = numbers[0];
                var sourceRangeStart = numbers[1];
                var rangeLength = numbers[2];

                transformationInfo!.AddMapEntries(
                    destinationRangeStart,
                    sourceRangeStart,
                    rangeLength
                );
            }
        }
    }

    protected override string SolvePartOne()
    {
        foreach (var item in itemsTask1)
        {
            while (item.Type != "location")
            {
                //Console.WriteLine($"Search for transformation Infos for type {item.Type}");
                var transformationInfo = transformationInfos
                    .Where(t => t.from == item.Type)
                    .ToList().First();

                item.Id = transformationInfo.Apply(item.Id);
                //Console.WriteLine($"Transformed {item.Type} to {transformationInfo.to}");
                item.Type = transformationInfo.to;
            }
        }

        return itemsTask1.Select(i => i.Id).ToList().Min().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "x";
        Item item = new Item("location", 0);
        while (item.Type != "seed")
        {

            //Console.WriteLine($"Search for transformation Infos for type {item.Type}");
            var transformationInfo = transformationInfos
                .Where(t => t.to == item.Type)
                .ToList().First();

            item.Id = transformationInfo.Apply(item.Id);
            //Console.WriteLine($"Transformed {item.Type} to {transformationInfo.to}");
            item.Type = transformationInfo.to;
        }

        return itemsTask1.Select(i => i.Id).ToList().Min().ToString();

    }
}

class TransformationInfo(string from, string to)
{
    public string from = from;
    public string to = to;

    public List<(long, long, long)> map = [];

    public void AddMapEntries(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        map.Add((
            sourceRangeStart,
            sourceRangeStart + rangeLength,
            destinationRangeStart - sourceRangeStart
        ));
    }

    public long Apply(long number, bool reverse = false)
    {
        var info = map.Find(x => x.Item1 <= number && x.Item2 >= number);
        if (info == (0, 0, 0))
        {
            return number;
        }
        else
        {
            if (reverse)
            {
                return number - info.Item3;
            }
            else
            {
                return number + info.Item3;
            }
        }
    }

    public string PrettyPrint()
    {
        return $"TransformationInfo from:{from} to:{to} entries:{map.Count}";
    }
}

class Item(string type, long id)
{
    public string Type = type;
    public long Id = id;
}
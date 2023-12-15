using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Channels;

namespace AdventOfCode.Solutions.Year2023.Day15;

class Solution : SolutionBase
{
    string[] steps;
    public Solution() : base(15, 2023, "")
    {
        steps = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)[0]
            .Split(',').ToArray();

        // Console.WriteLine(Hash("HASH"));
    }

    protected override string SolvePartOne()
    {
        List<int> hashValues = new();
        foreach (var step in steps)
        {
            //Console.WriteLine(step);
            var hashResult = Hash(step);
            hashValues.Add(hashResult);
            //Console.WriteLine(hashResult);
        }
        return hashValues.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        List<string>[] boxes = new List<string>[256];
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new List<string>();
        }

        foreach (var step in steps)
        {
            if (step.Contains('='))
            {
                string label = step.Split('=')[0];
                int boxId = Hash(label);
                string focalLength = step.Split('=')[1];


                if (boxes[boxId].Where(x => x.Contains(label)).ToList().Count() == 0)
                {
                    boxes[boxId].Add($"{label} {focalLength}");
                }
                else
                {
                    for (int i = 0; i < boxes[boxId].Count(); i++)
                    {
                        if (boxes[boxId][i].Contains(label))
                        {
                            boxes[boxId][i] = $"{label} {focalLength}";
                        }
                    }
                }

            }
            else
            {
                var label = step.Split('-')[0];
                int boxId = Hash(label);
                if (boxes[boxId].Where(x => x.Contains(label)).ToList().Count() != 0)
                {
                    for (int i = 0; i < boxes[boxId].Count(); i++)
                    {
                        if (boxes[boxId][i].Contains(label))
                        {
                            boxes[boxId].Remove(boxes[boxId][i]);
                        }
                    }
                }
            }
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            Console.WriteLine($"Box {i}: {string.Join(", ", boxes[i].Select(x => $"[{x}] "))}");
        }

        long focusPower = 0;
        for (int boxId = 0; boxId < boxes.Length; boxId++)
        {
            for (int slotId = 0; slotId < boxes[boxId].Count; slotId++)
            {
                int focalLength = int.Parse(boxes[boxId][slotId].Split(" ")[1]);
                int lensFocusPower = (boxId + 1) * (slotId + 1) * focalLength;
                focusPower += lensFocusPower;
            }
        }


        return focusPower.ToString();
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

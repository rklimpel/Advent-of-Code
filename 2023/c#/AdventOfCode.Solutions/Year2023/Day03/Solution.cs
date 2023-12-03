using System.Security.Cryptography;

namespace AdventOfCode.Solutions.Year2023.Day03;

class Solution : SolutionBase
{
    private List<int> numbers = new();
    public Solution() : base(03, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );

        for (int i = 0; i < lines.Length; i++)
        {
            bool isReadingNumber = false;
            string number = "";
            (int, int) numberStart = (-1,-1);
            (int, int) numberEnd = (-1,-1);
            
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (char.IsDigit(lines[i][j]))
                {
                    if (!isReadingNumber)
                    {
                        number += lines[i][j];
                        numberStart = (i, j);
                        isReadingNumber = true;
                    }
                    else
                    {
                        number += lines[i][j];
                    }
                }
                else
                {
                    if (isReadingNumber)
                    {
                        numberEnd = (i, j - 1);
                        isReadingNumber = false;
                        // Check number has symbol around and add number to array if yes
                        if (CheckNumberHasSymbolAround(lines, numberStart, numberEnd))
                        {
                            numbers.Add(int.Parse(number));
                            Console.WriteLine(number + " Added to list.");
                        }
                        else
                        {
                            Console.WriteLine(number + " has no symbol around.");
                        }
                        
                        numberStart = (-1, -1);
                        numberEnd = (-1, -1);
                        number = "";
                    }
                }
            }
            
            if (isReadingNumber)
            {
                numberEnd = (i, lines[i].Length - 1);
                isReadingNumber = false;
                // Check number has symbol around and add number to array if yes
                if (CheckNumberHasSymbolAround(lines, numberStart, numberEnd))
                {
                    numbers.Add(int.Parse(number));
                    Console.WriteLine(number + " Added to list.");
                }
                else
                {
                    Console.WriteLine(number + " has no symbol around.");
                }
                        
                numberStart = (-1, -1);
                numberEnd = (-1, -1);
                number = "";
            }
        }
    }

    private bool CheckNumberHasSymbolAround(string[] grid, (int, int) numberStart, (int, int) numberEnd)
    {
        for (int i = numberStart.Item1 - 1; i <= numberEnd.Item1 + 1; i++)
        {
            for (int j = numberStart.Item2 - 1; j <= numberEnd.Item2 + 1; j++)
            {
                if (i > 0 && j > 0 && i < grid.Length && j < grid[i].Length)
                {
                    if (!char.IsDigit(grid[i][j]) && grid[i][j] != '.')
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    protected override string SolvePartOne()
    {
        return numbers.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}

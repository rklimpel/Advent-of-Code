using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day07;

class Solution : SolutionBase
{
    List<BidHand> bidHands = [];
    public static Dictionary<char, int> cardStrength = new Dictionary<char, int>
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'J', 11},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2}
    };
    Dictionary<char, int> cardStrengthWithJoker = cardStrength
        .ToDictionary(pair => pair.Key, pair => pair.Key == 'J' ? 1 : pair.Value);

    public Solution() : base(07, 2023, "")
    {
        string[] lines = Input.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );
        bidHands.AddRange(lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(" "))
            .Select(parts => new BidHand(parts[0], int.Parse(parts[1])))
        );
    }

    private int CompareHands(string handA, string handB)
    {
        int result = GetHandValue(handA).CompareTo(GetHandValue(handB));
        if (result == 0)
        {
            result = handA
                .Zip(handB, (a, b) => cardStrength[a].CompareTo(cardStrength[b]))
                .FirstOrDefault(compResult => compResult != 0);
        }
        return result;
    }

    private int CompareHandsWithJoker(string handA, string handB)
    {
        if (GetHandValueWithJoker(handA) > GetHandValueWithJoker(handB)) return -1;
        else if (GetHandValueWithJoker(handA) < GetHandValueWithJoker(handB)) return 1;
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (cardStrengthWithJoker[handA[i]] > cardStrengthWithJoker[handB[i]]) return -1;
                else if (cardStrengthWithJoker[handA[i]] < cardStrengthWithJoker[handB[i]]) return 1;
            }
        }
        return 0;
    }

    private double GetHandValue(string hand)
    {
        var groupCounts = hand
            .GroupBy(c => c)
            .Select(g => g.Count())
            .OrderByDescending(x => x)
            .ToList();
        double maxCount = groupCounts[0];
        if ((maxCount == 3 || maxCount == 2) && groupCounts[1] == 2) maxCount += 0.5;
        return maxCount;
    }

    private double GetHandValueWithJoker(string hand)
    {
        int jokerCount = hand.Where(c => c == 'J').Count();
        var groupCounts = hand
            .GroupBy(c => c)
            .Where(c => c.Key != 'J')
            .Select(g => g.Count())
            .OrderByDescending(x => x)
            .ToList();
        if (groupCounts.Count == 0) return jokerCount;
        double maxCount = groupCounts[0] + jokerCount;
        if ((maxCount == 3 || maxCount == 2) && groupCounts[1] == 2) maxCount += 0.5;
        return maxCount;
    }

    protected override string SolvePartOne()
    {
        bidHands.Sort((x, y) => CompareHands(x.hand, y.hand));
        bidHands.Reverse();
        return bidHands
            .Select((bidHand, index) => (index + 1) * bidHand.bid)
            .Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        bidHands.Sort((x, y) => CompareHandsWithJoker(x.hand, y.hand));
        bidHands.Reverse();
        return bidHands
            .Select((bidHand, index) => (index + 1) * bidHand.bid)
            .Sum().ToString();
    }
}

class BidHand(string hand, int bid)
{
    public string hand = hand;
    public int bid = bid;
}
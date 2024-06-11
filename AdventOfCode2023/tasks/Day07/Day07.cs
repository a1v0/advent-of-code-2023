namespace AdventOfCode2023;

public class Day07 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day07Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day07Task2();
}

public class Day07Task1 : BaseTask
{
    public override string Solve()
    {
        int winnings = GetTotalWinnings();
        return winnings.ToString();
    }

    private int GetTotalWinnings()
    {
        int totalWinnings = 0;
        int rank = 1;

        foreach (Hand hand in Hands)
        {
            int winnings = rank * hand.Bid;
            totalWinnings += winnings;

            ++rank;
        }

        return totalWinnings;
    }

    private Hand[]? _hands;
    private Hand[] Hands
    {
        get
        {
            _hands ??= GetHands();
            return _hands;
        }
    }

    protected virtual Hand[] GetHands()
    {
        var hands = new Hand[InputRows.Length];

        for (int i = 0; i < hands.Length; ++i)
        {
            string currentRow = InputRows[i];
            hands[i] = new Hand(currentRow);
        }

        Array.Sort(hands);

        return hands;
    }
}

public class Day07Task2 : Day07Task1
{
    protected override Hand[] GetHands()
    {
        var hands = new Hand[InputRows.Length];

        for (int i = 0; i < hands.Length; ++i)
        {
            string currentRow = InputRows[i];
            hands[i] = new Hand(currentRow, 2);
        }

        Array.Sort(hands);

        return hands;
    }
}

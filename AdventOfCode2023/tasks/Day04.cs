namespace AdventOfCode2023;

public class Day04 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day04Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day04Task2();
}

public class Day04Task1 : BaseTask
{
    public override string Solve()
    {
        int[] pointsForAllCards = GetPoints();
        int totalPoints = pointsForAllCards.Sum();
        return totalPoints.ToString();
    }

    private int[] GetPoints()
    {
        (List<int>, List<int>)[] cards = GetCards(); // maybe make this into a property
        // split into list of winning numbers and actual numbers
        // - array of tuples?
        // loop over winning numbers per day to find amount of winning numbers
        // calculate points based on amount of winning numbers
    }

    private (List<int>, List<int>)[] GetCards()
    {
        string[] inputRows = GetInputRows();
        var cards = new (List<int>, List<int>)[inputRows.Length];

        for (int i = 0; i < inputRows.Length; ++i)
        {
            string row = inputRows[i];
            (List<int>, List<int>) parsedRow = ParseRow(row);
            cards[i] = parsedRow;
        }

        return cards;
    }

    private (List<int>, List<int>) ParseRow(string row)
    {
        int cardNameLength = 10; // e.g. "Card   1: "
        string rowWithoutCarName = row[cardNameLength..];
        string[] rowHalves = rowWithoutCarName.Split(" | ");
        List<int> winningNumbers = ExtractNumbers(rowHalves[0]);
        List<int> selectedNumbers = ExtractNumbers(rowHalves[1]);
        return (winningNumbers, selectedNumbers);
    }

    private List<int> ExtractNumbers(string set)
    {
        var extractedNumbers = new List<int>();

        string[] setContents = set.Split(" ");
        foreach (string setItem in setContents)
        {
            bool isValidItem = setItem.Length > 0;
            if (!isValidItem) continue;

            int number = int.Parse(setItem);
            extractedNumbers.Add(number);
        }

        return extractedNumbers;
    }

    private (List<int>, List<int>)[] Cards { get; } = GetCards();
}

public class Day04Task2 : Day04Task1
{ }

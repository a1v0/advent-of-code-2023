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
        double[] pointsForAllCards = GetPoints();
        double totalPoints = pointsForAllCards.Sum();
        return totalPoints.ToString();
    }

    private double[] GetPoints()
    {
        var points = new List<double>();
        (List<int>, List<int>)[] cards = GetCards(); // maybe make this into a property

        foreach (var card in cards)
        {
            int amountOfWinningNumbers = CountWinningNumbers(card);
            if (amountOfWinningNumbers == 0) continue;

            int exponent = amountOfWinningNumbers - 1;
            double pointsInCard = Math.Pow(2, exponent);
            points.Add(pointsInCard);
        }

        return points.ToArray();
    }

    private static int CountWinningNumbers((List<int> winningNumbers, List<int> selectedNumbers) card)
    {
        int winningNumbersCount = 0;

        foreach (int winningNumber in card.winningNumbers)
        {
            bool winningNumberSelected = card.selectedNumbers.Contains(winningNumber);
            if (winningNumberSelected) ++winningNumbersCount;
        }

        return winningNumbersCount;
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
        string[] cardNameAndContent = row.Split(": ");
        string rowWithoutCardName = cardNameAndContent[1];
        string[] rowHalves = rowWithoutCardName.Split(" | ");
        List<int> winningNumbers = ExtractNumbers(rowHalves[0]);
        List<int> selectedNumbers = ExtractNumbers(rowHalves[1]);
        return (winningNumbers, selectedNumbers);
    }

    private static List<int> ExtractNumbers(string set)
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

    (List<int>, List<int>)[]? _cards;
    private (List<int>, List<int>)[] Cards
    {
        get
        {
            if (_cards is null)
            {
                _cards = GetCards();
            }
            return _cards;
        }
    }
}

public class Day04Task2 : Day04Task1
{
    public override string Solve()
    {
        // create properties:
        // - dictionary WinningNumbers (maybe find better name) int: card number, int: number of winning numbers
        // - dictionary CardsHeld int: card number, int: quantity held (default 0)
        // populate WinningNumbers
        // loop through CardsHeld
        // - for every card held, add a suitable amount of additional cards (depending on number of winning numbers)
        // - don't add any beyond end of cards list
        // add up and stringify CardsHeld
    }

    Dictionary<int, int>? _winningNumbers;
    private Dictionary<int, int> WinningNumbers
    {
        get
        {
            if (_winningNumbers is null)
            {
                _winningNumbers = GetWinningNumbers();
            }
            return _winningNumbers;
        }
    }
}

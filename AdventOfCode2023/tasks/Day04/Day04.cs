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

    protected static int CountWinningNumbers((List<int> winningNumbers, List<int> selectedNumbers) card)
    {
        int winningNumbersCount = 0;

        foreach (int winningNumber in card.winningNumbers)
        {
            bool winningNumberSelected = card.selectedNumbers.Contains(winningNumber);
            if (winningNumberSelected) ++winningNumbersCount;
        }

        return winningNumbersCount;
    }

    protected (List<int>, List<int>)[] GetCards()
    {
        var cards = new (List<int>, List<int>)[InputRows.Length];

        for (int i = 0; i < InputRows.Length; ++i)
        {
            string row = InputRows[i];
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
            _cards ??= GetCards();
            return _cards;
        }
    }
}

public class Day04Task2 : Day04Task1
{
    public override string Solve()
    {
        ProcessAllCards();
        int[] finalCardQuantities = GetCardQuantities();
        int totalCards = finalCardQuantities.Sum();
        return totalCards.ToString();
    }

    private void ProcessAllCards()
    {
        for (int i = 1; i <= CardsHeld.Count; ++i)
        {
            int quantityHeld = CardsHeld[i];
            for (int j = 0; j < quantityHeld; ++j)
            {
                // This second loop doesn't feel ideal to me.
                // I could probably do it another way.

                ProcessCard(i);
            }
        }
    }

    private void ProcessCard(int cardNumber)
    {
        int maxCardNumber = CardsHeld.Count;
        int winningNumbers = WinningNumbers[cardNumber];
        int upperBound = cardNumber + winningNumbers;
        if (upperBound > maxCardNumber) upperBound = maxCardNumber;

        for (int i = cardNumber + 1; i <= upperBound; ++i)
        {
            ++CardsHeld[i];
        }
    }

    private int[] GetCardQuantities()
    {
        int[] cardQuantities = new int[CardsHeld.Count];
        for (int i = 0; i < CardsHeld.Count; ++i)
        {
            cardQuantities[i] = CardsHeld[i + 1];
        }
        return cardQuantities;
    }

    private Dictionary<int, int> GetCardsHeld()
    {
        var cardsHeld = new Dictionary<int, int>();
        int numberOfCards = InputRows.Length;

        for (int i = 1; i <= numberOfCards; ++i)
        {
            cardsHeld[i] = 1;
        }

        return cardsHeld;
    }

    private Dictionary<int, int> GetWinningNumbers()
    {
        (List<int>, List<int>)[] cards = GetCards();
        var winningNumbers = new Dictionary<int, int>();
        int currentCard = 1;

        foreach (var card in cards)
        {
            int amountOfWinningNumbers = CountWinningNumbers(card);
            winningNumbers[currentCard] = amountOfWinningNumbers;
            ++currentCard;
        }

        return winningNumbers;
    }

    Dictionary<int, int>? _cardsHeld;
    private Dictionary<int, int> CardsHeld
    {
        get
        {
            _cardsHeld ??= GetCardsHeld();
            return _cardsHeld;
        }
    }

    Dictionary<int, int>? _winningNumbers;
    private Dictionary<int, int> WinningNumbers
    {
        get
        {
            _winningNumbers ??= GetWinningNumbers();
            return _winningNumbers;
        }
    }
}

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

public class Hand : IComparable
{
    public Hand(string handInput, byte task = 1)
    {
        string[] elements = handInput.Split(' ');
        _cards = elements[0];
        _bid = int.Parse(elements[1]);
        _task = task;
        _type = IdentifyType();
    }

    private readonly byte _task;
    private byte Task
    {
        get
        {
            return _task;
        }
    }

    private readonly byte _type;
    private byte Type
    {
        get
        {
            return _type;
        }
    }

    private readonly int _bid;
    public int Bid
    {
        get
        {
            return _bid;
        }
    }

    private readonly string _cards;
    public string Cards
    {
        get
        {
            return _cards;
        }
    }

    private byte IdentifyType()
    {
        char[] cardsCopy = Cards.ToCharArray();
        Array.Sort(cardsCopy);
        string handCombination = GetHandCombination(cardsCopy);
        byte handType = GetType(handCombination);
        return handType;
    }

    private static byte GetType(string combination)
    {
        switch (combination)
        {
            case "5":
                return 6;
            case "41":
                return 5;
            case "32":
                return 4;
            case "311":
                return 3;
            case "221":
                return 2;
            case "2111":
                return 1;
            case "11111":
                return 0;
            default:
                throw new Exception($"Unknown hand combination encountered: {combination}.");
        }
    }

    private string GetHandCombination(char[] cards)
    {
        if (Task == 2)
        {
            cards = HandleJokers(cards);
        }

        var pattern = new List<byte>();

        char current = cards[0];
        byte counter = 0;

        for (int i = 0; i < cards.Length; ++i)
        {
            char card = cards[i];

            if (card == current)
            {
                ++counter;
                continue;
            }

            pattern.Add(counter);
            counter = 0;
            current = card;
            --i;
        }
        pattern.Add(counter); // ensures that the last card is counted

        // the end of this method feels super clunky
        // a refactor wouldn't go amiss, if I can think of anything...
        byte[] patternNumbers = pattern.ToArray();
        Array.Sort(patternNumbers);
        Array.Reverse(patternNumbers);
        string handCombination = string.Join("", patternNumbers);
        return handCombination;
    }

    private static char[] HandleJokers(char[] cards)
    {
        var quantities = new Dictionary<char, byte>();
        byte largestAmountOfSameCard = 0;
        char mostUsedCard = 'A'; // assigning here because input contains 'JJJJJ'

        foreach (char card in cards)
        {
            // the logic in here feels poorly written
            if (card == 'J') continue;

            if (quantities.ContainsKey(card))
            {
                ++quantities[card];
            }
            else
            {
                quantities[card] = 1;
            }

            if (quantities[card] > largestAmountOfSameCard)
            {
                largestAmountOfSameCard = quantities[card];
                mostUsedCard = card;
            }
        }

        char[] cardsWithoutJokers = ReplaceJokers(cards, mostUsedCard);

        Array.Sort(cardsWithoutJokers);

        return cardsWithoutJokers;
    }

    private static char[] ReplaceJokers(char[] cards, char mostUsedCard)
    {
        for (int i = 0; i < cards.Length; ++i)
        {
            char card = cards[i];
            if (card == 'J')
            {
                cards[i] = mostUsedCard;
            }
        }

        return cards;
    }

    public int CompareTo(object? comparison)
    {
        if (comparison is null) return 1;

        Hand hand = (Hand)comparison;
        if (Type > hand.Type) return 1;
        if (Type < hand.Type) return -1;

        return CompareHandWithSameType(hand);
    }

    private int CompareHandWithSameType(Hand hand)
    {
        string myCards = Cards,
        comparedCards = hand.Cards;

        if (Task == 2)
        {
            // this if statement might be redundant
            // 
            // 
            // 
            // check
            // 
            // 
            // 
            char[] myCardsWithoutJoker = HandleJokers(myCards.ToCharArray()),
                   comparedCardsWithoutJoker = HandleJokers(comparedCards.ToCharArray());
            myCards = new string(myCardsWithoutJoker);
            comparedCards = new string(comparedCardsWithoutJoker);
        }

        for (int i = 0; i < myCards.Length; ++i)
        {
            byte x = GetCardValue(myCards[i]),
                 y = GetCardValue(comparedCards[i]);

            if (x == y) continue;

            if (x > y) return 1;

            return -1;
        }

        return 0; // this line will never be executed, since the input contains no duplicate hands
    }

    private byte GetCardValue(char card)
    {
        switch (card)
        {
            case 'A':
                return 14;
            case 'K':
                return 13;
            case 'Q':
                return 12;
            case 'J':
                return Task == 2 ? (byte)1 : (byte)11;
            case 'T':
                return 10;
            case '9' or '8' or '7' or '6' or '5' or '4' or '3' or '2':
                return byte.Parse(card.ToString());
            default:
                throw new Exception($"Invalid card type received: {card}.");
        }
    }
}
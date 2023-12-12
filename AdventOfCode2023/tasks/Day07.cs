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
        // sort array
        // iterate over array to find total score
        // - maybe use an aggregator method, as a learning exercise
        // return as string
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

    private Hand[] GetHands()
    {
        var hands = new Hand[InputRows.Length];

        for (int i = 0; i < hands.Length; ++i)
        {
            string currentRow = InputRows[i];
            hands[i] = new Hand(currentRow);
        }

        return hands;
    }
}

public class Day07Task2 : Day07Task1
{ }

public class Hand : IComparable
{
    public Hand(string handInput)
    {
        string[] elements = handInput.Split(' ');
        _cards = elements[0];
        _bid = int.Parse(elements[1]);
        _type = IdentifyType();
    }

    private readonly byte _type;
    public byte Type
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
                throw new Exception("Unknown hand combination encountered.");
        }
    }

    private static string GetHandCombination(char[] cards)
    {
        var pattern = new List<byte>();

        char current = cards[0];
        byte counter = 0;

        foreach (char card in cards)
        {
            if (card == current)
            {
                ++counter;
                continue;
            }

            pattern.Add(counter);
            counter = 0;
            current = card;
        }

        // the end of this method feels super clunky
        // a refactor wouldn't go amiss, if I can think of anything...
        byte[] patternNumbers = pattern.ToArray();
        Array.Sort(patternNumbers);
        Array.Reverse(patternNumbers);
        string handCombination = string.Join("", patternNumbers);
        return handCombination;
    }

    public int CompareTo(object? comparison)
    {
        if (comparison is null) return 1;

        Hand hand = (Hand)comparison;
        // create sorting method to sort the array
        // - if a.type > b.type, a goes last
        // - a.type < b.type, a goes first
        // - a.type == b.type
        //   - great public static method on Hand class for comparing hands
        //   - map each card's value to an int via switch
        //     - if a letter, return corresponding number
        //     - else byte.Parse...
        //   - loop over numbers and compare
        if (Type > hand.Type) return 1;
        if (Type < hand.Type) return -1;

        return CompareHandWithSameType(hand);
    }

    private static byte GetCardValue(char card)
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
                return 11;
            case 'T':
                return 10;
            case '9' or '8' or '7' or '6' or '5' or '4' or '3' or '2':
                return byte.Parse(card.ToString());
            default:
                throw new Exception($"Invalid card type received: {card}.");
        }
    }
}
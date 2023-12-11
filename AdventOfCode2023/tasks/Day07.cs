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
        // parse input into array of custom Hand objects
        // - int bid
        // - string cards
        // - int type (strongest type is 6, weakest is 0)
        // create sorting method to sort the array
        // - if a.type > b.type, a goes last
        // - a.type < b.type, a goes first
        // - a.type == b.type, use some sort of switch statement method to assess which is greater
        // sort array
        // iterate over array to find total score
        // - maybe use an aggregator method, as a learning exercise
        // return as string
    }
}

public class Day07Task2 : Day07Task1
{ }

public class Hand
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

    private static string GetHandCombination(char[] cards)
    {
        // loop through characters
        // keep track of previous char
        // make counter variable
        // if char == previous, ++counter
        // else append counter to a list, reset counter, previous == current char
        // list to array, sort, to string <-- this is maybe inelegant
        // switch statement to return

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
}
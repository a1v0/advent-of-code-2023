namespace AdventOfCode2023;

public class Day09 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day09Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day09Task2();
}

public class Day09Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input as array of Sequences
        // create list of ints to store next values
        // - iterate through Sequences and find next value
        // - add to list
        // sum list
        // return as string
    }
}

public class Day09Task2 : Day09Task1
{ }

public class Sequence
{
    public Sequence(string input)
    {
        _contents = ParseInput(input);
        _nextValue = GetNextValue();
    }

    private static int[] ParseInput(string input)
    {
        string[] numbersText = input.Split(' ');
        int[] numbers = new int[numbersText.Length];

        for (int i = 0; i < numbers.Length; ++i)
        {
            numbers[i] = int.Parse(numbersText[i]);
        }

        return numbers;
    }

    private Sequence? Child
    {
        get;
        set;
    }

    private readonly int _nextValue;
    private int NextValue
    {
        get
        {
            return _nextValue;
        }
    }

    private readonly int[] _contents;
    private int[] Contents
    {
        get
        {
            return _contents;
        }
    }
}

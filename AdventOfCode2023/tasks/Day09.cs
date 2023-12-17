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
        int[] extrapolatedValues = GetExtrapolatedValues();
        int sumOfExtrapolatedValues = extrapolatedValues.Sum();
        return sumOfExtrapolatedValues.ToString();
    }

    private Sequence[]? _sequences;
    private Sequence[] Sequences
    {
        get
        {
            _sequences ??= GetSequences();
            return _sequences;
        }
    }

    private Sequence[] GetSequences()
    {
        var sequences = new Sequence[InputRows.Length];

        for (int i = 0; i < sequences.Length; ++i)
        {
            sequences[i] = new Sequence(InputRows[i]);
        }

        return sequences;
    }

    private int[] GetExtrapolatedValues()
    {
        int[] values = new int[Sequences.Length];

        for (int i = 0; i < values.Length; ++i)
        {
            values[i] = Sequences[i].NextValue;
        }

        return values;
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
    public Sequence(int[] input)
    {
        _contents = input;
        _nextValue = GetNextValue();
    }

    private bool? _needsChild;
    private bool NeedsChild
    {
        get
        {
            _needsChild ??= IsChildSequenceNeeded();
            return (bool)_needsChild; // The case was added because the compiler told me to, though I'm surprised. I've not needed to make such casts before
        }
    }

    private bool IsChildSequenceNeeded()
    {
        foreach (int value in Contents)
        {
            if (value != 0) return true;
        }

        return false;
    }

    private int GetNextValue()
    {
        // this needs to contain all the logic to check difference between values etc.
        // 
        // check if child sequence is needed (i.e. validate whether all values are 0)
        // - this might actually make a good Boolean property
        // if no child is needed, i.e. if all values are 0, return 0
        // if child is null and child is needed, create a child
        // - create int[] containing differences between values
        // - create new sequence and set it as child property
        // once child is created, child will run its own GetNextValue
        // run "last member of sequence + child's next value" and return
        // 
        // 
        // 
        // 
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
    public int NextValue
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

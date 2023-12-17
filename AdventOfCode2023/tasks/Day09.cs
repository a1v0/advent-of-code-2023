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
    protected Sequence[] Sequences
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

    protected virtual int[] GetExtrapolatedValues()
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
{
    protected override int[] GetExtrapolatedValues()
    {
        int[] values = new int[Sequences.Length];

        for (int i = 0; i < values.Length; ++i)
        {
            values[i] = Sequences[i].PreviousValue;
        }

        return values;
    }
    // seems straightforward enough
    // imitate functionality of NextValue using new property, GetPreviousValue
    // the rest should work in more or less the same way, just with subtraction instead of addition
    // might need to tweak the logic in all sorts of place for this to work
}

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
            return (bool)_needsChild; // The case was added because the compiler told me to, though I'm surprised. I've not needed to make such casts before. Why can other types implicitly convert and not `bool`?
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
        if (!NeedsChild) return 0;

        Child = CreateChild();
        int nextValue = GenerateNextValue();
        return nextValue;
    }

    private Sequence CreateChild()
    {
        int[] differences = GetDifferences();
        var child = new Sequence(differences);
        return child;
    }

    private int[] GetDifferences()
    {
        int[] differences = new int[Contents.Length - 1];

        for (int i = 0; i < differences.Length; ++i)
        {
            int firstNumber = Contents[i],
                secondNumber = Contents[i + 1];
            int difference = secondNumber - firstNumber;

            differences[i] = difference;
        }

        return differences;
    }

    private int GenerateNextValue()
    {
        if (Child is null) throw new Exception("Child is null. If this error is thrown, there is a logical flaw in the code.");
        return Contents[^1] + Child.NextValue;
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

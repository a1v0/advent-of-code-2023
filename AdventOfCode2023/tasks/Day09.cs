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
    // imitate functionality of NextValue using new property, GetPreviousValue
    // the rest should work in more or less the same way, just with subtraction instead of addition
    // might need to tweak the logic in all sorts of place for this to work
}

public class Sequence
{
    public Sequence(string input)
    {
        _contents = ParseInput(input);
        _needsChild = IsChildSequenceNeeded();
        if (NeedsChild) Child = CreateChild();
        _nextValue = GetNextValue();
        _previousValue = GetPreviousValue();
    }

    public Sequence(int[] input)
    {
        _contents = input;
        _needsChild = IsChildSequenceNeeded();
        if (NeedsChild) Child = CreateChild();
        _nextValue = GetNextValue();
        _previousValue = GetPreviousValue();
    }

    private readonly bool _needsChild;
    private bool NeedsChild
    {
        get
        {
            return _needsChild;
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

    private int GetPreviousValue()
    {
        if (!NeedsChild) return 0;

        int previousValue = GeneratePreviousValue();
        return previousValue;
    }

    private int GetNextValue()
    {
        if (!NeedsChild) return 0;

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

    private readonly int _previousValue;
    public int PreviousValue
    {
        get
        {
            return _previousValue;
        }
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

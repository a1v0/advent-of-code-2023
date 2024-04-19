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
}

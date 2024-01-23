namespace AdventOfCode2023;

public class Day15 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day15Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day15Task2();
}

public class Day15Task1 : BaseTask
{
    public override string Solve()
    {
        int sumOfHashes = Hashes.Sum();
        return sumOfHashes.ToString();
    }

    private int[] GetHashes()
    {
        var hashes = new List<int>();

        foreach (string sequence in Sequences)
        {
            int hash = GetHash(sequence);
            hashes.Add(hash);
        }

        return hashes.ToArray();
    }

    private static int GetHash(string sequence)
    {
        int current = 0;
        int multiplicationValue = 17,
            moduloValue = 256;

        foreach (char @char in sequence)
        {
            int asciiValue = (int)@char;
            current += asciiValue;
            current *= multiplicationValue;
            current %= moduloValue;
        }

        return current;
    }

    private int[]? _hashes;
    private int[] Hashes
    {
        get
        {
            _hashes ??= GetHashes();
            return _hashes;
        }
    }

    private string[]? _sequences;
    private string[] Sequences
    {
        get
        {
            _sequences ??= Input.Split(',');
            return _sequences;
        }
    }
}

public class Day15Task2 : Day15Task1
{ }

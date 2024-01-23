using System.Linq.Expressions;

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
{
    public override string Solve()
    {
        DistributeLenses();
        int totalFocusingPower = SummariseFocusingPower();
        return totalFocusingPower.ToString();
    }

    private int SummariseFocusingPower()
    {
        // calculate focusing power:
        // - for each lens in each box (i.e. nested loop), multiply together
        //   - box number + 1
        //   - slot number, 1-indexed, i.e. index of lens within list
        //   - lens focal length
        int total = 0;

        foreach (Box box in Boxes)
        {
            total += SummariseBox(box);
        }

        return total;
    }
    // check sequence for special char
    // if -:
    // - extract label from string
    // - go to relevant box
    //   - run hash on label to do this
    // - check if list contains label
    //   - if not, return
    //   - if so, remove item
    // 
    // if =:
    // - extract lens size from string
    // - extract label from string
    // - go to relevant box
    //   - run hash on label
    // - if lens already exists in list, overwrite lens value in dictionary
    // - if not, stick new lens on the end of the list
    // 

    private Box[]? _boxes;
    private Box[] Boxes
    {
        get
        {
            _boxes ??= GetBoxes();
            return _boxes;
        }
    }

    private static Box[] GetBoxes()
    {
        int totalBoxes = 256;
        var boxes = new Box[totalBoxes];

        for (byte i = 0; i < totalBoxes; ++i)
        {
            boxes[i] = new Box();
        }

        return boxes;
    }
}

public class Box
{
    public List<string> Labels { get; } = new List<string>();

    public Dictionary<string, byte> Lenses { get; } = new Dictionary<string, byte>();
}
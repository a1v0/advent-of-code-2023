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
    // create array of boxes
    // - box = tuple (list containing order of lens labels, dictionary containing current lens size of each lens label)
    // 
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
    // calculate focusing power:
    // - for each lens in each box (i.e. nested loop), multiply together
    //   - box number + 1
    //   - slot number, 1-indexed, i.e. index of lens within list
    //   - lens focal length
    // 
    // summarise focusing power
    // return as a string

    private (List<string>, Dictionary<string, byte>)[]? _boxes;
    private (List<string>, Dictionary<string, byte>)[] Boxes
    {
        get
        {
            _boxes ??= GetBoxes();
            return _boxes;
        }
    }

    private static (List<string>, Dictionary<string, byte>)[] GetBoxes()
    {
        int totalBoxes = 256;
        var boxes = new (List<string>, Dictionary<string, byte>)[totalBoxes];

        for (byte i = 0; i < totalBoxes; ++i)
        {
            var box = (new List<string>(), new Dictionary<string, byte>());
            boxes[i] = box;
        }

        return boxes;
    }
}

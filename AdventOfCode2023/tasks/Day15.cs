using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;

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

    protected static int GetHash(string sequence)
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
    protected string[] Sequences
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
        int total = 0;

        foreach (Box box in Boxes)
        {
            total += SummariseBox(box);
        }

        return total;
    }

    private static int SummariseBox(Box box)
    {
        int total = 0;
        int currentIndex = 0;

        foreach (string label in box.Labels)
        {
            ++currentIndex;
            byte focalStrength = box.Lenses[label];
            int lensTotal = box.Number * currentIndex * focalStrength;
            total += lensTotal;
        }

        return total;
    }

    private void DistributeLenses()
    {
        foreach (string sequence in Sequences)
        {
            bool hasDash = sequence.Contains('-'),
                 hasEquals = sequence.Contains('=');

            if (hasDash)
            {
                DistributeLensesDash(sequence);
            }
            else if (hasEquals)
            {
                DistributeLensesEquals(sequence);
            }
            else
            {
                throw new Exception("No operator identified in sequence. Check input.");
            }
        }
    }

    private void DistributeLensesEquals(string sequence)
    {
        string[] labelAndLens = sequence.Split('=');
        string label = labelAndLens[0];
        int boxNumber = GetHash(label);
        byte lensSize = byte.Parse(labelAndLens[1]);

        Box currentBox = Boxes[boxNumber];
        bool lensExists = currentBox.Lenses.ContainsKey(label);
        if (lensExists) currentBox.Lenses[label] = lensSize;
        else
        {
            currentBox.Lenses.Add(label, lensSize);
            currentBox.Labels.Add(label);
        }
    }

    private void DistributeLensesDash(string sequence)
    {
        string label = sequence[0..^1];
        int boxNumber = GetHash(label);

        Box currentBox = Boxes[boxNumber];
        currentBox.Labels.Remove(label);
        currentBox.Lenses.Remove(label);
    }

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

        for (int i = 0; i < totalBoxes; ++i)
        {
            boxes[i] = new Box(i + 1);
        }

        return boxes;
    }
}

public class Box
{
    public Box(int number)
    {
        Number = number;
    }

    public int Number { get; }

    public List<string> Labels { get; } = new List<string>();

    public Dictionary<string, byte> Lenses { get; } = new Dictionary<string, byte>();
}
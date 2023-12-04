using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2023;

public class Day03 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day03Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day03Task2();
}

public class Day03Task1 : BaseTask
{
    public override string Solve()
    {
        int[] partNumbers = GetPartNumbers();
        int sumOfPartNumbers = partNumbers.Sum();
        return sumOfPartNumbers.ToString();
    }

    private string[]? _inputRows;

    protected string[] InputRows
    {
        get
        {
            if (_inputRows is null)
            {
                _inputRows = GetInputRows();
            }
            return _inputRows;
        }
    }

    private int[] GetPartNumbers()
    {
        SchematicNumber[] schematicNumbers = GetSchematicNumbers();
        // for(int i = 0;i<partNumbers.Length;++i)
        // {
        //     partNumbers[i]=
        // }
        // loop through rows
        // find all the numbers
        // store numbers in a tuple (or similar)
        // - number, row, start index (and end index, if needed)
        // check up, down, left and right of each number in search of a symbol
    }

    private SchematicNumber[] GetSchematicNumbers()
    {
        var schematicNumbers = new List<SchematicNumber>();

        for (int i = 0; i < InputRows.Length; ++i)
        {
            List<SchematicNumber> numbersInRow = GetNumbersInRow(i);
            schematicNumbers.AddRange(numbersInRow);
        }

        return schematicNumbers.ToArray();
    }

    private string[] GetInputRows()
    {
        return Input.Split('\n');
    }
}

public class Day03Task2 : Day03Task1
{ }

public class SchematicNumber
{
    public SchematicNumber(string value, int x, int y)
    {
        Value = value;
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
    public string Value { get; }
}
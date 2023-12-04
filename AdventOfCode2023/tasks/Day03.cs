using System.Text.RegularExpressions;

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
        var partNumbers = new List<int>();

        foreach (SchematicNumber schematicNumber in schematicNumbers)
        {
            bool isSchematicNumber = CheckSchematicNumber(schematicNumber);

            if (isSchematicNumber)
            {
                int value = int.Parse(schematicNumber.Value);
                partNumbers.Add(value);
            }
        }

        return partNumbers.ToArray();
    }

    private bool CheckSchematicNumber(SchematicNumber schematicNumber)
    {
        int leftBound = schematicNumber.X == 0 ? 0 : schematicNumber.X - 1;
        int rightBound = schematicNumber.X + schematicNumber.Value.Length;

        int currentRow = schematicNumber.Y;

        bool isLeftMatch = IsMatch(currentRow, leftBound);
        if (isLeftMatch) return true;

        bool isRightMatch = IsMatch(currentRow, rightBound);
        if (isRightMatch) return true;

        int rowAbove = currentRow - 1,
        rowBelow = currentRow + 1;

        bool isFirstRow = currentRow == 0;
        if (isFirstRow) return false;

        bool isMatchAbove = IsMatchInRow(rowAbove, leftBound, rightBound);
        if (isMatchAbove) return true;

        bool isLastRow = currentRow == InputRows.Length - 1;
        if (isLastRow) return false;

        bool isMatchBelow = IsMatchInRow(rowBelow, leftBound, rightBound);
        if (isMatchBelow) return true;

        return false;
    }

    private bool IsMatchInRow(int row, int leftBound, int rightBound)
    {
        for (int i = leftBound; i <= rightBound; ++i)
        {
            bool isCurrentCharAMatch = IsMatch(row, i);
            if (isCurrentCharAMatch) return true;
        }
        return false;
    }

    private bool IsMatch(int rowIndex, int columnIndex)
    {
        string row = InputRows[rowIndex];
        string value = row[columnIndex].ToString();

        var symbolRegex = new Regex(@"[^\d.]");
        return symbolRegex.IsMatch(value);
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

    private List<SchematicNumber> GetNumbersInRow(int row)
    {
        string currentRow = InputRows[row];
        var numRegex = new Regex(@"\d+");
        MatchCollection matches = numRegex.Matches(currentRow);

        var numbersInRow = new List<SchematicNumber>();

        foreach (Match match in matches)
        {
            string value = match.Value;
            int column = match.Index;
            var schematicNumber = new SchematicNumber(value, row, column);
            numbersInRow.Add(schematicNumber);
        }

        return numbersInRow;
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
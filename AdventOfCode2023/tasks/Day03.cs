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
            _inputRows ??= GetInputRows();
            return _inputRows;
        }
    }

    protected int[] GetPartNumbers()
    {
        SchematicNumber[] schematicNumbers = GetSchematicNumbers();
        var partNumbers = new List<int>();

        foreach (SchematicNumber schematicNumber in schematicNumbers)
        {
            bool isPartNumber = CheckSchematicNumber(schematicNumber);

            if (isPartNumber)
            {
                int value = int.Parse(schematicNumber.Value);
                partNumbers.Add(value);
            }
        }

        return partNumbers.ToArray();
    }

    protected virtual bool CheckSchematicNumber(SchematicNumber schematicNumber)
    {
        //
        //
        // this method is messy beyond belief
        // desperately needs a refactor
        //
        //
        //
        int currentRow = schematicNumber.Y;

        int leftBound = schematicNumber.X == 0 ? 0 : schematicNumber.X - 1;
        int rightBound = schematicNumber.X + schematicNumber.Value.Length;
        bool isTooFarRight = rightBound >= InputRows[currentRow].Length - 1;

        if (isTooFarRight) --rightBound;
        else
        {
            bool isRightMatch = IsMatch(currentRow, rightBound);
            if (isRightMatch) return true;
        }

        bool isLeftMatch = IsMatch(currentRow, leftBound);
        if (isLeftMatch) return true;

        int rowAbove = currentRow - 1,
        rowBelow = currentRow + 1;

        bool isFirstRow = currentRow == 0;
        if (!isFirstRow)
        {
            bool isMatchAbove = IsMatchInRow(rowAbove, leftBound, rightBound);
            if (isMatchAbove) return true;
        }

        bool isLastRow = currentRow == InputRows.Length - 1;
        if (!isLastRow)
        {
            bool isMatchBelow = IsMatchInRow(rowBelow, leftBound, rightBound);
            if (isMatchBelow) return true;
        }

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
            var schematicNumber = new SchematicNumber(value, column, row);
            numbersInRow.Add(schematicNumber);
        }

        return numbersInRow;
    }
}

public class Day03Task2 : Day03Task1
{
    public override string Solve()
    {
        // This is a bit sneaky, because GetPartNumbers
        // does a number of things that I don't care
        // about in this solution. Really, I should
        // probably rewrite the whole method, but I'm
        // reusing as much code as possible in the
        // interest of time
        GetPartNumbers();
        int[] gearRatios = GetGearRatios();
        int sumOfGearRatios = gearRatios.Sum();
        return sumOfGearRatios.ToString();
    }

    private int[] GetGearRatios()
    {
        var gearRatios = new List<int>();
        foreach (KeyValuePair<string, List<int>> pair in PossibleGears)
        {
            if (pair.Value.Count != 2) continue;

            int gearRatio = pair.Value[0] * pair.Value[1];
            gearRatios.Add(gearRatio);
        }

        return gearRatios.ToArray();
    }

    protected bool IsMatch(int rowIndex, int columnIndex, SchematicNumber schematicNumber)
    {
        string row = InputRows[rowIndex];
        string value = row[columnIndex].ToString();

        var symbolRegex = new Regex(@"\*");

        bool isAsterisk = symbolRegex.IsMatch(value);

        if (isAsterisk)
        {
            string coordinate = $"{columnIndex},{rowIndex}";
            int productNumber = int.Parse(schematicNumber.Value);

            bool keyExists = PossibleGears.ContainsKey(coordinate);
            if (keyExists)
            {
                PossibleGears[coordinate].Add(productNumber);
            }
            else
            {
                var possibleGear = new List<int>() { productNumber };
                PossibleGears.Add(coordinate, possibleGear);
            }
        }

        return isAsterisk;
    }

    protected override bool CheckSchematicNumber(SchematicNumber schematicNumber)
    {
        //
        //
        // this method is messy beyond belief
        // desperately needs a refactor
        //
        //
        //
        int currentRow = schematicNumber.Y;

        int leftBound = schematicNumber.X == 0 ? 0 : schematicNumber.X - 1;
        int rightBound = schematicNumber.X + schematicNumber.Value.Length;
        bool isTooFarRight = rightBound >= InputRows[currentRow].Length - 1;

        if (isTooFarRight) --rightBound;
        else
        {
            bool isRightMatch = IsMatch(currentRow, rightBound, schematicNumber);
            if (isRightMatch) return true;
        }

        bool isLeftMatch = IsMatch(currentRow, leftBound, schematicNumber);
        if (isLeftMatch) return true;

        int rowAbove = currentRow - 1,
        rowBelow = currentRow + 1;

        bool isFirstRow = currentRow == 0;
        if (!isFirstRow)
        {
            bool isMatchAbove = IsMatchInRow(rowAbove, leftBound, rightBound, schematicNumber);
            if (isMatchAbove) return true;
        }

        bool isLastRow = currentRow == InputRows.Length - 1;
        if (!isLastRow)
        {
            bool isMatchBelow = IsMatchInRow(rowBelow, leftBound, rightBound, schematicNumber);
            if (isMatchBelow) return true;
        }

        return false;
    }

    private bool IsMatchInRow(int row, int leftBound, int rightBound, SchematicNumber schematicNumber)
    {
        for (int i = leftBound; i <= rightBound; ++i)
        {
            bool isCurrentCharAMatch = IsMatch(row, i, schematicNumber);
            if (isCurrentCharAMatch) return true;
        }

        return false;
    }

    private Dictionary<string, List<int>> PossibleGears
    {
        get;
    } = new Dictionary<string, List<int>>();
}

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
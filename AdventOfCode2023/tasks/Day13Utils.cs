namespace AdventOfCode2023;

public class AshPattern
{
    public AshPattern(string input)
    {
        Input = input;
    }

    public AshPattern(string input, int forbiddenColumns, int forbiddenRows)
    {
        Input = input;
        ForbiddenValues = (forbiddenColumns, forbiddenRows);
    }

    private (int, int)? ForbiddenValues { get; }

    private string Input { get; }

    private int? _summary;
    public int Summary
    {
        get
        {
            _summary ??= GetSummary();
            return (int)_summary;
        }
    }

    private int GetSummary()
    {
        int rowsCoefficient = 100;
        int multipliedRows = RowsAboveMirror * rowsCoefficient;
        return ColumnsLeftOfMirror + multipliedRows;
    }

    private string[]? _rows;
    private string[] Rows
    {
        get
        {
            _rows ??= GetRows();
            return _rows;
        }
    }

    private string[] GetRows()
    {
        return Input.Split('\n');
    }

    private string[]? _columns;
    private string[] Columns
    {
        get
        {
            _columns ??= GetColumns();
            return _columns;
        }
    }

    private string[] GetColumns()
    {
        int totalColumns = Rows[0].Length;
        string[] columns = new string[totalColumns];

        for (int i = 0; i < totalColumns; ++i)
        {
            string column = "";

            foreach (string row in Rows)
            {
                column += row[i];
            }

            columns[i] = column;
        }

        return columns;
    }

    private int? _rowsAboveMirror;
    public int RowsAboveMirror
    {
        get
        {
            _rowsAboveMirror ??= GetQuantityBeforeMirror(Rows, 1);
            return (int)_rowsAboveMirror;
        }
    }

    private int? _columnsLeftOfMirror;
    public int ColumnsLeftOfMirror
    {
        get
        {
            _columnsLeftOfMirror ??= GetQuantityBeforeMirror(Columns, -1);
            return (int)_columnsLeftOfMirror;
        }
    }

    private int GetQuantityBeforeMirror(string[] rows, int unitType = 0) // if unitType < 0, it's a column; > 0 means row
    {
        for (int i = 0; i < rows.Length - 1; ++i)
        {
            string leftmost = rows[0],
                   rightmost = rows[^1];

            string rightComparison = rows[rows.Length - 1 - i],
                   leftComparison = rows[i];

            bool reflectionStartsFromLeft = leftmost == rightComparison,
                 reflectionStartsFromRight = rightmost == leftComparison;

            if (reflectionStartsFromLeft)
            {
                bool isMirror = CheckIfMirror(0, rows.Length - 1 - i, rows);
                if (isMirror)
                {
                    int quantity = CalculateQuantityBeforeMirror(0, rows.Length - 1 - i);
                    bool isForbiddenQuantity = IsQuantityForbidden(quantity, unitType);
                    if (!isForbiddenQuantity) return quantity;
                }
            }

            if (reflectionStartsFromRight)
            {
                bool isMirror = CheckIfMirror(i, rows.Length - 1, rows);
                if (isMirror)
                {
                    int quantity = CalculateQuantityBeforeMirror(i, rows.Length - 1);
                    bool isForbiddenQuantity = IsQuantityForbidden(quantity, unitType);
                    if (!isForbiddenQuantity) return quantity;
                }
            }
        }

        return 0;
    }

    private bool IsQuantityForbidden(int quantity, int unitType)
    {
        // this method feels clunky
        if (quantity == 0) return true;
        if (ForbiddenValues is null) return false;

        (int forbiddenColumns, int forbiddenRows) = ForbiddenValues ?? default;

        if (unitType < 0) return quantity == forbiddenColumns;
        if (unitType > 0) return quantity == forbiddenRows;

        return false;
    }

    private static bool CheckIfMirror(int start, int end, string[] rows)
    {
        for (int i = start, j = end; i < j; ++i, --j)
        {
            string iValue = rows[i],
                   jValue = rows[j];

            if (iValue != jValue) return false;
        }

        return true;
    }

    private static int CalculateQuantityBeforeMirror(int i, int j)
    {
        double difference = j - i;
        double halfDifference = difference / 2;

        bool isInt = halfDifference == (int)halfDifference;
        if (isInt) return 0; // the mirror must be between two rows, so this number must end in .5, else it's a false positive 

        double ceilingOfHalfDifference = Math.Ceiling(halfDifference);
        return i + (int)ceilingOfHalfDifference;
    }
}
namespace AdventOfCode2023;

public class Day13 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day13Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day13Task2();
}

public class Day13Task1 : BaseTask
{
    public override string Solve()
    {
        int summarisedPatterns = SummariseAshPatterns();
        return summarisedPatterns.ToString();
    }

    private int SummariseAshPatterns()
    {
        int summary = AshPatterns.Aggregate(0, (acc, x) =>
        {
            return acc + x.Summary;
        });
        return summary;
    }

    private AshPattern[]? _ashPatterns;
    private AshPattern[] AshPatterns
    {
        get
        {
            _ashPatterns ??= GetAshPatterns();
            return _ashPatterns;
        }
    }

    protected virtual AshPattern[] GetAshPatterns()
    {
        string[] splitInput = Input.Split("\n\n");
        var ashPatterns = new AshPattern[splitInput.Length];

        for (int i = 0; i < ashPatterns.Length; ++i)
        {
            ashPatterns[i] = new AshPattern(splitInput[i]);
        }

        return ashPatterns;
    }
}

public class Day13Task2 : Day13Task1
{
    // Task 1 completes in about 0.5s
    // Therefore brute-forcing the answer might take no more than about 90s
    // I can't really think of any method other than brute force
    // 
    // Current solution works for second pattern, but not first.
    // I believe this is because there is more than one valid mirror line.
    // To get around this problem, I might need to alter the AshPattern class (or created an extended class).
    // Perhaps add a second constructor that takes the row/cols of the original AshPattern
    // If it identifies a mirror that's equal to the original mirror, it keeps looking
    // 
    protected override AshPattern[] GetAshPatterns()
    {
        string[] splitInput = Input.Split("\n\n");
        var ashPatterns = new AshPattern[splitInput.Length];

        for (int i = 0; i < ashPatterns.Length; ++i)
        {
            ashPatterns[i] = GetAshPattern(splitInput[i]);
        }

        return ashPatterns;
    }

    private static AshPattern GetAshPattern(string input)
    {
        var ashPatternWithSmudge = new AshPattern(input);

        for (int i = 0; i < input.Length; ++i)
        {
            char current = input[i];
            bool isPartOfPattern = current == '.' || current == '#';
            if (!isPartOfPattern) continue;

            char[] inputBuilder = input.ToCharArray();
            char updatedCurrent = current == '.' ? '#' : '.';
            inputBuilder[i] = updatedCurrent;

            string updatedInput = new string(inputBuilder);
            var ashPattern = new AshPattern(updatedInput, ashPatternWithSmudge.ColumnsLeftOfMirror, ashPatternWithSmudge.RowsAboveMirror);

            bool doesNotHaveExactlyOneMirror = (ashPattern.ColumnsLeftOfMirror > 0 && ashPattern.RowsAboveMirror > 0)
                                            || (ashPattern.ColumnsLeftOfMirror == 0 && ashPattern.RowsAboveMirror == 0);
            bool mirrorHasDifferentLocation = (ashPattern.ColumnsLeftOfMirror == 0 || ashPattern.ColumnsLeftOfMirror != ashPatternWithSmudge.ColumnsLeftOfMirror)
                                           && (ashPattern.RowsAboveMirror == 0 || ashPattern.RowsAboveMirror != ashPatternWithSmudge.RowsAboveMirror);

            bool isValidPattern = !doesNotHaveExactlyOneMirror && mirrorHasDifferentLocation;
            if (!isValidPattern) continue;

            return ashPattern;
        }

        throw new Exception("No mirror identified.");
    }
}

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
            _rowsAboveMirror ??= GetQuantityBeforeMirror(Rows);
            return (int)_rowsAboveMirror;
        }
    }

    private int? _columnsLeftOfMirror;
    public int ColumnsLeftOfMirror
    {
        get
        {
            _columnsLeftOfMirror ??= GetQuantityBeforeMirror(Columns);
            return (int)_columnsLeftOfMirror;
        }
    }

    private int GetQuantityBeforeMirror(string[] rows, int unitType = 0) // if unitType < 0, it's a column; > 0 means row
    {
        for (int i = 0; i < rows.Length; ++i)
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
                    if (isForbiddenQuantity) continue;
                    return quantity;
                }
            }

            if (reflectionStartsFromRight)
            {
                bool isMirror = CheckIfMirror(i, rows.Length - 1, rows);
                if (isMirror)
                {
                    int quantity = CalculateQuantityBeforeMirror(i, rows.Length - 1);
                    bool isForbiddenQuantity = IsQuantityForbidden(quantity, unitType);
                    if (isForbiddenQuantity) continue;
                    return quantity;
                }
            }
        }

        return 0;
    }

    private bool IsQuantityForbidden(int quantity, int unitType)
    {
        // this method feels clunky
        if (quantity == 0) return false;
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
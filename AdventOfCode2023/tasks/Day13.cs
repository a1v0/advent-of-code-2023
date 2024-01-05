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

    private AshPattern[] GetAshPatterns()
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
{ }

public class AshPattern
{
    public AshPattern(string input)
    {
        Input = input;
    }

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
        int columnLength = Rows.Length;
        string[] columns = new string[columnLength];

        for (int i = 0; i < columnLength; ++i)
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
    private int RowsAboveMirror
    {
        get
        {
            _rowsAboveMirror ??= GetQuantityBeforeMirror(Rows);
            return _rowsAboveMirror;
        }
    }

    private int? _columnsLeftOfMirror;
    private int ColumnsLeftOfMirror
    {
        get
        {
            _columnsLeftOfMirror ??= GetQuantityBeforeMirror(Columns);
            return _columnsLeftOfMirror;
        }
    }

    private static int GetQuantityBeforeMirror(string[] rows)
    {
        // loop through rows or columns (i = 0, j = Length - 1)
        // if rows[i] == rows[j]
        // - call method to check that i + 1 == j - 1, etc.
        // - if it's a valid mirror
        //   - find midpoint between i and j
        //   - round up to nearest whole number to give amount of columns to left
        //   - return
        // if rows[i] != rows[j], check rows[j - 1]
        // - --j and keep going
        // if nothing found, ++i and try again

        for (int i = 0; i < rows.Length; ++i)
        {
            string current = rows[i];

            for (int j = rows.Length - 1; j > i; --j)
            {
                string comparison = rows[j];
                if (current != comparison) continue;

                bool isMirror = CheckIfMirror(i, j, rows);
                if (!isMirror) continue;

                return CalculateQuantityBeforeMirror(i, j);
            }
        }

        throw new Exception("No mirror found in input.");
    }
}
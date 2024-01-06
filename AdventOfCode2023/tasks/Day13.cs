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
        // 
        // currently my answer is too high
        // 
        // THESE TWO PATTERNS HAVE MORE THAN ONE MIRROR, i.e. one on each axis
        // 
        // 
        // .#.##.#..##...#
        // ###..###.##.#..
        // #.####.##..#.##
        // #.#..#......#.. v
        // #.#..#......#.. ^
        // #.####.##..#.##
        // ###..###.##.#..
        // > < this is identified as one. tbh, I get why.
        // 
        // ##.####.##.####.#
        // ..#.##.####.##.#.
        // .#.####.##.####.#
        // #..#..#....#..#..
        // #..#..#....#..#..
        // ##.#..#.##.#..#.#
        // #.##..##..##..##.
        // ####..######..###
        // ###.##.####.##.##
        // ....##......##...
        // #.##..##..##..##.  v
        // #...##......##...  is this the problem? does it think this is a valid mirror, when it's not actually a gap between rows?
        // #.##..##..##..##.  ^
        //         ><
        // 
        // 
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
    private int RowsAboveMirror
    {
        get
        {
            _rowsAboveMirror ??= GetQuantityBeforeMirror(Rows);
            return (int)_rowsAboveMirror;
        }
    }

    private int? _columnsLeftOfMirror;
    private int ColumnsLeftOfMirror
    {
        get
        {
            _columnsLeftOfMirror ??= GetQuantityBeforeMirror(Columns);
            return (int)_columnsLeftOfMirror;
        }
    }

    private static int GetQuantityBeforeMirror(string[] rows)
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
                if (isMirror) return CalculateQuantityBeforeMirror(0, rows.Length - 1 - i);
            }

            if (reflectionStartsFromRight)
            {
                bool isMirror = CheckIfMirror(i, rows.Length - 1, rows);
                if (isMirror) return CalculateQuantityBeforeMirror(i, rows.Length - 1);
            }
        }

        return 0;
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
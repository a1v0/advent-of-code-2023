namespace AdventOfCode2023;

public class Day14 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day14Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day14Task2();
}

public class Day14Task1 : BaseTask
{
    public override string Solve()
    {
        int totalLoad = GetTotalLoad();
        return totalLoad.ToString();
    }

    private int GetTotalLoad()
    {
        int total = 0;

        foreach (string column in Columns)
        {
            total += SummariseColumn(column);
        }

        return total;
    }

    private static int SummariseColumn(string column)
    {
        string sortedColumn = GetSortedColumn(column);

        int total = 0;
        int coefficient = sortedColumn.Length;

        foreach (char rock in sortedColumn)
        {
            if (rock == 'O') total += coefficient;
            --coefficient;
        }

        return total;
    }

    private static string GetSortedColumn(string column)
    {
        string[] columnSections = column.Split('#');
        var sortedColumnSections = new List<string>();

        foreach (string columnSection in columnSections)
        {
            string sortedColumnSection = SortColumnSection(columnSection);
            sortedColumnSections.Add(sortedColumnSection);
        }

        string[] resultantArray = sortedColumnSections.ToArray();
        return string.Join('#', resultantArray);
    }

    private static string SortColumnSection(string columnSection)
    {
        char[] columnChars = columnSection.ToCharArray();
        Array.Sort(columnChars);
        Array.Reverse(columnChars);
        return new string(columnChars);
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
        int totalColumns = InputRows[0].Length;
        string[] columns = new string[totalColumns];

        for (int i = 0; i < totalColumns; ++i)
        {
            string column = "";

            foreach (string row in InputRows)
            {
                column += row[i];
            }

            columns[i] = column;
        }

        return columns;
    }
}

public class Day14Task2 : Day14Task1
{
    // 1 billion cycles doesn't even sound THAT bad but it probably IS that bad
    // 1bn cycles = 4bn rotations
    // 
    // 
    // 
    // 
    // create a loop that runs 1bn times (or 4bn, depending on how you code it)
    // each time, parse the input anew to achieve the anticlockwise flipping
    // 
    // 
    // 
    // 
    // if it does take too long (it could easily take too long), then the chances are that a pattern will emerge
    // 
    // 
    // 
}
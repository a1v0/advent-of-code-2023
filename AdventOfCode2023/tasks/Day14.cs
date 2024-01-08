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
        // split columns by "#"
        // turn each block char array and sort such that O and . are separate
        // join by "#"
        // 
        // summarise each column
        // - loop through column's contents and add
        // sum all columns
        // return
        // 

        string sortedColumn = GetSortedColumn(column);

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
{ }

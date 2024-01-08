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
        // parse input into columns
        // split columns by "#"
        // turn each block char array and sort such that O and . are separate
        // join by "#"
        // 
        // summarise each column
        // - loop through column's contents and add
        // sum all columns
        // return
        // 
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

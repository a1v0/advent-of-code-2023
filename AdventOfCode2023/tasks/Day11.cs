namespace AdventOfCode2023;

public class Day11 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day11Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day11Task2();
}

public class Day11Task1 : BaseTask
{
    public override string Solve()
    {
        // 
        // make dictionary with keys of tuples (galaxyA, galaxyB), where A is always the smaller galaxy number
        // - value is int?
        // go through all galaxies one by one and create keys for each pair, setting value to null <== AGAIN, THIS FEELS INEFFICIENT
        // iterate over dictionary
        // set value to calculated distance between galaxies
        // - this is always a simple calculation of change in Y plus change in X
    }

    private Galaxy[]? _galaxies;
    private Galaxy[] Galaxies
    {
        get
        {
            _galaxies ??= GetGalaxies();
            return _galaxies;
        }
    }

    private Galaxy[] GetGalaxies()
    {
        List<List<int?>> spaceMap = GetSpaceMap();
        var galaxies = new List<Galaxy>();

        for (int row = 0; row < spaceMap.Count; ++row)
        {
            for (int column = 0; column < spaceMap[row].Count; ++column)
            {
                int? current = spaceMap[row][column];
                if (current is null) continue;

                var galaxy = new Galaxy((int)current, column, row);
                galaxies.Add(galaxy);
            }
        }

        return galaxies.ToArray();
    }

    private List<List<int?>> GetSpaceMap()
    {
        List<List<int?>> unexpandedMap = GetUnexpandedMap();
        return ExpandMap(unexpandedMap);
        // parse input into list of lists of int?s
        // - '.' is null
        // - galaxies get a unique number
        // work right-to-left and bottom-to-top to expand universe:
        // - do columns first
        // - loop through each index of each row and check whether there's a galaxy
        // - if no galaxy, insert a column at that location in each row <== THIS WHOLE THING IS VERY INEFFICIENT BUT I'M UNSURE IF THERE'S A BETTER WAY TO DO IT
        // - then rows
        // - if row contains only '.', insert a copy of that row below
    }
}

public class Day11Task2 : Day11Task1
{ }

public class Galaxy
{
    public Galaxy(int id, int x, int y)
    {
        _id = id;
        _x = x;
        _y = y;
    }

    private int _id;
    public int ID
    {
        get
        {
            return _id;
        }
    }
    private int _x;
    public int X
    {
        get
        {
            return _x;
        }
    }
    private int _y;
    public int Y
    {
        get
        {
            return _y;
        }
    }
}

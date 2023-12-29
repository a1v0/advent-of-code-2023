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
        // make dictionary with keys of tuples (galaxyA, galaxyB), where A is always the smaller galaxy number
        // - value is int?
        // go through all galaxies one by one and create keys for each pair, setting value to null <== AGAIN, THIS FEELS INEFFICIENT
        // iterate over dictionary
        // set value to calculated distance between galaxies
        // - this is always a simple calculation of change in Y plus change in X
    }

    private Dictionary<(int, int), int?> GetDistances()
    {
        var distances = new Dictionary<(int, int), int?>();

        for (int i = 0; i < Galaxies.Length - 1; ++i)
        {
            for (int j = i + 1; j < Galaxies.Length; ++j)
            {
                distances.Add((i, j), null);
            }
        }

        CalculateDistances(distances);

        return distances;
    }

    private Dictionary<(int, int), int?>? _distances;
    private Dictionary<(int, int), int?> Distances
    {
        get
        {
            _distances ??= GetDistances();
            return _distances;
        }
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
        return GetExpandedMap(unexpandedMap);
    }

    private static List<List<int?>> GetExpandedMap(List<List<int?>> unexpandedMap)
    {
        List<List<int?>> mapWithExpandedColumns = GetMapWithExpandedColumns(unexpandedMap);
        return GetMapWithExpandedRows(mapWithExpandedColumns);
    }

    private static List<List<int?>> GetMapWithExpandedRows(List<List<int?>> unexpandedMap)
    {
        var blankRows = new List<int>();
        for (int i = unexpandedMap.Count - 1; i >= 0; --i)
        {
            List<int?> currentRow = unexpandedMap[i];
            bool isBlankRow = true;

            foreach (int? coordinate in currentRow)
            {
                if (coordinate is null) continue;

                isBlankRow = false;
                break;
            }

            if (isBlankRow) blankRows.Add(i);
        }

        AddExpandedRows(unexpandedMap, blankRows);
        return unexpandedMap;
    }

    private static void AddExpandedRows(List<List<int?>> unexpandedMap, List<int> blankRows)
    {
        foreach (int rowIndex in blankRows)
        {
            var blankRow = new List<int?>();
            for (int i = 0; i < unexpandedMap[0].Count; ++i)
            {
                blankRow.Add(null);
            }

            unexpandedMap.Insert(rowIndex, blankRow);
        }
    }

    private static List<List<int?>> GetMapWithExpandedColumns(List<List<int?>> unexpandedMap)
    {
        var blankColumns = new List<int>();
        for (int i = unexpandedMap[0].Count - 1; i >= 0; --i)
        {
            bool isBlankColumn = true;
            for (int j = 0; j < unexpandedMap.Count; ++j)
            {
                int? currentValue = unexpandedMap[j][i];
                if (currentValue is null) continue;
                isBlankColumn = false;
                break;
            }

            if (isBlankColumn) blankColumns.Add(i);
        }

        AddExpandedColumns(unexpandedMap, blankColumns);
        return unexpandedMap;
    }

    private static void AddExpandedColumns(List<List<int?>> unexpandedMap, List<int> blankColumns)
    {
        foreach (List<int?> row in unexpandedMap)
        {
            foreach (int column in blankColumns)
            {
                row.Insert(column, null);
            }
        }
    }

    private List<List<int?>> GetUnexpandedMap()
    {
        var unexpandedMap = new List<List<int?>>();
        int currentGalaxyID = 0;

        foreach (string rawRow in InputRows)
        {
            var row = new List<int?>();
            foreach (char coordinate in rawRow)
            {
                if (coordinate == '.') row.Add(null);
                else row.Add(currentGalaxyID++);
            }

            unexpandedMap.Add(row);
        }

        return unexpandedMap;
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

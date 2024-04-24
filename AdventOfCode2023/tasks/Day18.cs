namespace AdventOfCode2023;

public class Day18 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day18Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day18Task2();
}

public class Day18Task1 : BaseTask
{
    public override string Solve()
    {
        long area = CalculateArea();
        return area.ToString();
    }

    private long CalculateArea()
    {
        long area = 0;
        int? lastRowNumber = null;

        int[] rowNumbers = GetRowNumbers();

        for (int i = 0; i < rowNumbers.Length; ++i)
        {
            int rowNumber = rowNumbers[i];
            Dictionary<int, TerrainNode> row = TerrainMap[rowNumber];

            int areaOfCornerRow = GetAreaOfRow(row);

            UpdateOpenCorners(row);

            int areaOfNormalRow = GetAreaOfRow(OpenCorners);
            int rowsToAdd = GetRowsBetweenCorners(lastRowNumber, rowNumber);
            int areaOfNormalRows = areaOfNormalRow * rowsToAdd;

            int areaToAdd = areaOfNormalRows + areaOfCornerRow;

            area += areaToAdd;

            lastRowNumber = rowNumber;
        }

        return area;
    }

    private void UpdateOpenCorners(Dictionary<int, TerrainNode> row)
    {
        foreach (KeyValuePair<int, TerrainNode> corner in row)
        {
            int column = corner.Key;
            bool cornerClosed = OpenCorners.Remove(column); // true if there was an open corner to remove
            if (cornerClosed) continue;

            OpenCorners.Add(column, corner.Value);
        }
    }

    private static int GetAreaOfRow(Dictionary<int, TerrainNode> row)
    {
        int area = 0;

        int[] columns = row.Keys.ToArray();
        Array.Sort(columns); // TODO: move this into its own method
        char? direction = null;
        bool currentlyInsideShape = true; // the leftmost square always opens the row
        int lastColumn = columns[0];

        for (int i = 0; i < columns.Length; ++i)
        {
            ++area; // count the current corner

            int column = columns[i];

            if (currentlyInsideShape)
            {
                int columnsBetweenCorners = GetColumnsBetweenCorners(lastColumn, column);
                area += columnsBetweenCorners;
            }

            TerrainNode corner = row[column];
            direction ??= corner.Direction;

            if (direction != corner.Direction)
            {
                currentlyInsideShape = !currentlyInsideShape;
            }

            lastColumn = column;
        }

        return area;
    }

    private static int GetColumnsBetweenCorners(int lastColumn, int currentColumn)
    {
        return GetDifferenceMinusOne(lastColumn, currentColumn);
    }

    private static int GetRowsBetweenCorners(int? lastCornerRow, int currentCornerRow)
    {
        lastCornerRow ??= currentCornerRow;
        return GetDifferenceMinusOne((int)lastCornerRow, currentCornerRow);
    }

    private static int GetDifferenceMinusOne(int last, int current)
    {
        int difference = current - last;
        --difference; // This is to exclude any corners or corner rows. We're just counting between the rows or corners
        return difference;
    }

    private int[] GetRowNumbers()
    {
        int[] rowNumbers = TerrainMap.Keys.ToArray();
        Array.Sort(rowNumbers);
        return rowNumbers;
    }

    private readonly Dictionary<int, TerrainNode> _openCorners = new();
    private Dictionary<int, TerrainNode> OpenCorners
    {
        get
        {
            return _openCorners;
        }
    }

    private Dictionary<int, Dictionary<int, TerrainNode>>? _terrainMap;
    private Dictionary<int, Dictionary<int, TerrainNode>> TerrainMap
    {
        get
        {
            _terrainMap ??= GetTerrainMap();
            return _terrainMap;
        }
    }

    private Dictionary<int, Dictionary<int, TerrainNode>> GetTerrainMap()
    {
        Dictionary<int, Dictionary<int, TerrainNode>> terrainMap = GetBlankTerrainMap();
        DigInstruction[] digInstructions = GetDigInstructions();

        PopulateTerrainMap(terrainMap, digInstructions);

        return terrainMap;
    }

    private static Dictionary<int, Dictionary<int, TerrainNode>> GetBlankTerrainMap()
    {
        var blankTerrainMap = new Dictionary<int, Dictionary<int, TerrainNode>>();
        return blankTerrainMap;
    }

    private DigInstruction[] GetDigInstructions()
    {
        var digInstructions = new List<DigInstruction>();

        foreach (string inputRow in InputRows)
        {
            var digInstruction = new DigInstruction(inputRow);
            digInstructions.Add(digInstruction);
        }

        return digInstructions.ToArray();
    }

    private static void PopulateTerrainMap(Dictionary<int, Dictionary<int, TerrainNode>> terrainMap, DigInstruction[] digInstructions)
    {
        (int x, int y) currentCoordinates = (0, 0);

        for (int i = 0; i < digInstructions.Length; ++i)
        {
            DigInstruction digInstruction = digInstructions[i];
            DigInstruction? nextDigInstruction = i < digInstructions.Length - 1 ? digInstructions[i + 1] : null;

            (int x, int y) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates);
            char cornerDirection = GetCornerDirection(digInstruction.Direction, nextDigInstruction?.Direction);

            AddCornerToTerrain(terrainMap, newCoordinates, cornerDirection);

            currentCoordinates = newCoordinates;
        }
    }

    private static (int, int) GetNewCoordinates(DigInstruction digInstruction, (int x, int y) coordinates)
    {
        int amountOfStepsWithDirection = digInstruction.AmountOfSteps * digInstruction.DirectionCoefficient;

        int changeX = 0;
        int changeY = 0;

        if (digInstruction.IsHorizontal)
        {
            changeX = amountOfStepsWithDirection;
        }

        if (digInstruction.IsVertical)
        {
            changeY = amountOfStepsWithDirection;
        }

        return (coordinates.x + changeX, coordinates.y + changeY);
    }

    private static char GetCornerDirection(char currentDirection, char? nextDirection)
    {
        bool isVertical = "UD".Contains(currentDirection);
        if (isVertical) return currentDirection;

        if (nextDirection is null) throw new Exception("Next direction should not be null at this point.");

        return (char)nextDirection;
    }

    private static void AddCornerToTerrain(Dictionary<int, Dictionary<int, TerrainNode>> terrainMap, (int x, int y) coordinates, char direction)
    {
        bool hasRow = terrainMap.ContainsKey(coordinates.y);
        if (!hasRow)
        {
            terrainMap.Add(coordinates.y, new Dictionary<int, TerrainNode>());
        }

        Dictionary<int, TerrainNode> row = terrainMap[coordinates.y];

        var corner = new TerrainNode(direction);

        row.Add(coordinates.x, corner);
    }
}

public class Day18Task2 : Day18Task1 { }

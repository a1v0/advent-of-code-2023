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
        DigTerrain();
        int area = CalculateArea();
        return area.ToString();
    }

    private int CalculateArea()
    {
        int totalArea = 0;

        int[] rowNumbers = DugTerrain.Keys.ToArray();
        Array.Sort(rowNumbers);

        foreach (int rowNumber in rowNumbers)
        {
            totalArea += CalculateAreaInRow(rowNumber);
        }

        return totalArea;

        // calculate area:
        // - move top to bottom, left to right, through the rows
        // - note the direction at point of entry (up or down)
        //   - if the direction changes, i.e. if you enter on a corner, note the "direction" of the corner: going diagonally up or down
        // - move across, counting every square as you go, until you reach another dug square going in an opposite direction
    }

    private Dictionary<int, Dictionary<int, TerrainNode>>? _dugTerrain;

    private Dictionary<int, Dictionary<int, TerrainNode>> DugTerrain
    {
        get
        {
            _dugTerrain ??= CreateBlankTerrainMap();
            return _dugTerrain;
        }
    }

    private static Dictionary<int, Dictionary<int, TerrainNode>> CreateBlankTerrainMap()
    {
        var dugTerrain = new Dictionary<int, Dictionary<int, TerrainNode>>();
        return dugTerrain;
    }

    private void DigTerrain()
    {
        (int x, int y) currentCoordinates = (0, 0);

        foreach (DigInstruction digInstruction in DigInstructions)
        {
            DigTerrainSection(digInstruction, currentCoordinates);

            (int x, int y) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates);
            currentCoordinates = newCoordinates;
        }
    }

    private void DigTerrainSection(DigInstruction digInstruction, (int, int) currentCoordinates)
    {
        for (int i = 0; i < digInstruction.AmountOfSteps; ++i)
        {
            (int, int) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates, 1);

            var newNode = new TerrainNode(digInstruction.Direction, digInstruction.HexColour);

            UpsertDugTerrain(newCoordinates, newNode);

            currentCoordinates = newCoordinates;
        }
    }

    private static (int, int) GetNewCoordinates(DigInstruction digInstruction, (int x, int y) currentCoordinates, int? amountOfSteps = null)
    {
        amountOfSteps ??= digInstruction.AmountOfSteps;
        int amountOfStepsWithDirection = (int)amountOfSteps * digInstruction.DirectionCoefficient;

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

        return (currentCoordinates.x + changeX, currentCoordinates.y + changeY);
    }

    private void UpsertDugTerrain((int x, int y) coordinates, TerrainNode terrainNode)
    {
        bool hasRow = DugTerrain.ContainsKey(coordinates.y);
        if (!hasRow)
        {
            DugTerrain.Add(coordinates.y, new Dictionary<int, TerrainNode>());
        }

        Dictionary<int, TerrainNode> row = DugTerrain[coordinates.y];
        bool hasColumn = row.ContainsKey(coordinates.x);
        if (!hasColumn)
        {
            row.Add(coordinates.x, terrainNode);
            return;
        }

        row[coordinates.x] = terrainNode;
    }

    private DigInstruction[]? _digInstructions;

    private DigInstruction[] DigInstructions
    {
        get
        {
            _digInstructions ??= GetDigInstructions();
            return _digInstructions;
        }
    }

    private DigInstruction[] GetDigInstructions()
    {
        List<DigInstruction> digInstructions = new();

        foreach (string inputRow in InputRows)
        {
            DigInstruction currentDigInstruction = new(inputRow);
            digInstructions.Add(currentDigInstruction);
        }

        return digInstructions.ToArray();
    }
}

public class Day18Task2 : Day18Task1
{ }

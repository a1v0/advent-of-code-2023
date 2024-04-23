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
            char cornerDirection = GetCornerDirection(digInstruction, nextDigInstruction);

            AddCornerToTerrain(terrainMap, newCoordinates, cornerDirection);

            currentCoordinates = newCoordinates;
        }
    }
}

public class Day18Task2 : Day18Task1 { }

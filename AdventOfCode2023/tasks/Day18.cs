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
        DigInstruction[] digInstructions = GetDigInstructions();
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
}

public class Day18Task2 : Day18Task1 { }

using System.Security.Cryptography.X509Certificates;

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
        // calculate area:
        // - loop through rows
        // - start at leftmost position in the row
        // - count to the right
        //   - empty trench empty trench empty = out in in in out
        //   - empty trench trench trench empty = out in in in out
        //   - empty trench empty trench trench trench empty = out in in in in in out
        //   (hopefully this'll work out!)
        // - quantity of "ins" is the area
    }

    private Dictionary<int, Dictionary<int, string>>? _dugTerrain;

    private Dictionary<int, Dictionary<int, string>> DugTerrain
    {
        get
        {
            _dugTerrain ??= CreateBlankTerrainMap();
            return _dugTerrain;
        }
    }

    private static Dictionary<int, Dictionary<int, string>> CreateBlankTerrainMap()
    {
        var dugTerrain = new Dictionary<int, Dictionary<int, string>>();
        dugTerrain[0] = new Dictionary<int, string>();
        dugTerrain[0][0] = "";
        return dugTerrain;
    }

    private void DigTerrain()
    {
        (int x, int y) currentCoordinates = (0, 0);

        foreach (DigInstruction digInstruction in DigInstructions)
        {
            // dictionary to house all coordinates
            // {
            //    rowNo: {
            //        columnNo: hex colour string or null
            //    }
            // }
            //
            // start at { 0: { 0: "" } }
            // if you go right, create a new dictionary entry, e.g. { 0: { 1: "#234545"} }
            //
            //
            // create base data structure
            // parse inputs using regex
            // loop through inputs to fill in data structure
            DigTerrainSection(digInstruction, currentCoordinates);

            (int x, int y) newCoordinates = GetNewCoordinates(digInstruction.Direction, currentCoordinates);
            currentCoordinates = newCoordinates;
        }
    }

    private static void DigTerrainSection(DigInstruction digInstruction, (int x, int y) currentCoordinates)
    {
        for (int i = 0; i < digInstruction.AmountOfSteps; ++i)
        {
            (int newX, int newY) newCoordinates = GetNewCoordinates(digInstruction.Direction, currentCoordinates, 1);

            UpsertTerrain(newCoordinates, digInstruction.HexColour);

            currentCoordinates = newCoordinates;
        }
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

public class DigInstruction
{
    public DigInstruction(string input)
    {
        Direction = GetDirection(input);
        AmountOfSteps = GetSteps(input);
        HexColour = GetColour(input);
    }

    public string Direction { get; }
    public int AmountOfSteps { get; }
    public string HexColour { get; }
}

using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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
            DigTerrainSection(digInstruction, currentCoordinates);

            (int x, int y) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates);
            currentCoordinates = newCoordinates;
        }
    }

    private void DigTerrainSection(DigInstruction digInstruction, (int x, int y) currentCoordinates)
    {
        for (int i = 0; i < digInstruction.AmountOfSteps; ++i)
        {
            (int x, int y) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates, 1);

            UpsertTerrain(newCoordinates, digInstruction.HexColour);

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

    private void UpsertTerrain((int x, int y) coordinates, string hexColour)
    {
        bool hasRow = DugTerrain.ContainsKey(coordinates.y);
        if (!hasRow)
        {
            DugTerrain.Add(coordinates.y, new Dictionary<int, string>());
        }

        Dictionary<int, string> row = DugTerrain[coordinates.y];
        bool hasColumn = row.ContainsKey(coordinates.x);
        if (!hasColumn)
        {
            row.Add(coordinates.x, hexColour);
            return;
        }

        row[coordinates.x] = hexColour;
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

    private char Direction { get; }
    public int AmountOfSteps { get; }
    public string HexColour { get; }

    public bool IsHorizontal
    {
        get
        {
            return "LR".Contains(Direction);
        }
    }

    public bool IsVertical
    {
        get
        {
            return "UD".Contains(Direction);
        }
    }

    /// <summary>
    /// If the direction goes in a positive axial direction,
    /// i.e. up or right, then the coefficient is 1. Else -1.
    /// </summary>
    public int DirectionCoefficient
    {
        get
        {
            int coefficient = 1;

            if ("DL".Contains(Direction)) coefficient *= -1;

            return coefficient;
        }
    }

    private static char GetDirection(string input)
    {
        return input[0];
    }

    private static int GetSteps(string input)
    {
        string stepsRegexPattern = @"[0-9]+";
        var stepsRegex = new Regex(stepsRegexPattern);

        Match foundSteps = stepsRegex.Match(input);
        string steps = foundSteps.Value;

        return int.Parse(steps);
    }

    private static string GetColour(string input)
    {
        string colourRegexPattern = @"#[a-z0-9]{6}";
        var colourRegex = new Regex(colourRegexPattern);

        Match foundColour = colourRegex.Match(input);
        return foundColour.Value;
    }
}

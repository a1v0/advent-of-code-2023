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
        // - refactor data structure so that, instead of a string of hex colour, we have a new class containing direction as well as hex
        //   - direction needs to be more sophisticated, if poss, than the four directions given byt he input
        //   - places where the direction changes need to be marked somehow (unless there's a way around this)
        //   - the starting point, (0,0), doesn't have any direction, so needs to be added at the end
        // - move top to bottom, left to right, through the rows
        // - note the direction at point of entry (up or down)
        //   - if the direction changes, i.e. if you enter on a corner, note the "direction" of the corner: going diagonally up or down
        // - move across, counting every square as you go, until you reach another dug square going in an opposite direction
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

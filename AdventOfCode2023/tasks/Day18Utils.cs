using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class DigInstruction
{
    public DigInstruction(string input)
    {
        Direction = GetDirection(input);
        AmountOfSteps = GetSteps(input);
        // HexColour = GetColour(input);
    }

    public char Direction { get; }
    public int AmountOfSteps { get; }
    // public string HexColour { get; }

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

    // private static string GetColour(string input)
    // {
    //     string colourRegexPattern = @"#[a-z0-9]{6}";
    //     var colourRegex = new Regex(colourRegexPattern);

    //     Match foundColour = colourRegex.Match(input);
    //     return foundColour.Value;
    // }
}

public class TerrainNode
{
    public TerrainNode(char direction)
    {
        Direction = direction;
    }

    public char Direction { get; set; }
}

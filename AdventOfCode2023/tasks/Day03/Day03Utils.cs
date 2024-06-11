namespace AdventOfCode2023;

public class SchematicNumber
{
    public SchematicNumber(string value, int x, int y)
    {
        Value = value;
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
    public string Value { get; }
}
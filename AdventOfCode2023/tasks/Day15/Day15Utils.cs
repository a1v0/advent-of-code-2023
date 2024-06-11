namespace AdventOfCode2023;

public class Box
{
    public Box(int number)
    {
        Number = number;
    }

    public int Number { get; }

    public List<string> Labels { get; } = new List<string>();

    public Dictionary<string, byte> Lenses { get; } = new Dictionary<string, byte>();
}
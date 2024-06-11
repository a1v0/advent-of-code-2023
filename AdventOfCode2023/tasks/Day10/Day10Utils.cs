namespace AdventOfCode2023;

public class Pipe
{
    public Pipe(char value, int x, int y)
    {
        _value = value;
        _x = x;
        _y = y;
    }

    private readonly int _x;
    public int X
    {
        get
        {
            return _x;
        }
    }

    private readonly int _y;
    public int Y
    {
        get
        {
            return _y;
        }
    }

    private readonly char _value;
    public char Value
    {
        get
        {
            return _value;
        }
    }
}

/// <summary>
/// Default state is -1 (closed/off); the opposite state is 1 (open/on). Use the `Change` method to switch between the two states.
/// </summary>
public class Toggle
{
    public Toggle()
    {
        _state = -1;
    }

    private int _state;
    public int State
    {
        get
        {
            return _state;
        }
    }

    public void Change()
    {
        _state *= -1;
    }
}
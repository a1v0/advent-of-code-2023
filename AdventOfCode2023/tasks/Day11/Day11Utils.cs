namespace AdventOfCode2023;

public class Galaxy
{
    public Galaxy(int id, int x, int y)
    {
        _id = id;
        _x = x;
        _y = y;
    }

    private int _id;
    public int ID
    {
        get
        {
            return _id;
        }
    }
    private int _x;
    public int X
    {
        get
        {
            return _x;
        }
    }
    private int _y;
    public int Y
    {
        get
        {
            return _y;
        }
    }
}

namespace AdventOfCode2023;

public class Pulse
{
    public Pulse(string type, string destination, string source)
    {
        if (type != "high" && type != "low")
        {
            throw new Exception("Invalid Pulse type given: '" + type + "'.");
        }

        _type = type;
        _destination = destination;
        _source = source;
    }

    private string _type;
    private string Type
    {
        get
        {
            return _type;
        }
    }

    public bool IsHigh
    {
        get
        {
            return Type == "high";
        }
    }

    public bool IsLow
    {
        get
        {
            return !IsHigh;
        }
    }

    private string _destination;
    public string Destination
    {
        get
        {
            return _destination;
        }
    }

    private string _source;
    public string Source
    {
        get
        {
            return _source;
        }
    }
}

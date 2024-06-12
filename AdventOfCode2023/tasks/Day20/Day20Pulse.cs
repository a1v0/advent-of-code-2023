namespace AdventOfCode2023;

public class Pulse
{
    public Pulse(string type, BaseModule destination)
    {
        if (type!="high"&&type!="low")
        {
            throw new Exception("Invalid Pulse type given: '"+type+"'.");
        }

        _type = type;
        _destination = destination;
    }

    private string _type;
    private string Type
    {
        get
        {
            return _type;
        }
    }
}

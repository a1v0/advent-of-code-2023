namespace AdventOfCode2023;

public class BaseModule
{
    public BaseModule(string[] destinations)
    {
        _destinations = destinations;
    }

    private string[] _destinations;
    protected string[] Destinations
    {
        get
        {
            return _destinations;
        }
    }

    public virtual void EmitPulses()
    {
        throw new Exception("You should not be using this method. Instead, create an instance of a child Module class.");
    }

    public virtual void IngestPulse(Pulse pulse)
    {
        throw new Exception("You should not be using this method. Instead, create an instance of a child Module class.");
    }
}

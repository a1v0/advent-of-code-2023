namespace AdventOfCode2023;

public class BaseModule : IBaseModule
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

    public void EmitPulses()
    {
        throw new Exception("You should not be using this method. Instead, create an instance of a child Module class.");
    }

    public void IngestPulse(Pulse pulse)
    {
        throw new Exception("You should not be using this method. Instead, create an instance of a child Module class.");
    }
}

public interface IBaseModule
{
    public void EmitPulses();
    public void IngestPulse(Pulse pulse);
}

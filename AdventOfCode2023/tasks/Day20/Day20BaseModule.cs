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
}

public interface IBaseModule
{
    public void EmitPulses();
    public void IngestPulse(Pulse pulse);
}

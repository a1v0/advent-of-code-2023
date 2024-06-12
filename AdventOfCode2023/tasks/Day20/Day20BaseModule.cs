namespace AdventOfCode2023;

public class BaseModule
{
    private List<string> _destinations = new();
    protected List<string> Destinations
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

namespace AdventOfCode2023;

public class BaseModule
{
    // I'm not overly happy about the implementation here.
    // I'd've preferred making this an abstract class, but
    // that wouldn't work, given that I needed to use
    // BaseModule as a type when creating collections of modules.
    // 
    // I experimented with using an interface, but then I'd
    // need to recreate the Destinations property each time.

    public BaseModule(string[] destinations)
    {
        _destinations = destinations;
    }

    private string[] _destinations;
    public string[] Destinations
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

namespace AdventOfCode2023;

public class PulseQueue
{
    private static List<Pulse> _queue = new();
    public static List<Pulse> Queue
    {
        get
        {
            return _queue;
        }
    }

    public void Add(Pulse pulse)
    {
        Queue.Add(pulse);
    }

    private static int _lowPulseTally;
    private static int LowPulseTally
    {
        get
        {
          return _lowPulseTally;
        }
        set
        {
          ++_lowPulseTally;
        }
    }
}

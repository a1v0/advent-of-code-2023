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

    public static void Add(Pulse pulse)
    {
        Queue.Add(pulse);

        if (pulse.IsHigh) ++HighPulseTally;
        else ++LowPulseTally;
    }

    public static void Clear()
    {
        Queue.Clear();
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

    private static int _highPulseTally;
    private static int HighPulseTally
    {
        get
        {
            return _highPulseTally;
        }
        set
        {
            ++_highPulseTally;
        }
    }

    public static int GetProductOfTallies()
    {
        return HighPulseTally * LowPulseTally;
    }
}

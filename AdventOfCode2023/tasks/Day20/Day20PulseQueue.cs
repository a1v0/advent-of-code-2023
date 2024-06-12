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
}

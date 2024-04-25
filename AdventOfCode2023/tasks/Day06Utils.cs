namespace AdventOfCode2023;

public class Race
{
    public Race(string time, string record)
    {
        _time = long.Parse(time);
        _record = long.Parse(record);
    }

    private long _time;
    public long Time
    {
        get
        {
            return _time;
        }
    }

    private long _record;
    public long Record
    {
        get
        {
            return _record;
        }
    }
}
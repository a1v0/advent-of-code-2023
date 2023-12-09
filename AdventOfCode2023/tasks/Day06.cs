namespace AdventOfCode2023;

public class Day06 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day06Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day06Task2();
}

public class Day06Task1 : BaseTask
{
    public override string Solve()
    {
        // create list property to store quantity of beaten records per race
        // parse input as races
        // loop through races
        // loop through seconds in each race and find numbers that'd beat the record
        // multiple quantities, stringify and return
    }

    private List<int> _beatenRecords = new List<int>();
    private List<int> BeatenRecords
    {
        get
        {
            return _beatenRecords;
        }
    }
}

public class Day06Task2 : Day06Task1
{ }

public class Race
{
    public Race(string time, string record)
    {
        _time = int.Parse(time);
        _record = int.Parse(record);
    }

    private int _time;
    public int Time
    {
        get
        {
            return _time;
        }
    }

    private int _record;
    public int Record
    {
        get
        {
            return _record;
        }
    }
}
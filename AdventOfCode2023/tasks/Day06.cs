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

    private Race[]? _races;
    private Race[] Races
    {
        get
        {
            _races ??= GetRaces();
            return _races;
        }
    }

    private Race[] GetRaces()
    {
        (string[] times, string[] records) = SplitInput();
        var races = new Race[times.Length];

        for (int i = 0; i < races.Length; ++i)
        {
            string currentTime = times[i],
                   currentRecord = records[i];
            var race = new Race(currentTime, currentRecord);
            races[i] = race;
        }

        return races;
        // split into lines
        // split into numbers via regex
        // create array
        // parse strings into Race
        // return
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
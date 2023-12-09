using System.Text.RegularExpressions;

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
    }

    private (string[], string[]) SplitInput()
    {
        // split into lines
        // split into numbers via regex
        string timeRow = InputRows[0],
               recordRow = InputRows[1];
        var spaceRegex = new Regex(@"\s+");
        string[] times = spaceRegex.Split(timeRow)[1..]; // I could probably have made this a bit more elegant...
        string[] records = spaceRegex.Split(recordRow)[1..];

        return (times, records);
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
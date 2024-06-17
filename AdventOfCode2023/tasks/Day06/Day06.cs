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
        PopulateBeatenRecords();
        long[] beatenRecords = BeatenRecords.ToArray();
        long product = beatenRecords.Aggregate((a, b) => a * b);
        return product.ToString();
    }

    private void PopulateBeatenRecords()
    {
        foreach (Race race in Races)
        {
            long recordsBeaten = GetRecordsBeaten(race);
            BeatenRecords.Add(recordsBeaten);
        }
    }

    private static long GetRecordsBeaten(Race race)
    {
        int recordsBroken = 0;
        for (long buttonPressDuration = 1; buttonPressDuration < race.Time; ++buttonPressDuration)
        {
            long remainingDuration = race.Time - buttonPressDuration;
            long distanceTravelled = buttonPressDuration * remainingDuration;

            if (distanceTravelled > race.Record) ++recordsBroken;
        }
        return recordsBroken;
    }

    private List<long> _beatenRecords = new List<long>();
    private List<long> BeatenRecords
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

    protected virtual (string[], string[]) SplitInput()
    {
        string timeRow = InputRows[0],
               recordRow = InputRows[1];
        var spaceRegex = new Regex(@"\s+");
        string[] times = spaceRegex.Split(timeRow)[1..]; // I could probably have made this a bit more elegant...
        string[] records = spaceRegex.Split(recordRow)[1..];

        return (times, records);
    }
}

public class Day06Task2 : Day06Task1
{
    protected override (string[], string[]) SplitInput()
    {
        string timeRow = InputRows[0],
               recordRow = InputRows[1];

        string timeWithoutTitle = timeRow.Replace("Time:", ""),
               recordWithoutTitle = recordRow.Replace("Distance:", "");

        string[] time = new string[] { timeWithoutTitle.Replace(" ", "") },
                 record = new string[] { recordWithoutTitle.Replace(" ", "") };

        return (time, record);
    }
}

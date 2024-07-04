namespace AdventOfCode2023;

public class Day21 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day21Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day21Task2();
}

public class Day21Task1 : BaseTask
{
    public override string Solve()
    {
        /**
         * This is in desperate need of a refactor.
         * This variable is only being declared so
         * that GetGardenPlots is called, without
         * which the whole thing doesn't work.
         *
         *
         *
         *
         */
        var unusedVariable = GardenPlots;

        StepThroughGardenPlots();
        int totalActivePlots = ActivePlots.Count;
        return totalActivePlots.ToString();
    }

    private void StepThroughGardenPlots()
    {
        int noOfSteps = InputRows.Length < 100 ? 6 : 64; // This is a bodgy way to distinguish between the unit test and the real thing

        for (int i = 0; i < noOfSteps; ++i)
        {
            ActivePlots = UpdateActivePlots();
        }
    }

    private HashSet<GardenPlot> UpdateActivePlots()
    {
        HashSet<GardenPlot> newActivePlots = new();
        foreach (GardenPlot gardenPlot in ActivePlots)
        {
            foreach (GardenPlot neighbour in gardenPlot.Neighbours)
            {
                newActivePlots.Add(neighbour);
            }
        }

        return newActivePlots;
    }

    private HashSet<GardenPlot> _activePlots = new();
    private HashSet<GardenPlot> ActivePlots
    {
        get
        {
            return _activePlots;
        }

        set
        {
            _activePlots = value;
        }
    }

    private Dictionary<(int x, int y), GardenPlot>? _gardenPlots;
    private Dictionary<(int x, int y), GardenPlot> GardenPlots
    {
        get
        {
            _gardenPlots ??= GetGardenPlots();
            return _gardenPlots;
        }
    }

    private Dictionary<(int, int), GardenPlot> GetGardenPlots()
    {
        Dictionary<(int, int), GardenPlot> gardenPlots = new();

        for (int y = 0; y < InputRows.Length; ++y)
        {
            string currentRow = InputRows[y];

            for (int x = 0; x < currentRow.Length; ++x)
            {
                if (currentRow[x] == '#') continue;

                GardenPlot newPlot = new();

                if (currentRow[x] == 'S')
                {
                    newPlot.IsStart = true;
                }

                gardenPlots.Add((x, y), newPlot);
            }
        }

        SetPlotNeighbours(gardenPlots);

        return gardenPlots;
    }

    private static void SetPlotNeighbours(Dictionary<(int, int), GardenPlot> gardenPlots)
    {
        foreach (KeyValuePair<(int, int), GardenPlot> pair in gardenPlots)
        {
            GardenPlot plot = pair.Value;
            (int x, int y) coordinates = pair.Key;

            (int, int) north = (coordinates.x, coordinates.y - 1),
                       south = (coordinates.x, coordinates.y + 1),
                       east = (coordinates.x + 1, coordinates.y),
                       west = (coordinates.x - 1, coordinates.y);

            if (gardenPlots.ContainsKey(north))
            {
                plot.Neighbours.Add(gardenPlots[north]);
            }

            if (gardenPlots.ContainsKey(south))
            {
                plot.Neighbours.Add(gardenPlots[south]);
            }

            if (gardenPlots.ContainsKey(east))
            {
                plot.Neighbours.Add(gardenPlots[east]);
            }

            if (gardenPlots.ContainsKey(west))
            {
                plot.Neighbours.Add(gardenPlots[west]);
            }
        }
    }
}

public class Day21Task2 : Day21Task1
{ }

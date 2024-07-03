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
        // create GardenPlot class
        // - list of neighbours
        // - coordinates?
        // parse input as GardenPlots
        // create global HashSet of "active" plots
        // loop through active plots, add each plot's neighbours to new HashSet of active plots and deactivate current plot
        // do this X times and return total active plots
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
        // create blank dictionary
        // loop through inputs in nested loop
        // create new garden plots accordingly
        // loop through plots and identify neighbours
        Dictionary<(int, int), GardenPlot> gardenPlots = new();

        for (int y = 0; y < InputRows.Length; ++y)
        {
            string currentRow = InputRows[y];

            for (int x = 0; x < currentRow.Length; ++x)
            {
                if (currentRow[x] == '#') continue;

                gardenPlots.Add((x, y),new GardenPlot());
            }
        }

        SetPlotNeighbours(gardenPlots);

        return gardenPlots;
    }
}

public class Day21Task2 : Day21Task1
{ }

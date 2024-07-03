namespace AdventOfCode2023;

public class GardenPlot
{
    public GardenPlot((int x, int y) coordinates)
    {

    }

    private List<GardenPlot> _neighbours = new();
    public List<GardenPlot> Neighbours
    {
        get;
    }
}
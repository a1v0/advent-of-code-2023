namespace AdventOfCode2023;

public class GardenPlot
{
    private List<GardenPlot> _neighbours = new();
    public List<GardenPlot> Neighbours
    {
        get
        {
            return _neighbours;
        }
    }
}

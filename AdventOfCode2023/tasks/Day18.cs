namespace AdventOfCode2023;

public class Day18 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day18Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day18Task2();
}

public class Day18Task1 : BaseTask
{
    public override string Solve()
    {
        int area = CalculateArea();
        return area.ToString();
    }

    private int CalculateArea()
    {
        // calculate area:
        // - loop through rows
        // - start at leftmost position in the row
        // - count to the right
        //   - empty trench empty trench empty = out in in in out
        //   - empty trench trench trench empty = out in in in out
        //   - empty trench empty trench trench trench empty = out in in in in in out
        //   (hopefully this'll work out!)
        // - quantity of "ins" is the area
    }

    private Dictionary<int, Dictionary<int, string>>? _dugTerrain;

    private Dictionary<int, Dictionary<int, string>> DugTerrain
    {
        get
        {
            _dugTerrain ??= ParseDugTerrain();
            return _dugTerrain;
        }
    }

    private Dictionary<int, Dictionary<int, string>> ParseDugTerrain()
    {
        Dictionary<int, Dictionary<int, string>> dugTerrain = CreateBlankTerrainMap();
        ParseInputToTerrain(dugTerrain);
        return dugTerrain;
    }

    private static Dictionary<int, Dictionary<int, string>> CreateBlankTerrainMap()
    {
        var dugTerrain = new Dictionary<int, Dictionary<int, string>>();
        dugTerrain[0] = new Dictionary<int, string>();
        dugTerrain[0][0] = "";
        return dugTerrain;
    }

    private void ParseInputToTerrain(Dictionary<int, Dictionary<int, string>> dugTerrain)
    {
        (int x, int y) currentCoordinates = (0, 0);

        foreach (string inputRow in InputRows)
        {
            // dictionary to house all coordinates
            // {
            //    rowNo: {
            //        columnNo: hex colour string or null
            //    }
            // }
            //
            // start at { 0: { 0: "" } }
            // if you go right, create a new dictionary entry, e.g. { 0: { 1: "#234545"} }
            //
            //
            // create base data structure
            // parse inputs using regex
            // loop through inputs to fill in data structure
        }
    }
}

public class Day18Task2 : Day18Task1
{ }

public class DigInstruction
{
    public DigInstruction(string input)
    {
        
    }

    public string Direction {get;}
    public int AmountOfSteps {get;}
}

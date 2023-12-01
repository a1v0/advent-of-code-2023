namespace AdventOfCode2023;

public class Day01 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day01Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day01Task2();
}

public class Day01Task1 : BaseTask
{

    private string[] UnsanitisedCalibrations
    {
    get;
    } = ParseInput();

    public override string Solve()
    {
        int[] calibrations = SanitiseCalibrations();
    }

    private int[] SanitiseCalibrations()
    {
        int[] sanitisedCalibrations = new int[UnsanitisedCalibrations.Length];
        // 
        // loop through each line
        // loop through each char
        // since TryParse will converrt chars to ints, it's probably easier for now to use a regex to check for no. from 1–9
        // try using one of C#'s double loops to count down as well as up
        // ensure that, if there's only one int, that the int doesn't get counted twice
        // combine both numbers, convert to int and add to sanitisedCals
        // sum array
        // return
        // 
    }

    private string[] ParseInput()
    {
        return Input.Split('\n');
    }
}

public class Day01Task2 : Day01Task1
{

}

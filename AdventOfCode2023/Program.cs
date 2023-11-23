namespace AdventOfCode2023;

internal class Program
{
    static void Main(string[] args)
    {
        var parsedArgs = AOCUtils.ParseInitialArgs(args);
        (chosenDay, chosenTask) = parsedArgs;  


        //
        //
        //
        //
        //
        //
        // since this switch is likely to get pretty big, this could be moved into a separate method on AOCUtils
        // is there a way to call a class based off a string? E.g. "Day" + chosenDay; ?
        //   - there IS a way to do this, but it's slow. Having a switch is preferred
        switch (chosenDay) {
            case 1:
                Day01.Solve(chosenChallenge);
                break;
            default:
                throw new Exception("Something went wrong. Please try again.");
                return;
        }
        
        
        Console.WriteLine(Day01.DayName);
        Console.WriteLine(AOCUtils.GetRawInput(Day01.DayName));
    }
}


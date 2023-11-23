namespace AdventOfCode2023;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(Day01.DayName);
        Console.WriteLine(AOCUtils.GetRawInput(Day01.DayName));

        
        var parsedArgs = AOCUtils.ParseInitialArgs(args);
        (chosenDay, chosenTask) = parsedArgs;  

       SolveChosenTask(chosenDay, chosenTask);
        
    }

   private static void SolveChosenTask(int chosenDay, int chosenTask)
    {
        switch (chosenDay) {
            case 1:
                Day01.Solve(chosenTask);
                break;
            default:
                throw new Exception("Something went wrong. Please try again.");
        }
    }
}

namespace AdventOfCode2023;

internal class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine(Day01.DayName);
        //Console.WriteLine(AOCUtils.GetRawInput(Day01.DayName));

        
        var parsedArgs = ParseInitialArgs(args);
        (chosenDay, chosenTask) = parsedArgs;  

       SolveChosenTask(chosenDay, chosenTask);
        
    }

   private static void SolveChosenTask(int chosenDay, int chosenTask)
    {
        switch (chosenDay) {
            case 1:
               new Day01().Solve(chosenTask);
                break;
            default:
                throw new Exception("Something went wrong. Please try again.");
        }
    }

    private static bool EnoughArgs(string[] args)
    {
        return args.Length >= 2;
    }

    private static int ParseDayArg(string day){
        int parsedDay;
        try
        {
            parsedDay = Int32.Parse(day);
        }
        catch (Exception e)
        {
            throw new Exception("Invalid Day argument provided.");
        }

        bool dayNotInRange = parsedDay < 1 || parsedDay > 25;

        if (dayNotInRange)
        {
            throw new Exception("Day argument needs to be between 1 and 25.");
        }

        return parsedDay;
    }
    
    private static int ParseTaskArg(string task){
         int parsedTask;
        try
        {
            parsedTask = Int32.Parse(task);
        }
        catch(Exception e)
        {
            throw new Exception("Invalid Task argument provided.");
        }

        bool taskNotInRange = parsedTask != 1 && parsedTask != 2;

        if (taskNotInRange)
        {
            throw new Exception("Task argument needs to be 1 or 2.");
        }

        return parsedTask;
    }

    private static (int, int) ParseInitialArgs(string[] args)
    {
        bool notEnoughArgs = !EnoughArgs(args);
        if (notEnoughArgs) throw new Exception("Not enough arguments given. Two are required: Day and Task.");
        
        string chosenDay = args[0];
        string chosenChallenge = args[1];

        int parsedDay = ParseDayArg(chosenDay);
        int parsedTask = ParseTaskArg(chosenChallenge);

        return (parsedDay, parsedTask);
    }
}

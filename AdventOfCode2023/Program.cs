namespace AdventOfCode2023;

internal class Program
{
    static void Main(string[] args)
    {
        (byte chosenDay, byte chosenTask) = ParseInitialArgs(args);

        SolveChosenTask(chosenDay, chosenTask);
    }

    private static void SolveChosenTask(byte chosenDay, byte chosenTask)
    {
        switch (chosenDay)
        {
            case 1:
                new Day01().Solve(chosenTask);
                break;
            case 2:
                new Day02().Solve(chosenTask);
                break;
            case 3:
                new Day03().Solve(chosenTask);
                break;
            case 4:
                new Day04().Solve(chosenTask);
                break;
            case 5:
                new Day05().Solve(chosenTask);
                break;
            default:
                throw new Exception("Something went wrong. Please try again.");
        }
    }

    private static bool EnoughArgs(string[] args)
    {
        return args.Length >= 2;
    }

    private static byte ParseDayArg(string day)
    {
        byte parsedDay;
        bool dayIsNumber = Byte.TryParse(day, out parsedDay);
        bool dayIsInvalid = !dayIsNumber || parsedDay < 1 || parsedDay > 25;

        if (dayIsInvalid)
        {
            throw new Exception("Day argument must be an integer between 1 and 25.");
        }

        return parsedDay;
    }

    private static byte ParseTaskArg(string task)
    {
        byte parsedTask;
        bool taskIsNumber = Byte.TryParse(task, out parsedTask);
        bool taskIsInvalid = !taskIsNumber || (parsedTask != 1 && parsedTask != 2);

        if (taskIsInvalid)
        {
            throw new Exception("Task argument must be 1 or 2.");
        }

        return parsedTask;
    }

    private static (byte, byte) ParseInitialArgs(string[] args)
    {
        bool notEnoughArgs = !EnoughArgs(args);
        if (notEnoughArgs) throw new Exception("Not enough arguments given. Two are required: Day and Task.");

        string chosenDay = args[0];
        string chosenTask = args[1];

        byte parsedDay = ParseDayArg(chosenDay);
        byte parsedTask = ParseTaskArg(chosenTask);

        return (parsedDay, parsedTask);
    }
}

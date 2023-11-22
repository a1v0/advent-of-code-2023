namespace AdventOfCode2023;

internal class Program
{
    static void Main(string[] args)
    {
        bool enoughArgs = AOCUtils.EnoughArgs(args);

        if (!enoughArgs)
        {
            // throw error here, or request user input
        }

        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // this could also be parsed as an integer
        string chosenDay = args[0];
        string chosenChallenge = args[1];

        // 
        // what if we did something like this:
        // - create ParseArgs method
        //   - returns a tuple of ints with both args
        //   - ignores any args beyond the first two
        //   - checks we have enough args
        //     - throw different exception if not enough args given to the exception thrown when args are invalid
        //   - checks if arg is valid (between 1 and 25, etc.) and parses as an int
        //   - throws exception if not
        //     - since I'm realistically going to be the only user, there's no need to ask for user input for better UX
        // 
        // 
        // 
        // 
        // 



        //
        //
        //
        //
        //
        //
        // since this switch is likely to get pretty big, this could be moved into a separate method on AOCUtils
        // is there a way to call a class based off a string? E.g. "Day" + chosenDay; ?
        switch (chosenDay) {
            case 1:
                // pass challenge number to Day01 here, and run Solve() method
                break;
            default:
                Console.WriteLine("Something went wrong. Please try again.");
                // or you could throw an exception
                return;
        }
        
        
        Console.WriteLine(Day01.DayName);
        Console.WriteLine(AOCUtils.GetRawInput(Day01.DayName));

        // 
        // 
        // accept two args from console (e.g. dotnet run 01 1)
        // parse inputs as integers and check they're 1-25 and 1 or 2 respectively
        // switch statement to load correct day's class
        // add Run method (or similar name) to each day class which takes a parameter of 1 or 2 to define which challenge
        // 
        // create parent class for all days
        // consider setting the day name automatically by doing some string manipulation using something like this.GetType().Name
        // that will remove much of the boilerplate needed for each day
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
    }
}


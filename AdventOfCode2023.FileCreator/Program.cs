namespace AdventOfCode2023.FileCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dayName = ParseArgs(args);
            FileCreatorUtils.CreateDay(dayName);
            Console.WriteLine($"Blank classes and tests created for Day {dayName}.");
            Console.WriteLine("\nDon't forget to add today to the `switch` statement in Program!");
        }

        static string ParseArgs(string[] args)
        {
            string dayName = args[0];

            bool isInt = int.TryParse(dayName, out int result);
            var error = new Exception("Invalid argument given.");

            if (!isInt) throw error;

            bool resultInRange = result >= 1 && result <= 25;
            if (!resultInRange) throw error;

            if (dayName.Length == 1) dayName = "0" + dayName;

            return dayName;
        }
    }

    public class FileCreatorUtils
    {
        public static void CreateDay(string dayName)
        {
            CreateClass(dayName);
            CreateTest(dayName);
        }

        private static void CreateClass(string dayName)
        {
            bool classExists = File.Exists(BasePath + $"/AdventOfCode2023/tasks/Day{dayName}.cs");
            if (classExists)
            {
                Console.WriteLine($"Class file for Day {dayName} already exists.");
                return;
            }
            // get file contents
            // replace strings as necessary
            // create .cs file at appropriate path
        }

        private static void CreateTest(string dayName)
        {
            bool testExists = File.Exists(BasePath + $"/AdventOfCode2023.Tests/Day{dayName}Tests.cs");
            if (testExists)
            {
                Console.WriteLine($"Test file for Day {dayName} already exists.");
                return;
            }
            // get file contents
            // replace strings as necessary
            // create .cs file at appropriate path
        }

        private static string BasePath
        {
            get
            {
                return "/home/alvo/advent-of-code/advent-of-code-2023";
            }
        }
    }
}
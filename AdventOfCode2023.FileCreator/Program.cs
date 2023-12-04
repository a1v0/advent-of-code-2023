namespace AdventOfCode2023.FileCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dayName = ParseArgs(args);
            CreateDay(dayName);
            Console.WriteLine($"Blank classes and tests created for Day {dayName}.");
            Console.WriteLine("\nDon't forget to add today to the `switch` statement in Program!");
        }

        private void CreateDay(string dayName)
        {
            bool classExists = File.Exists(BasePath + $"/AdventOfCode2023/tasks/Day{dayName}.cs"),
            testExists = File.Exists(BasePath + $"/AdventOfCode2023.Tests/Day{dayName}Tests.cs");

            if (classExists)
            {
                Console.WriteLine($"Class file for Day {dayName} already exists.");
            }
            else
            {
                // otherwise create file
            }

            if (testExists)
            {
                Console.WriteLine($"Test file for Day {dayName} already exists.");
            }
            else
            {
                // otherwise create file
            }
        }

        public string BasePath
        {
            get
            {
                return "/home/alvo/advent-of-code/advent-of-code-2023";
            }
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
}

namespace AdventOfCode2023.FileCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dayName = ParseArgs(args);
            CreateDay(dayName);
            Console.WriteLine($"Blank classes and tests created for Day {dayName}.");
        }

        static void CreateDay(string dayName)
        {
            string basePath = "/home/alvo/advent-of-code/advent-of-code-2023";
        }

        static string ParseArgs(string[] args)
        {
            bool isInt = int.TryParse(args[0], out int result);
            var error = new Exception("Invalid argument given.");

            if (!isInt) throw error;

            bool resultInRange = result >= 1 && result <= 25;
            if (!resultInRange) throw error;

            return args[0];
        }
    }
}

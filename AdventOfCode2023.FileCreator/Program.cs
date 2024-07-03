namespace AdventOfCode2023.FileCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dayName = ParseArgs(args);
            Console.WriteLine($"CREATING FILES FOR DAY {dayName}.\n--------------------------");
            FileCreatorUtils.CreateDay(dayName);
            Console.WriteLine($"\nOperation complete for Day {dayName}.");
            Console.WriteLine("\nDon't forget to add today to the `switch` statement in Program!");
        }

        static string ParseArgs(string[] args)
        {
            string dayName = args[0];

            bool isInt = int.TryParse(dayName, out int result);
            var error = new Exception("Invalid argument given. Must be integer between 1 and 25.");

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
            CreateInputFiles(dayName);
        }

        private static void CreateInputFiles(string dayName)
        {
            string inputFolderPath = BasePath + "/AdventOfCode2023/inputs";
            string inputPath = inputFolderPath + $"/{dayName}.txt",
            testInputPath = inputFolderPath + $"/{dayName}.test.txt";

            bool inputExists = File.Exists(inputPath);
            if (inputExists)
            {
                Console.WriteLine($"Input file for Day {dayName} already exists.");
            }
            else
            {
                File.Create(inputPath);
                Console.WriteLine($"Day {dayName} input file created successfully.");
            }

            bool testInputExists = File.Exists(testInputPath);
            if (testInputExists)
            {
                Console.WriteLine($"Test input file for Day {dayName} already exists.");
            }
            else
            {
                File.Create(testInputPath);
                Console.WriteLine($"Day {dayName} test input file created successfully.");
            }
        }

        private static void CreateClass(string dayName)
        {
            string fullPath = BasePath + $"/AdventOfCode2023/tasks/Day{dayName}";
            CreateFolder(fullPath);

            fullPath += $"/Day{dayName}.cs";

            bool classExists = File.Exists(fullPath);
            if (classExists)
            {
                Console.WriteLine($"Class file for Day {dayName} already exists.");
                return;
            }

            string fileTemplate = File.ReadAllText("blank-files/blank-class.txt");
            string fileContents = fileTemplate.Replace("DAY_NAME_HERE", dayName);
            File.WriteAllText(fullPath, fileContents);

            Console.WriteLine($"Day {dayName} class created successfully.");
        }

        private static void CreateFolder(string fullPath)
        {
            if (Directory.Exists(fullPath)) return;

            Directory.CreateDirectory(fullPath);
        }

        private static void CreateTest(string dayName)
        {
            string fullPath = BasePath + $"/AdventOfCode2023.Tests/Day{dayName}Tests.cs";
            bool testExists = File.Exists(fullPath);
            if (testExists)
            {
                Console.WriteLine($"Test file for Day {dayName} already exists.");
                return;
            }

            string fileTemplate = File.ReadAllText("blank-files/blank-test.txt");
            string fileContents = fileTemplate.Replace("DAY_NAME_HERE", dayName);
            fileContents = fileContents.Replace("DAY_NAME_INT_HERE", int.Parse(dayName).ToString());
            File.WriteAllText(fullPath, fileContents);

            Console.WriteLine($"Day {dayName} unit test suite created successfully.");
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
namespace AdventOfCode2023.Tests;

public class Day11Tests : BaseTest
{
    [Trait("dp", "11,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "374";
        string actual = new Day11().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "11,2")]
    [Fact]
    public void Test2()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\nNOTE: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("THIS TEST WILL ONLY PASS IF THE SUPPLEMENT QUANTITY IS 100, NOT 1000000.");
        Console.WriteLine("      I DON'T HAVE ACCESS TO THE CORRECT ANSWER FOR 1000000.");

        const string expected = "8410";
        string actual = new Day11().Solve(2);
        Assert.Equal(expected, actual);
    }
}

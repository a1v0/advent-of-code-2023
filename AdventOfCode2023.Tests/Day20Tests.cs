namespace AdventOfCode2023.Tests;

public class Day20Tests : BaseTest
{
    [Trait("dp", "20,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "32000000";
        string actual = new Day20().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "20,1")]
    [Fact]
    public void Test1a()
    {
        UseSecondTestInput();
        const string expected = "11687500";
        string actual = new Day20().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "20,2")]
    [Fact]
    public void Test2()
    {
        // Since I have no expected value for the test input, I've left the default value
        // I've kept the unit test here, though, as it's the easiest way to run the debugger
        const string expected = "RANDOM_VALUE_TO_ENSURE_TEST_FAILS_UNTIL_REAL_VALUE_IS_ENTERED";
        string actual = new Day20().Solve(2);
        Assert.Equal(expected, actual);
    }
}

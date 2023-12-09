namespace AdventOfCode2023.Tests;

public class Day07Tests : BaseTest
{
    [Trait("dp", "7,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "6440";
        string actual = new Day07().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "7,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "RANDOM VALUE TO ENSURE TEST FAILS UNTIL REAL VALUE IS ENTERED";
        string actual = new Day07().Solve(2);
        Assert.Equal(expected, actual);
    }
}

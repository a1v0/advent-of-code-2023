namespace AdventOfCode2023.Tests;

public class Day06Tests : BaseTest
{
    [Trait("dp", "6,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "288";
        string actual = new Day06().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "6,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "RANDOM VALUE TO ENSURE TEST FAILS UNTIL REAL VALUE IS ENTERED";
        string actual = new Day06().Solve(2);
        Assert.Equal(expected, actual);
    }
}

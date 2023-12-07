namespace AdventOfCode2023.Tests;

public class Day04Tests : BaseTest
{
    [Trait("dp", "4,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "RANDOM VALUE TO ENSURE TEST FAILS UNTIL REAL VALUE IS ENTERED";
        string actual = new Day04().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "4,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "RANDOM VALUE TO ENSURE TEST FAILS UNTIL REAL VALUE IS ENTERED";
        string actual = new Day04().Solve(2);
        Assert.Equal(expected, actual);
    }
}

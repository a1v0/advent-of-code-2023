namespace AdventOfCode2023.Tests;

public class Day18Tests : BaseTest
{
    [Trait("dp", "18,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "62";
        string actual = new Day18().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "18,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "952408144115";
        string actual = new Day18().Solve(2);
        Assert.Equal(expected, actual);
    }
}

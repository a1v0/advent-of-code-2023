namespace AdventOfCode2023.Tests;

public class Day14Tests : BaseTest
{
    [Trait("dp", "14,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "136";
        string actual = new Day14().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "14,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "64";
        string actual = new Day14().Solve(2);
        Assert.Equal(expected, actual);
    }
}

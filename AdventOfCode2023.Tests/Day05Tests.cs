namespace AdventOfCode2023.Tests;

public class Day05Tests : BaseTest
{
    [Trait("dp", "5,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "35";
        string actual = new Day05().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "5,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "46";
        string actual = new Day05().Solve(2);
        Assert.Equal(expected, actual);
    }
}

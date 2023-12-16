namespace AdventOfCode2023.Tests;

public class Day08Tests : BaseTest
{
    [Trait("dp", "8,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "6";
        string actual = new Day08().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "8,2")]
    [Fact]
    public void Test2()
    {
        UseSecondTestInput();
        const string expected = "6";
        string actual = new Day08().Solve(2);
        Assert.Equal(expected, actual);
    }
}

namespace AdventOfCode2023.Tests;

public class Day15Tests : BaseTest
{
    [Trait("dp", "15,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "1320";
        string actual = new Day15().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "15,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "145";
        string actual = new Day15().Solve(2);
        Assert.Equal(expected, actual);
    }
}

namespace AdventOfCode2023.Tests;

public class Day03Tests : BaseTest
{
    [Trait("dp", "3,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "4361";
        string actual = new Day03().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "3,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "467835";
        string actual = new Day03().Solve(2);
        Assert.Equal(expected, actual);
    }
}

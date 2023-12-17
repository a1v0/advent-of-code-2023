namespace AdventOfCode2023.Tests;

public class Day09Tests : BaseTest
{
    [Trait("dp", "9,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "114";
        string actual = new Day09().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "9,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "2";
        string actual = new Day09().Solve(2);
        Assert.Equal(expected, actual);
    }
}

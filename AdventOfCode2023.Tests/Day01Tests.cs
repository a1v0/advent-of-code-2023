namespace AdventOfCode2023.Tests;

public class Day01Tests : BaseTest
{
    [Trait("dp", "1,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "01";
        string actual = new Day01Task1().Solve();
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "1,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "01";
        string actual = new Day01Task2().Solve();
        Assert.Equal(expected, actual);
    }
}

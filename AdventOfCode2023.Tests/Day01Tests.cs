namespace AdventOfCode2023.Tests;

public class Day01Tests : BaseTest
{
    [Trait("dp", "1,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "142";
        string actual = new Day01().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "1,2")]
    [Fact]
    public void Test2()
    {
        UseSecondTestInput();
        const string expected = "281";
        string actual = new Day01().Solve(2);
        Assert.Equal(expected, actual);
    }
}

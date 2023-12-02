namespace AdventOfCode2023.Tests;

public class Day02Tests : BaseTest
{
    [Trait("dp", "2,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "8";
        string actual = new Day02().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "2,2")]
    [Fact]
    public void Test2()
    {
        UseSecondTestInput();
        const string expected = "2345678987iwdjsblfkjsdfowiwiew8328381";
        string actual = new Day02().Solve(2);
        Assert.Equal(expected, actual);
    }
}

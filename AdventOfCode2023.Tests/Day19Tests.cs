namespace AdventOfCode2023.Tests;

public class Day19Tests : BaseTest
{
    [Trait("dp", "19,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "19114";
        string actual = new Day19().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "19,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "167409079868000";
        string actual = new Day19().Solve(2);
        Assert.Equal(expected, actual);
    }
}

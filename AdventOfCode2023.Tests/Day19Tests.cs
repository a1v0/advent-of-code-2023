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
        const string expected = "RANDOM_VALUE_TO_ENSURE_TEST_FAILS_UNTIL_REAL_VALUE_IS_ENTERED";
        string actual = new Day19().Solve(2);
        Assert.Equal(expected, actual);
    }
}

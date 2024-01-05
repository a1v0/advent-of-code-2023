namespace AdventOfCode2023.Tests;

public class Day13Tests : BaseTest
{
    [Trait("dp", "13,1")]
    [Fact]
    public void Test1()
    {
        const string expected = "RANDOM_VALUE_TO_ENSURE_TEST_FAILS_UNTIL_REAL_VALUE_IS_ENTERED";
        string actual = new Day13().Solve(1);
        Assert.Equal(expected, actual);
    }

    [Trait("dp", "13,2")]
    [Fact]
    public void Test2()
    {
        const string expected = "RANDOM_VALUE_TO_ENSURE_TEST_FAILS_UNTIL_REAL_VALUE_IS_ENTERED";
        string actual = new Day13().Solve(2);
        Assert.Equal(expected, actual);
    }
}

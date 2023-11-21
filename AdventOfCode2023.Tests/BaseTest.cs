namespace AdventOfCode2023.Tests;

public class BaseTest
{
    /*
     * Any test-suite-wide config goes in this class
     * All tests inherit from here
     */

    public BaseTest()
    {
        Environment.SetEnvironmentVariable("RUN_MODE", "TEST");
    }
}
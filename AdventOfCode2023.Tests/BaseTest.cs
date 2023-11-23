namespace AdventOfCode2023.Tests;

public abstract class BaseTest
{
    /*
     * Any test-suite-wide config goes in this class
     * All tests inherit from here
     */

    protected BaseTest()
    {
        Environment.SetEnvironmentVariable("RUN_MODE", "TEST");
    }
}

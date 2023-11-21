namespace AdventOfCode2023.Tests
{
    public class Day01Tests
    {
        [Trait("dp", "1,1")]
        [Fact]
        public void Test1()
        {
            string name = Day01.DayName;
            Assert.Equal("01", name);
        }

        [Trait("dp", "1,2")]
        [Fact]
        public void Test2()
        {
            string name = Day01.DayName;
            Assert.NotEqual("02", name);
        }
    }
}
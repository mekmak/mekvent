using Xunit;

namespace mekvent.tests.Days
{
    public class Tests : TestBase
    {
        #region Day One

        [Theory]
        [InlineData(true, 7)]
        [InlineData(false, 1301)]
        public void DayOne_PartOne(bool isTestFile, int expected)
        {
            var partOne = new mekvent.Days.One.PartOne();
            var input = ReadInput(1, isTestFile);
            var actual = partOne.CountDepthIncreases(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 5)]
        [InlineData(false, 1346)]
        public void DayOne_PartTwo(bool isTestFile, int expected)
        {
            var partOne = new mekvent.Days.One.PartTwo();
            var input = ReadInput(1, isTestFile);
            var actual = partOne.CountDepthIncreases(input);
            Assert.Equal(expected, actual);
        }

        #endregion
    
        #region Day Two

        [Theory]
        [InlineData(true, 150)]
        [InlineData(false, 2322630)]
        public void DayTwo_PartOne(bool isTestFile, int expected)
        {
            var partOne = new mekvent.Days.Two.PartOne();
            var input = ReadInput(2, isTestFile);
            var actual = partOne.GetFinalPosition(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 900)]
        [InlineData(false, 2105273490)]
        public void DayTwo_PartTwo(bool isTestFile, int expected)
        {
            var partOne = new mekvent.Days.Two.PartTwo();
            var input = ReadInput(2, isTestFile);
            var actual = partOne.GetFinalPosition(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Three

        #endregion

        #region Day Four

        #endregion

        #region Day Five

        #endregion
    }
}
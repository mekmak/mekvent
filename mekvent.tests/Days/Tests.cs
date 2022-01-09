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
            var part = new mekvent.Days.One.PartOne();
            var input = ReadInput(1, isTestFile);
            var actual = part.CountDepthIncreases(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 5)]
        [InlineData(false, 1346)]
        public void DayOne_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.One.PartTwo();
            var input = ReadInput(1, isTestFile);
            var actual = part.CountDepthIncreases(input);
            Assert.Equal(expected, actual);
        }

        #endregion
    
        #region Day Two

        [Theory]
        [InlineData(true, 150)]
        [InlineData(false, 2322630)]
        public void DayTwo_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Two.PartOne();
            var input = ReadInput(2, isTestFile);
            var actual = part.GetFinalPosition(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 900)]
        [InlineData(false, 2105273490)]
        public void DayTwo_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Two.PartTwo();
            var input = ReadInput(2, isTestFile);
            var actual = part.GetFinalPosition(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Three

        [Theory]
        [InlineData(true, 198)]
        [InlineData(false, 3885894)]
        public void DayThree_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Three.PartOne();
            var input = ReadInput(3, isTestFile);
            var actual = part.CalculateConsumption(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 230)]
        [InlineData(false, 4375225)]
        public void DayThree_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Three.PartTwo();
            var input = ReadInput(3, isTestFile);
            var actual = part.CalculateLifeSupport(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Four

        [Theory]
        [InlineData(true, 4512)]
        [InlineData(false, 35711)]
        public void DayFour_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Four.PartOne();
            var input = ReadInput(4, isTestFile);
            var actual = part.GetFinalScore(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 1924)]
        [InlineData(false, 5586)]
        public void DayFour_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Four.PartTwo();
            var input = ReadInput(4, isTestFile);
            var actual = part.GetFinalScore(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Five

        #endregion
    }
}
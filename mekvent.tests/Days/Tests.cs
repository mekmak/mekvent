using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace mekvent.tests.Days
{
    public class Tests : TestBase
    {
        public Tests(ITestOutputHelper output) : base(output)
        {
        }

        #region Day One

        [Theory]
        [InlineData(1, 7)]
        [InlineData(2, 1301)]
        public void DayOne_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.One.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.CountDepthIncreases(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 1346)]
        public void DayOne_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.One.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.CountDepthIncreases(input);
            Assert.Equal(expected, actual);
        }

        #endregion
    
        #region Day Two

        [Theory]
        [InlineData(1, 150)]
        [InlineData(2, 2322630)]
        public void DayTwo_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Two.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.GetFinalPosition(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 900)]
        [InlineData(2, 2105273490)]
        public void DayTwo_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Two.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.GetFinalPosition(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Three

        [Theory]
        [InlineData(1, 198)]
        [InlineData(2, 3885894)]
        public void DayThree_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Three.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.CalculateConsumption(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 230)]
        [InlineData(2, 4375225)]
        public void DayThree_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Three.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.CalculateLifeSupport(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Four

        [Theory]
        [InlineData(1, 4512)]
        [InlineData(2, 35711)]
        public void DayFour_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Four.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.GetFinalScore(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1924)]
        [InlineData(2, 5586)]
        public void DayFour_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Four.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.GetFinalScore(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Five

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 5197)]
        public void DayFive_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Five.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.NumOverlappingLines(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 12)]
        [InlineData(2, 18605)]
        public void DayFive_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Five.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.NumOverlappingLines(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Six

        [Theory]
        [InlineData(1,  5934)]
        [InlineData(2, 356190)]
        public void DaySix_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Six.PartOne();
            var input = ReadInput( fileNumber).Single();
            int actual = part.NumOfFish(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1,  26984457539)]
        [InlineData(2, 1617359101538)]
        public void DaySix_PartTwo(int fileNumber, decimal expected)
        {
            var part = new mekvent.Days.Six.PartTwo();
            var input = ReadInput( fileNumber).Single();
            decimal actual = part.NumOfFish(input);
            Assert.Equal(expected, actual);            
        }

        #endregion

        #region Day Seven

        [Theory]
        [InlineData(1, 37)]
        [InlineData(2, 355592)]
        public void DaySeven_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Seven.PartOne();
            var input = ReadInput( fileNumber).Single();
            var actual = part.MinFuelCost(input);
            Assert.Equal(expected, actual);    
        }

        [Theory]
        [InlineData(1, 168)]
        [InlineData(2, 101618069)]
        public void DaySeven_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Seven.PartTwo();
            var input = ReadInput( fileNumber).Single();
            var actual = part.MinFuelCost(input);
            Assert.Equal(expected, actual);    
        }

        #endregion

        #region Day Nine

        [Theory]
        [InlineData(1, 15)]
        [InlineData(2, 603)]
        public void DayNine_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Nine.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.CalculateRiskLevel(input);
            Assert.Equal(expected, actual);    
        }

        [Theory]
        [InlineData(1, 1134)]
        [InlineData(2, 786780)]
        public void DayNine_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Nine.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.CalculateBasinSize(input);
            Assert.Equal(expected, actual);    
        }

        #endregion

        #region Day Ten

        [Theory]
        [InlineData(1, 26397)]
        [InlineData(2, 392367)]
        public void DayTen_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Ten.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.CalculateSyntaxErrorScore(input);
            Assert.Equal(expected, actual); 
        }

        [Theory]
        [InlineData(1, 288957)]
        [InlineData(2, 2192104158)]
        public void DayTen_PartTwo(int fileNumber, decimal expected)
        {
            var part = new mekvent.Days.Ten.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.FindMiddleCompletionScore(input);
            Assert.Equal(expected, actual); 
        }

        #endregion

        #region Day Eleven

        [Theory]
        [InlineData(1, 1656, 100)]
        [InlineData(2, 1625, 100)]
        [InlineData(4, 9, 2)]
        public void DayEleven_PartOne(int fileNumber, int expected, int numFlashes)
        {
            var part = new mekvent.Days.Eleven.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.CalculateTotalFlashes(input, numFlashes);
            Assert.Equal(expected, actual); 
        }

        [Theory]
        [InlineData(1, 195)]
        [InlineData(2, 244)]
        [InlineData(3, 2)]
        public void DayEleven_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Eleven.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.FirstSimultaneousFlashStep(input);
            Assert.Equal(expected, actual); 
        }

        #endregion

        #region Day Twelve

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 19)]
        [InlineData(3, 226)]
        [InlineData(4, 5252)]
        public void DayTwelve_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Twelve.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.GetNumRoutes(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 36)]
        [InlineData(2, 103)]
        [InlineData(3, 3509)]
        [InlineData(4, 147784)]
        public void DayTwelve_PartTwo(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Twelve.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.GetNumRoutes(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Thirteen

        [Theory]
        [InlineData(1, 17)]
        [InlineData(2, 638)]
        public void DayThirteen_PartOne(int fileNumber, int expected)
        {
            var part = new mekvent.Days.Thirteen.PartOne();
            var input = ReadInput(fileNumber);
            var actual = part.GetVisibleDots(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, "cjckbapb")]
        public void DayThirteen_PartTwo(int fileNumber, string expected)
        {
            var part = new mekvent.Days.Thirteen.PartTwo();
            var input = ReadInput(fileNumber);
            var actual = part.GetCode(input);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
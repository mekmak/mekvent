using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData(true, 5)]
        [InlineData(false, 5197)]
        public void DayFive_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Five.PartOne();
            var input = ReadInput(5, isTestFile);
            var actual = part.NumOverlappingLines(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 12)]
        [InlineData(false, 18605)]
        public void DayFive_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Five.PartTwo();
            var input = ReadInput(5, isTestFile);
            var actual = part.NumOverlappingLines(input);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Day Six

        [Theory]
        [InlineData(true,  5934)]
        [InlineData(false, 356190)]
        public void DaySix_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Six.PartOne();
            var input = ReadInput(6, isTestFile).Single();
            int actual = part.NumOfFish(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true,  26984457539)]
        [InlineData(false, 1617359101538)]
        public void DaySix_PartTwo(bool isTestFile, decimal expected)
        {
            var part = new mekvent.Days.Six.PartTwo();
            var input = ReadInput(6, isTestFile).Single();
            decimal actual = part.NumOfFish(input);
            Assert.Equal(expected, actual);            
        }

        #endregion

        #region Day Seven

        [Theory]
        [InlineData(true, 37)]
        [InlineData(false, 355592)]
        public void DaySeven_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Seven.PartOne();
            var input = ReadInput(7, isTestFile).Single();
            var actual = part.MinFuelCost(input);
            Assert.Equal(expected, actual);    
        }

        [Theory]
        [InlineData(true, 168)]
        [InlineData(false, 101618069)]
        public void DaySeven_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Seven.PartTwo();
            var input = ReadInput(7, isTestFile).Single();
            var actual = part.MinFuelCost(input);
            Assert.Equal(expected, actual);    
        }

        #endregion

        #region Day Nine

        [Theory]
        [InlineData(true, 15)]
        [InlineData(false, 603)]
        public void DayNine_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Nine.PartOne();
            var input = ReadInput(9, isTestFile);
            var actual = part.CalculateRiskLevel(input);
            Assert.Equal(expected, actual);    
        }

        [Theory]
        [InlineData(true, 1134)]
        [InlineData(false, 786780)]
        public void DayNine_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Nine.PartTwo();
            var input = ReadInput(9, isTestFile);
            var actual = part.CalculateBasinSize(input);
            Assert.Equal(expected, actual);    
        }

        #endregion

        #region Day Ten

        [Theory]
        [InlineData(true, 26397)]
        [InlineData(false, 392367)]
        public void DayTen_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Ten.PartOne();
            var input = ReadInput(10, isTestFile);
            var actual = part.CalculateSyntaxErrorScore(input);
            Assert.Equal(expected, actual); 
        }

        [Theory]
        [InlineData(true, 288957)]
        [InlineData(false, 2192104158)]
        public void DayTen_PartTwo(bool isTestFile, decimal expected)
        {
            var part = new mekvent.Days.Ten.PartTwo();
            var input = ReadInput(10, isTestFile);
            var actual = part.FindMiddleCompletionScore(input);
            Assert.Equal(expected, actual); 
        }

        #endregion

        #region Day Eleven

        [Fact]
        public void DayEleven_PartOne_Mini()
        {
            var inputs = new List<string>
            {
                "11111",
                "19991",
                "19191",
                "19991",
                "11111"
            };

            var part = new mekvent.Days.Eleven.PartOne();
            var actual = part.CalculateTotalFlashes(inputs, 2);
            Assert.Equal(9, actual);
        }

        [Theory]
        [InlineData(true, 1656)]
        [InlineData(false, 1625)]
        public void DayEleven_PartOne(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Eleven.PartOne();
            var input = ReadInput(11, isTestFile);
            var actual = part.CalculateTotalFlashes(input, 100);
            Assert.Equal(expected, actual); 
        }

        [Fact]
        public void DayEleven_PartTwo_Mini()
        {
            var inputs = new List<string>
            {
                "5877777777",
                "8877777777",
                "7777777777",
                "7777777777",
                "7777777777",
                "7777777777",
                "7777777777",
                "7777777777",
                "7777777777",
                "7777777777"
            };

            var part = new mekvent.Days.Eleven.PartTwo();
            var actual = part.FirstSimultaneousFlashStep(inputs);
            Assert.Equal(2, actual);
        }

        [Theory]
        [InlineData(true, 195)]
        [InlineData(false, 244)]
        public void DayEleven_PartTwo(bool isTestFile, int expected)
        {
            var part = new mekvent.Days.Eleven.PartTwo();
            var input = ReadInput(11, isTestFile);
            var actual = part.FirstSimultaneousFlashStep(input);
            Assert.Equal(expected, actual); 
        }

        #endregion
    }
}
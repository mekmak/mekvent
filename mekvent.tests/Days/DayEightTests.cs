using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using mekvent.Days.Eight;
using Xunit.Abstractions;

namespace mekvent.tests.Days
{
    public class DayEightTests : TestBase
    {
        public DayEightTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("a,b,c,e,f,g", 0)]
        [InlineData("b,a,c,g,f,e", 0)]
        [InlineData("c,f", 1)]
        [InlineData("a,c,d,e,g", 2)]
        [InlineData("a,c,d,f,g", 3)]
        [InlineData("b,c,d,f", 4)]
        [InlineData("a,b,d,f,g", 5)]
        [InlineData("a,b,d,e,f,g", 6)]
        [InlineData("a,c,f", 7)]
        [InlineData("a,b,c,d,e,f,g", 8)]
        [InlineData("a,b,c,d,f,g", 9)]
        public void DayEight_Decoder(string input, int expected)
        {
            uint[] segments = ParseSegmentInput(input).ToArray();
            int actual = SevenSegmentDecoder.Decode(segments);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("a,b")]
        public void DayEight_Decoder_Invalid(string input)
        {
            uint[] segments = ParseSegmentInput(input).ToArray();
            Assert.Throws<Exception>(() => SevenSegmentDecoder.Decode(segments));
        }

        [Theory]
        [InlineData("0,1", "0,1|1,0")]
        [InlineData("0,1,2", "0,1,2|0,2,1|1,0,2|1,2,0|2,1,0|2,0,1")]
        [InlineData("0,1,2,3", "3,0,1,2|3,0,2,1|3,1,0,2|3,1,2,0|3,2,1,0|3,2,0,1|0,3,1,2|0,3,2,1|1,3,0,2|1,3,2,0|2,3,1,0|2,3,0,1|0,1,3,2|0,2,3,1|1,0,3,2|1,2,3,0|2,1,3,0|2,0,3,1|0,1,2,3|0,2,1,3|1,0,2,3|1,2,0,3|2,1,0,3|2,0,1,3")]
        public void DayEight_Permutator(string input, string permsInput)
        {
            List<uint> inputs = input.Split(",").Select(uint.Parse).ToList();
            List<List<uint>> expectedPerms = permsInput
                                    .Split("|")
                                    .Select(p => p.Split(",").Select(uint.Parse).ToList())
                                    .ToList();
            int expectedPermLength = expectedPerms.First().Count;

            List<List<uint>> actualPerms = Permutator.Permutate(inputs);

            Assert.Equal(expectedPerms.Count, actualPerms.Count);
            foreach(var expectedPerm in expectedPerms)
            {
                Assert.True(actualPerms.Any(p => p.SequenceEqual(expectedPerm)), $"Could not find perm {string.Join(", ", expectedPerm)}");
            }
        }

        [Theory]
        [InlineData("d,e,a,f,g,b,c", "d", "a")]
        [InlineData("d,e,a,f,g,b,c", "e", "b")]
        [InlineData("d,e,a,f,g,b,c", "a", "c")]
        [InlineData("d,e,a,f,g,b,c", "f", "d")]
        [InlineData("d,e,a,f,g,b,c", "g", "e")]
        [InlineData("d,e,a,f,g,b,c", "b", "f")]
        [InlineData("d,e,a,f,g,b,c", "c", "g")]
        public void DayEight_Csp(string mapInput, string crossed, string expected)
        {
            var map = new CrossedSegmentMap(ParseSegmentInput(mapInput).ToArray());
            var uncrossed = map.UncrossSegment(Segment.Parse(crossed));
            Assert.Equal(Segment.Parse(expected), uncrossed);
        }

        [Theory]
        [InlineData("d,e,a,f,g,b,c", "a,b", "c,f", true)]
        [InlineData("d,e,a,f,g,b,c", "g,c,d,f,a", "a,c,d,e,g", true)]
        [InlineData("d,e,a,f,g,b,c", "f,b,c,a,d", "a,c,d,f,g", true)]
        [InlineData("d,e,a,f,g,b,c", "d,a,b", "a,c,f", true)]
        [InlineData("d,e,a,f,g,b,c", "c,e,f,a,b,d", "a,b,c,d,f,g", true)]
        [InlineData("d,e,a,f,g,b,c", "c,d,f,g,e,b", "a,b,d,e,f,g", true)]
        [InlineData("d,e,a,f,g,b,c", "e,a,f,b", "b,c,d,f", true)]
        [InlineData("d,e,a,f,g,b,c", "c,a,g,e,d,b", "a,b,c,e,f,g", true)]
        public void DayEight_CspMatches(string mapInput, string crossedInput, string uncrossedInput, bool expected)
        {
            var map = new CrossedSegmentMap(ParseSegmentInput(mapInput).ToArray());
            var crossed = ParseSegmentInput(crossedInput);
            var uncrossed = ParseSegmentInput(uncrossedInput);

            var actual = CrossedSegmentDecoder.CspMatches(crossed, uncrossed, map);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf", "5,3,5,3")]
        [InlineData("fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb", "8,4,1,8")]
        [InlineData("egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb", "8,7,1,7")]
        public void DayEight_CrossedDecode(string input, string outputDigitsInput)
        {
            var parsed = InputParser.Parse(input);
            List<int> expectedDigits = outputDigitsInput.Split(",").Select(int.Parse).ToList();
            List<int> actual = CrossedSegmentDecoder.Decode(parsed.UniquePatterns, parsed.OutputPatterns);
            Assert.True(expectedDigits.SequenceEqual(actual), $"Expected {string.Join(", ", expectedDigits)} but got {string.Join(", ", actual)}");
        }

        [Theory]
        [InlineData(1, 26)]
        [InlineData(2, 519)]
        public void DayEight_PartOne(int fileNumber, int expected)
        {
            var part = new PartOne();
            var actual = part.CountDigits(ReadInput(8, fileNumber));
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 61229)]
        [InlineData(2, 1027483)]
        public void DayEight_PartTwo(int fileNumber, int expected)
        {
            var part = new PartTwo();
            var actual = part.OutputSum(ReadInput(8, fileNumber));
            Assert.Equal(expected, actual);
        }

        private List<uint> ParseSegmentInput(string input)
        {
            return input.Split(",").Select(Segment.Parse).ToList();
        }
    }
}
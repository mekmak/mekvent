using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.Eight
{
    public class Permutator
    {
        public static List<List<uint>> Permutate(params uint[] inputs)
        {
            return Permutate(inputs.ToList());
        }

        public static List<List<uint>> Permutate(List<uint> inputs)
        {
            if(inputs.Count == 1)
            {
                return new List<List<uint>> 
                {
                    new List<uint> { inputs.Single() }
                };
            }

            var permutations = new List<List<uint>>();
            foreach(uint input in inputs)
            {
                var inner = Permutate(inputs.Where(i => i != input).ToList());
                inner.ForEach(i => i.Add(input));
                permutations.AddRange(inner);
            }
            return permutations;
        }
    }

    public static class Segment
    {
        public const uint A = 0;
        public const uint B = 1;
        public const uint C = 2;
        public const uint D = 3;
        public const uint E = 4;
        public const uint F = 5;
        public const uint G = 6;

        public static uint Parse(char segment)
        {
            return Parse(segment.ToString());
        }

        public static uint Parse(string segment)
        {
            switch(segment.ToLower())
            {
                case "a":
                    return A;
                case "b":
                    return B;
                case "c":
                    return C;
                case "d":
                    return D;
                case "e":
                    return E;
                case "f":
                    return F;
                case "g":
                    return G;
                default:
                    throw new Exception($"Could not parse segment {segment}");
            }
        }
    }

    public class SevenSegmentDecoder
    {
        private static uint[] _numToPattern =
        {
            0b_0_1110111, // 0: abc_efg
            0b_0_0010010, // 1: __c__f_
            0b_0_1011101, // 2: a_cde_g
            0b_0_1011011, // 3: a_cd_fg
            0b_0_0111010, // 4: _bcd_f_
            0b_0_1101011, // 5: ab_d_fg
            0b_0_1101111, // 6: ab_defg
            0b_0_1010010, // 7: a_c__f_
            0b_0_1111111, // 8: abcdefg
            0b_0_1111011, // 9: abcd_fg
        };

        private static uint[] _segmentToMask = 
        {
            0b_0_1000000, // a
            0b_0_0100000, // b
            0b_0_0010000, // c
            0b_0_0001000, // d
            0b_0_0000100, // e
            0b_0_0000010, // f
            0b_0_0000001  // g
        };

        public static int Decode(params uint[] segments)
        {
            uint pattern = 0b_0_0000000;
            foreach(int segment in segments)
            {
                if(segment < Segment.A || segment > Segment.G)
                {
                    throw new Exception($"Invalid segment {segment}");
                }
                
                uint mask = _segmentToMask[segment];
                pattern = pattern | mask;
            }

            for(int num = 0; num < 10; num++)
            {
                if(_numToPattern[num] == pattern)
                {
                    return num;
                }
            }

            throw new Exception($"Could not decode segments {string.Join(", ", segments)}");
        }
    }

    public class CrossedSegmentMap
    {
        private uint[] _segments;
        public CrossedSegmentMap(uint[] segments)
        {
            _segments = new uint[segments.Length];
            for(uint i = 0; i < segments.Length; i++)
            {
                _segments[segments[i]] = i;
            }
        }

        public uint UncrossSegment(uint crossedSegment)
        {
            if(crossedSegment >= _segments.Length)
            {
                throw new Exception($"Invalid segment {crossedSegment}");
            }

            return _segments[crossedSegment];
        }
    }

    public class CrossedSegmentDecoder
    {
        private static List<CrossedSegmentMap> GenerateAllPossibleMaps()
        {
            List<List<uint>> perms = Permutator.Permutate(
                    Segment.A,
                    Segment.B,
                    Segment.C,
                    Segment.D,
                    Segment.E,
                    Segment.F,
                    Segment.G);

            return perms.Select(p => new CrossedSegmentMap(p.ToArray())).ToList();
        }

        private static Dictionary<int, List<List<uint>>> _patternLengthToUncrossed = new Dictionary<int, List<List<uint>>>
        {
            [2] = new List<List<uint>> { new List<uint> { Segment.C, Segment.F } }, // 1
            [3] = new List<List<uint>> { new List<uint> { Segment.A, Segment.C, Segment.F } }, // 7
            [4] = new List<List<uint>> { new List<uint> { Segment.B, Segment.C, Segment.D, Segment.F } }, // 4
            [5] = new List<List<uint>> 
            { 
                new List<uint> { Segment.A, Segment.C, Segment.D, Segment.E, Segment.G }, // 2
                new List<uint> { Segment.A, Segment.C, Segment.D, Segment.F, Segment.G }, // 3
                new List<uint> { Segment.A, Segment.B, Segment.D, Segment.F, Segment.G }  // 5
            },
            [6] = new List<List<uint>> 
            { 
                new List<uint> { Segment.A, Segment.B, Segment.C, Segment.E, Segment.F, Segment.G }, // 0
                new List<uint> { Segment.A, Segment.B, Segment.D, Segment.E, Segment.F, Segment.G }, // 6
                new List<uint> { Segment.A, Segment.B, Segment.C, Segment.D, Segment.F, Segment.G }  // 9
            },
            [7] = new List<List<uint>> { new List<uint> { Segment.A, Segment.B, Segment.C, Segment.D, Segment.E, Segment.F, Segment.G } }, // 8
        };

        public static bool CspMatches(List<uint> crossed, List<uint> uncrossed, CrossedSegmentMap map)
        {
            List<List<uint>> crossedPerms = Permutator.Permutate(crossed);
            foreach (List<uint> crossedPerm in crossedPerms)
            {
                bool matches = true;
                for (int i = 0; i < crossedPerm.Count; i++)
                {
                    uint segment = crossedPerm[i];
                    if(map.UncrossSegment(segment) != uncrossed[i])
                    {
                        matches = false;
                        break;
                    }
                }

                if(matches)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<int> Decode(List<List<uint>> uniquePatterns, List<List<uint>> outputPatterns)
        {
            List<CrossedSegmentMap> possibleMaps = GenerateAllPossibleMaps();           

            foreach(List<uint> pattern in uniquePatterns.OrderBy(p => p.Count))
            {
                if(!_patternLengthToUncrossed.TryGetValue(pattern.Count, out List<List<uint>> possibleDigits))
                {
                    throw new Exception($"No number has {pattern.Count} segments lit: {string.Format(", ", pattern)}");
                }

                var newPossible = new List<CrossedSegmentMap>();
                foreach(List<uint> possibleDigit in possibleDigits)
                {
                    newPossible.AddRange(possibleMaps.Where(csp => CspMatches(pattern, possibleDigit, csp)).ToList());
                }
                possibleMaps = newPossible;
            }

            if(possibleMaps.Count != 1)
            {
                throw new Exception($"Should have ended up with one map but had {possibleMaps.Count}");
            }
            
            CrossedSegmentMap correctMap = possibleMaps.Single();
            
            var decodedOutputs = new List<int>();
            foreach(List<uint> outputPattern in outputPatterns)
            {
                uint[] uncrossed = outputPattern.Select(correctMap.UncrossSegment).ToArray();
                int decoded = SevenSegmentDecoder.Decode(uncrossed);
                decodedOutputs.Add(decoded);
            }
            return decodedOutputs;
        } 
    }

    public class InputParser
    {
        public class Input
        {
            public List<List<uint>> UniquePatterns {get;set;}
            public List<List<uint>> OutputPatterns {get;set;}
        }

        public static Input Parse(string input)
        {
            string[] split = input.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
            if(split.Length != 2)
            {
                throw new Exception($"Invalid input {input}");
            }

            List<List<uint>> ParseSegments(string segments) => segments
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => p.Select(Segment.Parse).ToList())
                        .ToList();

            List<List<uint>> uniquePatterns = ParseSegments(split[0]);
            List<List<uint>> outputPatterns = ParseSegments(split[1]);

            return new Input
            {
                OutputPatterns = outputPatterns,
                UniquePatterns = uniquePatterns
            };
        }
    }

    public class PartOne
    {
        public int CountDigits(List<string> inputs)
        {
            int digitCount = 0;
            foreach(var input in inputs.Select(InputParser.Parse))
            {
                foreach(List<uint> outputPattern in input.OutputPatterns)
                {
                    switch(outputPattern.Count)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            digitCount++;
                            break;
                    }
                }
            }
            return digitCount;
        }
    }

    public class PartTwo
    {
        private int ToNum(List<int> digits)
        {
            double num = 0;
            int pow = digits.Count - 1;
            foreach(var digit in digits)
            {
                num += digit * Math.Pow(10, pow);
                pow--;
            }
            return (int)num;
        }

        public int OutputSum(List<string> inputs)
        {
            int sum = 0;
            foreach(var input in inputs.Select(InputParser.Parse))
            {
                List<int> outputDigits = CrossedSegmentDecoder.Decode(input.UniquePatterns, input.OutputPatterns);
                sum += ToNum(outputDigits);
            }
            return sum;
        }
    }
}
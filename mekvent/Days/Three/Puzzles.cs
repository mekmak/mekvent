using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.Three
{
    public class PartOne
    {
        public int CalculateConsumption(List<string> lines)
        {
            if(lines == null || !lines.Any())
            {
                return 0;
            }

            int numberLength = lines.First().Length;
            int power = numberLength - 1;

            int gamma = 0;
            int epsilon = 0;

            for(int index = 0; index < numberLength; index++)
            {
                int zeroCount = 0;
                int oneCount = 0;
                
                foreach(string line in lines)
                {
                    switch(line[index])
                    {
                        case '0':
                            zeroCount++;
                            break;
                        case '1':
                            oneCount++;
                            break;
                        default:
                            throw new System.Exception($"Binary number '{line}' can only have ones and zeros");
                    }
                }

                int gammaBit = zeroCount > oneCount ? 0 : 1;
                int epsilonBit = zeroCount > oneCount ? 1 : 0;

                gamma += gammaBit * (1<<power);
                epsilon += epsilonBit * (1<<power);

                power--;
            }

            return gamma * epsilon;
        }
    }

    public class PartTwo
    {
        public int CalculateLifeSupport(List<string> lines)
        {
            string GetRatingName(bool isOxygen) => isOxygen ? "oxygen" : "co2";

            List<string> Filter(List<string> inputs, bool isOxygen, int index)
            {
                int zeroCount = 0;
                int oneCount = 0;
                
                foreach(string input in inputs)
                {
                    switch(input[index])
                    {
                        case '0':
                            zeroCount++;
                            break;
                        case '1':
                            oneCount++;
                            break;
                        default:
                            throw new System.Exception($"Binary number '{input}' can only have ones and zeros");
                    }
                }

                char digitToKeep;
                if(isOxygen)
                {
                    digitToKeep = zeroCount > oneCount ? '0' : '1';
                }
                else
                {
                    digitToKeep = zeroCount > oneCount ? '1' : '0';
                }
                
                var filtered = inputs.Where(l => l[index] == digitToKeep).ToList();
                return filtered;
            }

            string FindRating(List<string> inputs, bool isOxygen)
            {
                int inputLength = inputs.First().Length;
                int index = 0;
                while(inputs.Count > 1)
                {
                    if(index >= inputLength)
                    {
                        throw new Exception($"Reached the end of the {GetRatingName(isOxygen)} inputs but we still have {inputs.Count} left");
                    }

                    inputs = Filter(inputs, isOxygen, index);                    
                    index++;
                }

                if(!inputs.Any())
                {
                    throw new Exception($"Left with no results for {GetRatingName(isOxygen)}!");
                }

                return inputs.Single();
            }

            int BinToDec(string binary)
            {
                int dec = 0;
                int pow = binary.Length - 1;
                foreach(char bit in binary)
                {
                    switch(bit)
                    {
                        case '0':
                            break;
                        case '1':
                            dec += (1<<pow);
                            break;
                        default:
                            throw new Exception($"Binary number '{binary}' can only have ones and zeroes");
                    }

                    pow--;
                }

                return dec;
            }

            string oxygenBinary = FindRating(lines, true);
            string c02Binary = FindRating(lines, false);

            return BinToDec(oxygenBinary) * BinToDec(c02Binary);
        }
    }
}
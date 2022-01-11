using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.Six
{
    public class PartOne
    {
        public int NumOfFish(string input)
        {
            List<int> fish = input.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList();

            int numOfDays = 80;
            while(numOfDays > 0)
            {
                int numOfFish = fish.Count;
                for(int i = 0; i < numOfFish; i++)
                {
                    fish[i] -= 1;
                    if(fish[i] >= 0)
                    {
                        continue;
                    }

                    fish.Add(8);
                    fish[i] = 6;
                }

                numOfDays--;
            }

            return fish.Count;
        }
    }

    public class PartTwo
    {
        public decimal NumOfFish(string input)
        {
            List<int> fish = input.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList();

            decimal[] fishCounts = new decimal[9];
            foreach(var f in fish)
            {
                if(f < 0 || f > 8)
                {
                    throw new Exception($"Fish age must be 0-8: {f}");
                }

                fishCounts[f] += 1;
            }

            int numOfDays = 256;
            while(numOfDays > 0)
            {
                decimal numOfZero = fishCounts[0];

                for(int i = 1; i < fishCounts.Length; i++)
                {
                    fishCounts[i-1] = fishCounts[i];
                }

                fishCounts[8] = numOfZero;
                fishCounts[6] += numOfZero;

                numOfDays--;
            }            
            
            return fishCounts.Sum();
        }
    }
}
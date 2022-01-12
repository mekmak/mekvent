using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.Seven
{
    public class PartOne
    {
        public int MinFuelCost(string input)
        {
            List<int> positions = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            int minPosition = positions.Min();
            int maxPosition = positions.Max();

            int GetFuelCost(int targetPosition, List<int> positions)
            {
                return positions.Sum(p => Math.Abs(targetPosition - p));
            }

            int minCost = int.MaxValue;
            for(int position = minPosition; position <= maxPosition; position++)
            {
                var cost = GetFuelCost(position, positions);
                if(cost < minCost)
                {
                    minCost = cost;
                }
            }

            return minCost;
        }
    }

    public class PartTwo
    {
        public int MinFuelCost(string input)
        {
            List<int> positions = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            int minPosition = positions.Min();
            int maxPosition = positions.Max();

            int GetFuelCost(int targetPosition, List<int> positions)
            {
                return positions.Sum(p => 
                {
                    var n = Math.Abs(targetPosition - p);
                    return (n * (n+1)) / 2;
                });
            }

            int minCost = int.MaxValue;
            for(int position = minPosition; position <= maxPosition; position++)
            {
                var cost = GetFuelCost(position, positions);
                if(cost < minCost)
                {
                    minCost = cost;
                }
            }

            return minCost;
        }
    }
}
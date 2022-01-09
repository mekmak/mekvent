using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.One
{
    public class PartOne
    {
        public int CountDepthIncreases(List<string> depths)
        {            
            int depthIncreases = 0;
            int lastDepth = int.MinValue;
            foreach(string d in depths)
            {
                if(!int.TryParse(d, out int currentDepth))
                {
                    throw new Exception($"Could not parse depth '{d}");
                }

                if(lastDepth == int.MinValue)
                {
                    lastDepth = currentDepth;
                    continue;
                }

                if(currentDepth > lastDepth)
                {
                    depthIncreases++;
                }

                lastDepth = currentDepth;
            }

            return depthIncreases;
        }
    }

    public class PartTwo
    {
        public int CountDepthIncreases(List<string> depths)
        {            
            int depthIncreases = 0;
            const int windowSize = 3;

            int[] currentWindow = new int[windowSize];
            Array.Fill(currentWindow, int.MinValue);
            int currentIndex = 0;

            foreach(string d in depths)
            {
                if(!int.TryParse(d, out int currentDepth))
                {
                    throw new Exception($"Could not parse depth '{d}");
                }

                if(currentWindow[currentIndex] == int.MinValue)
                {
                    currentWindow[currentIndex] = currentDepth;
                }
                else
                {
                    int lastWindowDepth = currentWindow.Sum();
                    currentWindow[currentIndex] = currentDepth;
                    int currentWindowDepth = currentWindow.Sum();

                    if(currentWindowDepth > lastWindowDepth)
                    {
                        depthIncreases++;
                    }
                }                

                currentIndex = currentIndex == currentWindow.Length - 1 ? 0 : currentIndex + 1;
            }

            return depthIncreases;
        }
    }
}
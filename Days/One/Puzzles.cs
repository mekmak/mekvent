using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mekvent.Days.One
{
    public class First : Puzzle
    {
        public override int Day => 1;
        public override int Part => 1;
        public override string Name => "Depth Increases";

        public string CountDepthIncreases(List<string> depths)
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

            return depthIncreases.ToString();
        }

        public override List<TestResult> Test()
        {
            return new List<TestResult>
            {
                RunTest("Test", () => ("7", CountDepthIncreases(ReadInput(true)))),
                RunTest("Official", () => ("1301", CountDepthIncreases(ReadInput(false))))
            };
        }
    }

    public class Second : Puzzle
    {
        public override int Day => 1;
        public override int Part => 2;
        public override string Name => "Depth Increases";

        public string CountDepthIncreases(List<string> depths)
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

            return depthIncreases.ToString();
        }

        public override List<TestResult> Test()
        {
            return new List<TestResult>
            {
                RunTest("Test", () => ("5", CountDepthIncreases(ReadInput(true)))),
                RunTest("Official", () => ("1346", CountDepthIncreases(ReadInput(false))))
            };
        }
    }
}
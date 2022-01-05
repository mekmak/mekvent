using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mekvent.Days.One
{
    public class First : Day, IPuzzle
    {
        public int Day => 1;
        public int Part => 1;

        /*
            Given an input file of 'depth readings', return the number of times the
            next 'depth' larger that the last 'depth'
        */
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

        public List<TestResult> Test()
        {
            return new List<TestResult>
            {
                RunTest("Depth Increases - Test", () => ("7", CountDepthIncreases(File.ReadAllLines(@".\Days\One\test_input.txt").ToList()))),
                RunTest("Depth Increases - Official", () => ("1301", CountDepthIncreases(File.ReadAllLines(@".\Days\One\input.txt").ToList())))
            };
        }
    }

    public class Second : Day, IPuzzle
    {
        public int Day => 1;
        public int Part => 2;

        /*
            Each input file line is a 'depth' that applies to one, two or three 'windows':
                199  A      
                200  A B    
                208  A B C  
                210    B C D
                200  E   C D
                207  E F   D
                240  E F G  
                269    F G H
                260      G H
                263        H
            
            A depth measurement for a given 'window' is the sum of three partial measurements. Note that the input
            file does not contain the 'windows' - just the depths. We are to assume that there are always three per window.

            Count the number of times a 'window' has a larger sum than the previous 'window'
            
        */
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

        public List<TestResult> Test()
        {
            return new List<TestResult>
            {
                RunTest("Depth Increases - Test", () => ("5", CountDepthIncreases(File.ReadAllLines(@".\Days\One\test_input.txt").ToList()))),
                RunTest("Depth Increases - Official", () => ("1346", CountDepthIncreases(File.ReadAllLines(@".\Days\One\input.txt").ToList())))
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mekvent.Days
{
    public abstract class Puzzle : IPuzzle
    {
        private static string[] _numToWord = new string[]
        {
            "Zero",
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten"
        };

        public abstract int Day {get;}
        public abstract int Part {get;}
        public abstract string Name {get;}
        public abstract List<TestResult> Test();

        private string GetDayAsWord(int day)
        {
            if(day < 0)
            {
                throw new Exception("Day must be zero or more");
            }

            if(day >= _numToWord.Length)
            {
                throw new Exception($"Could not translate day number {day} to the word");
            }

            return _numToWord[day];
        }

        private string ResolveFileName(bool isTestFile)
        {
            string day = GetDayAsWord(Day);
            string fileName = isTestFile ? "test_input" : "input";
            return $".\\mekvent\\Days\\{day}\\{fileName}.txt";
        }

        protected List<string> ReadInput(bool useTestFile)
        {
            return ReadInput(useTestFile, s => s);
        }

        protected List<T> ReadInput<T>(bool useTestFile, Func<string, T> parser)
        {
            string filePath = ResolveFileName(useTestFile);
            return ReadInput(filePath, parser);
        }

        protected List<T> ReadInput<T>(string filePath, Func<string, T> parser)
        {
            var lines = File.ReadAllLines(filePath);
            return lines.Select(parser).ToList();
        }

        protected TestResult RunTest(string testName, Func<(string, string)> test)
        {
            try
            {
                (string expected, string actual) = test();
                
                bool passed = Equals(expected, actual);
                return passed ? TestResult.Pass(testName) : TestResult.Fail(testName, expected, actual, "Incorrect value");
            }
            catch(Exception e)
            {
                return TestResult.Fail(testName, "no error", "error", e);
            }
        }
    }
}
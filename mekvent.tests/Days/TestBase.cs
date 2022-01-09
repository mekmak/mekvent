using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mekvent.tests.Days
{
    public abstract class TestBase 
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

        private string ResolveFileName(int day, bool isTestFile)
        {
            string dayWord = GetDayAsWord(day);
            string fileName = isTestFile ? $"{dayWord.ToLower()}_test" : $"{dayWord.ToLower()}";
            return $".\\Days\\inputs\\{fileName}.txt";
        }

        protected List<string> ReadInput(int day, bool useTestFile)
        {
            return ReadInput(day, useTestFile, s => s);
        }

        protected List<T> ReadInput<T>(int day, bool useTestFile, Func<string, T> parser)
        {
            string filePath = ResolveFileName(day, useTestFile);
            return ReadInput(filePath, parser);
        }

        protected List<T> ReadInput<T>(string filePath, Func<string, T> parser)
        {
            var lines = File.ReadAllLines(filePath);
            return lines.Select(parser).ToList();
        }
    }
}
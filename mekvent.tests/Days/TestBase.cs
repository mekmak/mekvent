using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit.Abstractions;

namespace mekvent.tests.Days
{
    public abstract class TestBase : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly TextWriter _originalOut;
        private readonly TextWriter _textWriter;

        public TestBase(ITestOutputHelper output)
        {
            // All this sillyness just to get xUnit 
            // tests to print Console.WriteLine to Console...
            
            _output = output;
            _originalOut = Console.Out;
            _textWriter = new StringWriter();
            Console.SetOut(_textWriter);
        }

        public void Dispose()
        {
            _output.WriteLine(_textWriter.ToString());
            Console.SetOut(_originalOut);
        }

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
            "Ten",
            "Eleven",
            "Twelve",
            "Thirteen",
            "Fourteen",
            "Fifteen",
            "Sixteen",
            "Seventeen",
            "Eighteen",
            "Nineteen",
            "Twenty"
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

        private string ResolveFileName(int day, int fileNumber)
        {
            string dayWord = GetDayAsWord(day);
            string fileName = $"{dayWord.ToLower()}_{fileNumber}.txt";
            return Path.Combine("Days", "inputs", fileName);
        }
        
        protected List<string> ReadInput(int day, int fileNumber)
        {
            string filePath = ResolveFileName(day, fileNumber);
            var lines = File.ReadAllLines(filePath);
            return lines.ToList();
        }
    }
}
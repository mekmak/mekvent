using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
        
        private static Regex _methodRegex = new Regex(@"^Day(?<day>\w+)_Part(One|Two)$");
        protected List<string> ReadInput(int fileNumber, [CallerMemberName] string memberName = "")
        {
            // Pro tip: don't do this in real life
            string ExtractDay(string methodName)
            {
                Match m = _methodRegex.Match(methodName);
                if(!m.Success)
                {
                    throw new Exception($"Could not extract day out of method name '{methodName}'");
                }
                
                return m.Groups[_methodRegex.GroupNumberFromName("day")].Value;
            }

            string day = ExtractDay(memberName);
            string fileName = $"{day.ToLower()}_{fileNumber}.txt";
            string filePath = Path.Combine("Days", "inputs", fileName);
            var lines = File.ReadAllLines(filePath);
            return lines.ToList();
        }
    }
}
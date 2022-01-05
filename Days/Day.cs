using System;

namespace mekvent.Days
{
    public abstract class Day
    {
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
                return TestResult.Fail(testName, "no error", "error", $"Error: {e.Message}");
            }
        }
    }
}
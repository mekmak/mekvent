namespace mekvent
{
    public class TestResult
    {
        public TestResult(string name, string expected, string actual, string errorMessage)
        {
            TestName = name;
            Expected = expected;
            Actual = actual;
            ErrorMessage = errorMessage;
        }

        public string TestName {get;set;}
        public string Expected {get;set;}
        public string Actual {get;set;}
        public bool Passed => ErrorMessage == null;
        public string ErrorMessage {get;set;}

        public static TestResult Pass(string testName) => new TestResult(testName, null, null, null);
        public static TestResult Fail(string testName, string expected, string actual, string errorMessage) => new TestResult(testName, expected, actual, errorMessage);
    }
}
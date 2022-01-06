using System.Collections.Generic;

namespace mekvent.Days
{
    public interface IPuzzle
    {
        int Day {get;}
        int Part {get;}
        string Name {get;}
        List<TestResult> Test();
    }
}
using System.Collections.Generic;

namespace mekvent.Days
{
    public interface IPuzzle
    {
        int Day {get;}
        int Part {get;}
        List<TestResult> Test();
    }
}
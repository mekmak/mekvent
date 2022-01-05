using System.Collections.Generic;

namespace mekvent.Days
{
    public interface IPuzzle
    {
        int Day {get;}
        int Puzzle {get;}
        List<TestResult> Test();
    }
}
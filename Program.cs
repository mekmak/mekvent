using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using mekvent.Days;

namespace mekvent
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var puzzle in GetPuzzles().OrderBy(p => p.Day).ThenBy(p => p.Part))
            {
                Console.WriteLine($"--- Day {puzzle.Day}, Part {puzzle.Part}: '{puzzle.Name}'");
                foreach(var tr in puzzle.Test())
                {
                    if(tr.Passed)
                    {
                        Console.WriteLine($"\tTest '{tr.TestName}' passed");
                    }
                    else
                    {
                        Console.WriteLine($"\tTest '{tr.TestName}' failed: {tr.ErrorMessage}");
                        Console.WriteLine($"\t\tExpected:\t{tr.Expected}");
                        Console.WriteLine($"\t\tActual:  \t{tr.Actual}");
                    }
                }
            }
        }

        private static IEnumerable<IPuzzle> GetPuzzles()
        {
            foreach(var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()))
            {
                if(!type.IsInterface && !type.IsAbstract && typeof(IPuzzle).IsAssignableFrom(type))
                {
                    yield return (IPuzzle)Activator.CreateInstance(type);
                }
            }
        }
    }
}

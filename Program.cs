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
                Console.WriteLine($"--- Day {puzzle.Day}: Part {puzzle.Part}");
                foreach(var tr in puzzle.Test())
                {
                    if(tr.Passed)
                    {
                        Console.WriteLine($"Test '{tr.TestName}' passed");
                    }
                    else
                    {
                        Console.WriteLine($"Test '{tr.TestName}' failed: {tr.ErrorMessage}");
                        Console.WriteLine($"\tExpected:\t{tr.Expected}");
                        Console.WriteLine($"\tActual:  \t{tr.Actual}");
                    }
                }
            }
        }

        private static IEnumerable<IPuzzle> GetPuzzles()
        {
            foreach(var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()))
            {
                if(!type.IsInterface && typeof(IPuzzle).IsAssignableFrom(type))
                {
                    yield return (IPuzzle)Activator.CreateInstance(type);
                }
            }
        }
    }
}

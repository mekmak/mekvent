using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mekvent.Days.Twelve
{
    public interface IRouteFinder
    {
        int NumRoutes(CaveSystem system, string start, string destination);
    }

    public class VisitSmallOnlyOnceFinder : IRouteFinder
    {
        public int NumRoutes(CaveSystem system, string start, string destination)
        {          
            int GetNumRoutes(string start, string destination, HashSet<string> visitedCaves)
            {
                if(start == destination)
                {
                    return 1;
                }

                var toSet = system.GetDestinations(start);
                if(!toSet.Any())
                {
                    return 0;
                }

                int numRoutes = 0;
                foreach(var to in toSet)
                {
                    if(visitedCaves.Contains(to))
                    {
                        continue;
                    }

                    var newVisited = new HashSet<string>(visitedCaves);
                    if(system.IsSmallCave(to))
                    {
                        newVisited.Add(to);
                    }

                    numRoutes += GetNumRoutes(to, destination, newVisited);
                }

                return numRoutes;
            }

            return GetNumRoutes("start", "end", new HashSet<string>{"start"});
        }
    }

    public class VisitOneSmallTwiceFinder : IRouteFinder
    {
        private class VisitedCaves
        {
            private bool _visitedTwice;
            private HashSet<string> _visited;
            private readonly string _startCaveName;

            public VisitedCaves(string startCaveName) : this(startCaveName, new HashSet<string>()) { }
            public VisitedCaves(string startCaveName, HashSet<string> visited) : this(startCaveName, visited, false) { }

            private VisitedCaves(string startCaveName, HashSet<string> visited, bool visitedTwice)
            {
                _visited = new HashSet<string>(visited);
                _visitedTwice = visitedTwice;
                _startCaveName = startCaveName;
            }

            public bool CanVisit(string cave)
            {
                if(cave == _startCaveName)
                {
                    return false;
                }

                bool visited = _visited.Contains(cave);
                return !visited || !_visitedTwice;
            }

            public void Visit(string cave)
            {
                bool added = _visited.Add(cave);
                if(!added)
                {
                    if(_visitedTwice)
                    {
                        throw new Exception($"Asked to re-visit {cave} but we've already revisited one");
                    }

                    _visitedTwice = true;
                }
            }

            public VisitedCaves Clone()
            {
                return new VisitedCaves(_startCaveName, _visited, _visitedTwice);
            }
        }

        public int NumRoutes(CaveSystem system, string start, string destination)
        {          
            int GetNumRoutes(string start, string destination, VisitedCaves visitedCaves)
            {
                if(start == destination)
                {
                    return 1;
                }

                var toSet = system.GetDestinations(start);
                if(!toSet.Any())
                {
                    return 0;
                }

                int numRoutes = 0;
                foreach(var to in toSet)
                {
                    if(!visitedCaves.CanVisit(to))
                    {
                        continue;
                    }

                    var newVisited = visitedCaves.Clone();
                    if(system.IsSmallCave(to))
                    {
                        newVisited.Visit(to);
                    }

                    numRoutes += GetNumRoutes(to, destination, newVisited);
                }

                return numRoutes;
            }

            var visited = new VisitedCaves("start", new HashSet<string> {"start"});
            return GetNumRoutes("start", "end", visited);
        }
    }

    public class CaveSystem
    {
        private readonly Dictionary<string, HashSet<string>> _caveRoutes;
        public CaveSystem()
        {
            _caveRoutes = new Dictionary<string, HashSet<string>>();
        }

        public bool IsSmallCave(string cave) => char.IsLower(cave[0]);

        public void AddOneWayRoute(string from, string to)
        {
            if(!_caveRoutes.TryGetValue(from, out var toSet))
            {
                toSet = new HashSet<string>();
                _caveRoutes[from] = toSet;
            }

            toSet.Add(to);
        }

        public void AddTwoWayRoute(string from, string to)
        {
            AddOneWayRoute(from, to);
            AddOneWayRoute(to, from);            
        }

        public HashSet<string> GetDestinations(string from)
        {
            if(!_caveRoutes.TryGetValue(from, out var toSet))
            {
                toSet = new HashSet<string>();
            }

            return toSet;
        }        

        public static CaveSystem Init(List<string> inputs)
        {
            var system = new CaveSystem();
            foreach(string input in inputs)
            {
                var fromTo = input.Split("-", StringSplitOptions.RemoveEmptyEntries);
                if(fromTo.Length != 2)
                {
                    throw new Exception($"Invalid entry {input}");
                }

                system.AddTwoWayRoute(fromTo[0], fromTo[1]);
            }

            return system;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach((string from, HashSet<string> toSet) in _caveRoutes)
            foreach(var to in toSet)
            {
                sb.AppendLine($"{from}-{to}");
            }
            
            return sb.ToString();
        }
    }

    public class PartOne
    {
        public int GetNumRoutes(List<string> inputs)
        {
            var system = CaveSystem.Init(inputs);
            var finder = new VisitSmallOnlyOnceFinder();
            return finder.NumRoutes(system, "start", "end");
        }
    }

    public class PartTwo
    {
        public int GetNumRoutes(List<string> inputs)
        {
            var system = CaveSystem.Init(inputs);
            var finder = new VisitOneSmallTwiceFinder();
            return finder.NumRoutes(system, "start", "end");
        }
    }
}
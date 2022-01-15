using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.Nine
{
    public class HeightReading
    {
        public HeightReading(int reading, int row, int col)
        {
            Row = row;
            Column = col;
            Reading = reading;
        }

        public int Row {get;}
        public int Column {get;}
        public int Reading {get;}

        public override bool Equals(object obj)
        {
            return obj is HeightReading reading &&
                   Row == reading.Row &&
                   Column == reading.Column &&
                   Reading == reading.Reading;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Row, Column, Reading);
        }

        public override string ToString()
        {
            return $"{Reading} ({Row},{Column})";
        }
    }

    public class HeightMap
    {
        private readonly int _numRows;
        private readonly int _numCols;
        private readonly HeightReading[,] _readings;
        public HeightMap(int[,] readings)
        {
            _numRows = readings.GetLength(0);
            _numCols = readings.GetLength(1);

            _readings = new HeightReading[_numRows, _numCols];
            for(int row = 0; row < _numRows; row++)
            {
                for(int col = 0; col < _numCols; col++)
                {
                    _readings[row, col] = new HeightReading(readings[row,col], row, col);
                }
            }
        }

        public List<HeightReading> GetLowPoints()
        {
            var lowPoints = new List<HeightReading>();
            foreach(var reading in _readings)
            {
                if(GetNeighbors(reading).All(n => n.Reading > reading.Reading))
                {
                    lowPoints.Add(reading);
                }
            }
            return lowPoints;
        }

        public List<HeightReading> GetNeighbors(HeightReading reading)
        {
            var neighbs = new List<HeightReading>();
            foreach(var move in new [] { (0,1), (0,-1), (-1,0), (1,0) })
            {
                int row = reading.Row + move.Item1;
                int col = reading.Column + move.Item2;

                if(TryGetReading(row, col, out var neighb))
                {
                    neighbs.Add(neighb);
                }
            }
            return neighbs;
        }

        private bool TryGetReading(int row, int col, out HeightReading reading)
        {
            if(row < 0 || row >= _numRows || col < 0 || col >= _numCols)
            {
                reading = null;
                return false;
            }

            reading = _readings[row, col];
            return true;
        }

        public static HeightMap Init(List<string> inputs)
        {
            int numRows = inputs.Count;
            int numCols = inputs.First().Length;

            var readings = new int[numRows, numCols];
            for(int row = 0; row < numRows; row++)
            {
                for(int col = 0; col < numCols; col++)
                {
                    int val = int.Parse(inputs[row][col].ToString());
                    readings[row,col] = val;
                }
            }

            return new HeightMap(readings);
        }
    }

    public class PartOne
    {
        public int CalculateRiskLevel(List<string> inputs)
        {
            var map = HeightMap.Init(inputs);
            var lowPoints = map.GetLowPoints();
            var sum = lowPoints.Sum(p => p.Reading + 1);
            return sum;
        }
    }

    public class PartTwo
    {
        public int CalculateBasinSize(List<string> inputs)
        {
            var map = HeightMap.Init(inputs);

            var basinSizes = new List<int>();
            foreach(HeightReading lowPoint in map.GetLowPoints())
            {
                var neighbs = new Queue<HeightReading>();
                neighbs.Enqueue(lowPoint);
                

                int basinSize = 0;
                var seen = new HashSet<HeightReading>();
                seen.Add(lowPoint);

                while(neighbs.Any())
                {
                    HeightReading neighb = neighbs.Dequeue();
                    basinSize++;

                    foreach(var n in map.GetNeighbors(neighb).Where(n => n.Reading != 9 && !seen.Contains(n)))
                    {
                        neighbs.Enqueue(n);
                        seen.Add(n);
                    }
                }
                basinSizes.Add(basinSize);
            }

            var threeLargest = basinSizes.OrderByDescending(b => b).Take(3).ToList();
            return threeLargest.Product(t => t);
        }
    }

    public static class EnumerableExtensions
    {
        public static int Product<T>(this IEnumerable<T> input, Func<T, int> map)
        {
            int product = 1;
            foreach(var t in input)
            {
                product *= map(t);
            }
            return product;
        }   
    }

}
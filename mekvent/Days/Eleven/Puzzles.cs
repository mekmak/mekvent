using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mekvent.Days.Eleven
{
    public class EnergyReading
    {
        public EnergyReading(int row, int col, int level)
        {
            Row = row;
            Column = col;
            EnergyLevel = level;
        }

        public int Row {get;}
        public int Column {get;}
        public int EnergyLevel {get;set;}

        public override string ToString()
        {
            return $"{EnergyLevel} ({Row},{Column})";
        }
    }

    public class EnergyLevels : IEnumerable<EnergyReading>
    {
        public int NumRows => _numRows;
        public int NumCols => _numCols;

        private readonly int _numRows;
        private readonly int _numCols;
        private readonly EnergyReading[,] _readings;

        public EnergyLevels(int[,] levels)
        {
            _numRows = levels.GetLength(0);
            _numCols = levels.GetLength(1);

            _readings = new EnergyReading[_numRows, _numCols];
            for(int row = 0; row < _numRows; row++)
            {
                for(int col = 0; col < _numCols; col++)
                {
                    _readings[row,col] = new EnergyReading(row, col, levels[row,col]);
                }
            }
        }

        public List<EnergyReading> GetNeighbors(EnergyReading reading)
        {
            var neighbs = new List<EnergyReading>();
            foreach(var move in new []{ (1,0), (-1,0), (0,1), (0,-1), (1,1), (1,-1), (-1,1), (-1,-1) })
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

        private bool TryGetReading(int row, int col, out EnergyReading reading)
        {
            if(row < 0 || row >= _numRows || col < 0 || col >= _numCols)
            {
                reading = null;
                return false;
            }

            reading = _readings[row, col];
            return true;
        }

        public IEnumerator<EnergyReading> GetEnumerator()
        {
            foreach(var reading in _readings)
            {
                yield return reading;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static EnergyLevels Init(List<string> inputs)
        {
            int numRows = inputs.Count;
            int numCols = inputs.First().Length;

            var readings = new int[numRows,numCols];
            for(int row = 0; row < numRows; row++)
            {
                for(int col = 0; col < numCols; col++)
                {
                    int reading = int.Parse(inputs[row][col].ToString());
                    readings[row,col] = reading;
                }
            }

            return new EnergyLevels(readings);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for(int row = 0; row < _numRows; row++)
            {
                for(int col = 0; col < _numCols; col++)
                {
                    sb.Append(_readings[row,col].EnergyLevel);
                }

                if(row != _numRows - 1)
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }

    public class DumboOctopusGrid
    {
        private const int FlashLevel = 10;

        public static EnergyLevels ExecuteStep(EnergyLevels before)
        {
            var afterReadings = new int[before.NumRows, before.NumCols];
            foreach(var reading in before)
            {
                int newReading = reading.EnergyLevel + 1;
                if(newReading > FlashLevel)
                {
                     throw new Exception($"Invalid reading: {reading}");
                }

                afterReadings[reading.Row, reading.Column] = newReading;
            }
            var after = new EnergyLevels(afterReadings);

            var flashQueue = new Queue<EnergyReading>(after.Where(r => r.EnergyLevel == FlashLevel));
            while(flashQueue.Any())
            {
                var nextFlash = flashQueue.Dequeue();
                var neighbs = after.GetNeighbors(nextFlash);
                foreach(var neighb in neighbs)
                {
                    if(neighb.EnergyLevel == FlashLevel)
                    {
                        continue;
                    }

                    neighb.EnergyLevel += 1;
                    if(neighb.EnergyLevel == FlashLevel)
                    {
                        flashQueue.Enqueue(neighb);
                    }
                }
            }

            foreach(var reading in after.Where(r => r.EnergyLevel == FlashLevel))
            {
                reading.EnergyLevel = 0;
            }

            return after;
        }
    }

    public class PartOne
    {
        public int CalculateTotalFlashes(List<string> inputs, int numOfSteps)
        {
            var levels = EnergyLevels.Init(inputs);

            int numFlashed = 0;
            for(int step = 0; step < numOfSteps; step++)
            {
                levels = DumboOctopusGrid.ExecuteStep(levels);
                numFlashed += levels.Count(r => r.EnergyLevel == 0);
            }
            return numFlashed;
        }
    }

    public class PartTwo
    {
        public int FirstSimultaneousFlashStep(List<string> inputs)
        {
            var levels = EnergyLevels.Init(inputs);
            var numOctos = levels.NumRows * levels.NumCols;

            const int sanityCheck = 1000;
            int stepNumber = 0;
            while(stepNumber < sanityCheck)
            {
                stepNumber++;
                levels = DumboOctopusGrid.ExecuteStep(levels);

                if(levels.Count(r => r.EnergyLevel == 0) == numOctos)
                {
                    return stepNumber;
                }
            }

            throw new Exception($"Reached step {stepNumber} without finding a simultaneous flash =(");
        }
    }
}
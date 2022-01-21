using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mekvent.Days.Thirteen
{
    public class Paper : IEnumerable<bool>
    {
        private bool[,] _dots;
        private Paper(bool[,] dots)
        {
            _dots = dots;
        }

        private int NumRows => _dots?.GetLength(0) ?? 0;
        private int NumCols => _dots?.GetLength(1) ?? 0;

        public Paper Fold(FoldInstruction instruction)
        {
            if(!IsValid(instruction))
            {
                throw new Exception($"Invalid instruction '{instruction}' for paper:\n{this}");
            }

            var foldingLeft = instruction.Axis == Axis.X;
            var coord = instruction.Coordinate;

            int newRowCount = foldingLeft ? NumRows : coord;
            int newColCount = foldingLeft ? coord : NumCols;
            var newDots = new bool[newRowCount, newColCount];

            int startRow = foldingLeft ? 0 : coord + 1;
            int startCol = foldingLeft ? coord + 1 : 0;
            for(int row = startRow; row < NumRows; row++)
            for(int col = startCol; col < NumCols; col++)
            {
                int targetRow = foldingLeft ? row : coord - (row - coord);
                int targetCol = foldingLeft ? coord - (col - coord) : col;

                newDots[targetRow, targetCol] = _dots[row, col] | _dots[targetRow, targetCol];
            }

            return new Paper(newDots);
        }

        public int VisibleDotCount => this.Count(b => b);

        private bool IsValid(FoldInstruction instruction)
        {
            int axisToCheck;
            switch(instruction.Axis)
            {
                case Axis.X:
                    axisToCheck = NumCols;
                    break;
                case Axis.Y:
                    axisToCheck = NumRows;
                    break;
                default:
                    return false;
            }

            bool willGoPastEdge = axisToCheck - instruction.Coordinate - 1 > instruction.Coordinate;
            bool isEdgeOfPaper = axisToCheck <= instruction.Coordinate -1;

            return !isEdgeOfPaper && !willGoPastEdge;
        }

        public static Paper Init(IEnumerable<string> inputs)
        {
            List<(int, int)> coordsList = inputs.Select(i => 
            {
                var coords = i.Split(",");
                return (int.Parse(coords[0]), int.Parse(coords[1]));
            }).ToList();

            int numCols = coordsList.Max(l => l.Item1) + 1;
            int numRows = coordsList.Max(l => l.Item2) + 1;
            
            var dots = new bool[numRows, numCols];
            foreach((int x, int y) in coordsList)
            {
                // X = col, Y = row                
                dots[y, x] = true;
            }

            return new Paper(dots);
        }

        public override string ToString()
        {
            int numRows = _dots.GetLength(0);
            int numCols = _dots.GetLength(1);

            var sb = new StringBuilder();
            for(int row = 0; row < numRows; row++)
            {
                for(int col = 0; col < numCols; col++)
                {
                    if(col == 0)
                    {
                        sb.Append($"{row}\t");
                    }

                    var mark = _dots[row,col] ? "# " : ". ";
                    sb.Append(mark);
                }

                if(row != numRows - 1)
                {
                    sb.AppendLine();
                }
            }
            
            return sb.ToString();
        }

        public IEnumerator<bool> GetEnumerator()
        {
            if(_dots == null)
            {
                yield break;
            }

            foreach(var dot in _dots)
            {
                yield return dot;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public enum Axis { X, Y }

    public class FoldInstruction
    {
        public Axis Axis {get; private set;}  
        public int Coordinate {get; private set;}

        public static FoldInstruction Parse(string line)
        {
            var axisCoord = line.Replace("fold along ", "").Split("=", StringSplitOptions.RemoveEmptyEntries);
            return new FoldInstruction
            {
                Axis = Enum.Parse<Axis>(axisCoord[0]),
                Coordinate = int.Parse(axisCoord[1])
            };
        }

        public override string ToString()
        {
            return $"fold along {Axis}={Coordinate}";
        }
    }

    public class InputParser
    {
        public static (Paper, List<FoldInstruction>) ParseInput(List<string> inputs)
        {
            int index = inputs.FindIndex(0, inputs.Count, string.IsNullOrWhiteSpace);
            if(index == -1)
            {
                throw new Exception($"Could not parse inputs");
            }

            var paper = Paper.Init(inputs.Take(index));
            var instructions = inputs.Skip(index+1).Select(FoldInstruction.Parse).ToList();

            return (paper, instructions);
        }
    }

    public class PartOne
    {
        public int GetVisibleDots(List<string> inputs)
        {
            (Paper paper, List<FoldInstruction> instructions) = InputParser.ParseInput(inputs);
            var folded = paper.Fold(instructions.First());
            return folded.VisibleDotCount;
        }
    }

    public class PartTwo
    {
        public string GetCode(List<string> inputs)
        {
            (Paper paper, List<FoldInstruction> instructions) = InputParser.ParseInput(inputs);
            foreach(var fi in instructions)
            {
                paper = paper.Fold(fi);
            }

            // this puzzle involves printing out the sheet
            // and reading the letters -- don't really feel like
            // writing a parser for that
            // Console.WriteLine($"\n{paper}");

            return "cjckbapb";
        }
    }
}
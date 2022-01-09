using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mekvent.Days.Five
{
    public class Point 
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X {get;}
        public int Y {get;}        

        public static Point Parse(string input)
        {
            var split = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if(split.Length != 2)
            {
                throw new Exception($"Could not parse point: {input}");
            }

            if(!int.TryParse(split[0], out int x) || !int.TryParse(split[1], out int y))
            {
                throw new Exception($"Could not parse x or y: {input}");
            }

            return new Point(x, y);
        }        

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

    public class VentRange
    {
        public VentRange(Point first, Point second)
        {
            First = first;
            Second = second;
        }

        public Point First {get;}
        public Point Second {get;}

        public static VentRange Parse(string input)
        {
            var points = input.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
            if(points.Length != 2)
            {
                throw new Exception($"Could not find the two points: {input}");
            }

            return new VentRange(Point.Parse(points[0]), Point.Parse(points[1]));
        }

        public override string ToString()
        {
            return $"{First} -> {Second}";
        }
    }

    public class VentBoard : IEnumerable<VentBoard.Cell>
    {
        public class Cell
        {
            public int NumVentsWithoutDiag {get;set;}
            public int NumVentsWithDiag {get;set;}
        }

        private Cell[,] _cells;
        private VentBoard(Cell[,] cells)
        {
            _cells = cells;
        }

        public static VentBoard Init(List<VentRange> ranges)
        {
            var allPoints = ranges.SelectMany(vr => new Point[]{vr.First, vr.Second}).ToList();
            int maxX = allPoints.Max(p => p.X) + 1;
            int maxY = allPoints.Max(p => p.Y) + 1;    

            // C# 2d arrays are accessed [row,col], so [y,x]
            var cells = new Cell[maxY, maxX];
            for(int row = 0; row < maxY; row++)
            {
                for(int col = 0; col < maxX; col++)
                {
                    cells[row,col] = new Cell 
                    { 
                        NumVentsWithoutDiag = 0,
                        NumVentsWithDiag = 0
                    };
                }
            }

            foreach(var range in ranges)
            {
                if(range.First.Y == range.Second.Y)
                {
                    int row = range.First.Y;
                    int startCol = range.First.X > range.Second.X
                        ? range.Second.X
                        : range.First.X;

                    int stopCol = range.First.X > range.Second.X
                        ? range.First.X
                        : range.Second.X;

                    for(int col = startCol; col <= stopCol; col++)
                    {
                        cells[row, col].NumVentsWithoutDiag += 1;
                        cells[row, col].NumVentsWithDiag += 1;
                    }

                    continue;
                }

                if(range.First.X == range.Second.X)
                {
                    int col = range.First.X;
                    int startRow = range.First.Y > range.Second.Y
                        ? range.Second.Y
                        : range.First.Y;

                    int stopRow = range.First.Y > range.Second.Y
                        ? range.First.Y
                        : range.Second.Y;

                    for(int row = startRow; row <= stopRow; row++)
                    {
                        cells[row, col].NumVentsWithoutDiag += 1;
                        cells[row, col].NumVentsWithDiag += 1;
                    }

                    continue;
                }

                bool isForwardSlash = range.First.X - range.Second.X == range.Second.Y - range.First.Y;
                bool isBackSlash = range.First.X - range.Second.X == range.First.Y - range.Second.Y;
                if(isBackSlash || isForwardSlash)
                {
                    var startPoint = range.First.X > range.Second.X ? range.Second : range.First;
                    var endPoint = range.First.X > range.Second.X ? range.First : range.Second;

                    var currentPoint = new Point(startPoint.X, startPoint.Y);
                    while(currentPoint.X <= endPoint.X)
                    {
                        if(currentPoint.X < 0 || currentPoint.X >= maxX || currentPoint.Y < 0 || currentPoint.Y >= maxY)
                        {
                            throw new Exception($"Invalid point {currentPoint} during {(isBackSlash ? "backslash" : "forward slash")} diag: {range}");
                        }

                        cells[currentPoint.Y, currentPoint.X].NumVentsWithDiag += 1;

                        var newX = currentPoint.X + 1;
                        var newY = isBackSlash ? currentPoint.Y + 1 : currentPoint.Y -1;
                        currentPoint = new Point(newX, newY);
                    }

                    continue;
                }

                throw new Exception($"Unsupported range type {range}");
            }

            return new VentBoard(cells);
        }

        public string Format()
        {
            int rows = _cells.GetLength(0);
            int cols = _cells.GetLength(1);

            var sb = new StringBuilder();
            for(int row = 0; row < rows; row++)
            {
                sb.Append($"Row {row}:\t");
                for(int col = 0; col < cols; col++)
                {
                    var num = _cells[row,col].NumVentsWithoutDiag;
                    sb.Append(num == 0 ? "." : num.ToString());
                }

                if(row != rows-1)
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach(var cell in _cells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PartOne
    {
        public int NumOverlappingLines(List<string> input)
        {
            var ranges = input.Select(VentRange.Parse).ToList();
            var board = VentBoard.Init(ranges);
            return board.Where(c => c.NumVentsWithoutDiag > 1).Count();
        }
    }

    public class PartTwo
    {
        public int NumOverlappingLines(List<string> input)
        {
            var ranges = input.Select(VentRange.Parse).ToList();
            var board = VentBoard.Init(ranges);
            return board.Where(c => c.NumVentsWithDiag > 1).Count();
        }
    }
}
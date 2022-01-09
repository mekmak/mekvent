using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mekvent.Days.Four
{
    public class Board 
    {
        private class Cell
        {
            public int Value {get;set;}
            public bool Marked {get;set;}
            public int Row {get;set;}
            public int Col {get;set;}

        }
        
        public Board(int size)
        {
            Size = size;
            _cells = new Cell[size, size];
            _valueToCell = new Dictionary<int, Cell>();
        }

        public int Size {get;}

        private Cell[,] _cells {get;set;}
        private Dictionary<int, Cell> _valueToCell;

        public void SetCell(int row, int col, int value)
        {
            if(_cells[row,col] != null)
            {
                throw new Exception($"Row {row} col {col} already set");
            }

            if(_valueToCell.ContainsKey(value))
            {
                throw new Exception($"Value {value} already seen in board");
            }

            var cell = new Cell
            {
                Value = value,
                Marked = false,
                Row = row,
                Col = col
            };

            _cells[row,col] = cell;
            _valueToCell[value] = cell;
        }

        public bool MarkCell(int value)
        {
            if(!_valueToCell.TryGetValue(value, out Cell cell))
            {
                return false;
            }

            if(cell.Marked)
            {
                throw new Exception($"Cell {value} already marked");
            }

            cell.Marked = true;

            bool rowMarked = true;
            for(int row = 0; row < Size; row++)
            {
                rowMarked &= _cells[row, cell.Col].Marked;
                if(!rowMarked)
                {
                    break;
                }
            }

            bool colMarked = true;
            for(int col = 0; col < Size; col++)
            {
                colMarked &= _cells[cell.Row, col].Marked;
                if(!colMarked)
                {
                    break;
                }
            }

            return rowMarked || colMarked;
        }

        public IEnumerable<int> GetUnmarkedValues()
        {
            foreach(var cell in _cells)
            {
                if(!cell.Marked)
                {
                    yield return cell.Value;
                }
            }
        }

        public int GetCellValue(int row, int col)
        {
            if(row < 0 || row >= Size || col < 0 || col >= Size)
            {
                throw new Exception($"Invalid row {row} or col {col} for board size {Size}");
            }

            if(_cells[row,col] == null)
            {
                throw new Exception($"Cell at {row},{col} not set");
            }

            return _cells[row,col].Value;
        }
    }

    public class BoardParser
    {
        private readonly int _size;
        public BoardParser(int size)
        {
            _size = size;
        }

        public (List<int>, List<Board>) ParseInput(List<string> lines)
        {
            List<int> calledNums = lines.First()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var boards = new List<Board>();

            Board currentBoard = new Board(_size);
            int row = 0;
            bool expectingNewLine = true;

            foreach(string line in lines.Skip(1))
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    if(!expectingNewLine)
                    {
                        throw new Exception($"Not enough rows ({row}) in current board");
                    }

                    expectingNewLine = false;
                    continue;
                }

                if(expectingNewLine)
                {
                    throw new Exception($"Too many rows in current board. Line: {line}");
                }

                List<int> columns = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                if(columns.Count != _size)
                {
                    throw new Exception($"Expecting {_size} columns but got {columns.Count}: {line}");
                }

                for(int col = 0; col < _size; col++)
                {
                    currentBoard.SetCell(row, col, columns[col]);
                }
                
                if(row == _size - 1)
                {
                    boards.Add(currentBoard);

                    currentBoard = new Board(_size);
                    row = 0;
                    expectingNewLine = true;
                }
                else
                {
                    row++;
                }
            }

            return (calledNums, boards);
        }

        public string FormatBoard(Board board)
        {
            var sb = new StringBuilder();
            for(int row = 0; row < board.Size; row++)
            {
                for(int col = 0; col < board.Size; col++)
                {
                    int val = board.GetCellValue(row, col);
                    sb.Append($"{val}\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    public class First : Puzzle
    {
        public override int Day => 4;
        public override int Part => 1;
        public override string Name => "Giant Squid";

        public int GetFinalScore(List<string> input)
        {
            var boardParser = new BoardParser(5);
            (List<int> called, List<Board> boards) = boardParser.ParseInput(input);

            foreach(var num in called)
            {
                foreach(var board in boards)
                {
                    bool winner = board.MarkCell(num);
                    if(!winner)
                    {
                        continue;
                    }

                    var sum = board.GetUnmarkedValues().Sum();
                    var score = sum * num;
                    return score;
                }
            }

            throw new Exception($"No board won after all numbers called");
        }

        public override List<TestResult> Test()
        {            
            return new List<TestResult>
            {
                RunTest("Test", () => ("4512", GetFinalScore(ReadInput(true)).ToString())),
                RunTest("Official", () => ("35711", GetFinalScore(ReadInput(false)).ToString()))
            };
        }
    }

    public class Second : Puzzle
    {
        public override int Day => 4;
        public override int Part => 2;
        public override string Name => "Giant Squid";

        public int GetFinalScore(List<string> input)
        {
            var boardParser = new BoardParser(5);
            (List<int> called, List<Board> boards) = boardParser.ParseInput(input);

            var boardWinners = boards.Select(b => false).ToArray();
            var boardWinnerCount = 0;

            foreach(var num in called)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    if(boardWinners[i])
                    {
                        continue;
                    }

                    Board board = boards[i];
                    bool winner = board.MarkCell(num);
                    if(winner)
                    {
                        boardWinners[i] = true;
                        boardWinnerCount++;
                    }

                    if(boardWinnerCount == boards.Count)
                    {
                        var sum = board.GetUnmarkedValues().Sum();
                        var score = sum * num;
                        return score;
                    }
                }
            }

            throw new Exception($"Not all boards eventually won");
        }

        public override List<TestResult> Test()
        {            
            return new List<TestResult>
            {
                RunTest("Test", () => ("1924", GetFinalScore(ReadInput(true)).ToString())),
                RunTest("Official", () => ("5586", GetFinalScore(ReadInput(false)).ToString()))
            };
        }
    }
}
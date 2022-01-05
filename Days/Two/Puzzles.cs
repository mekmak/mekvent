using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mekvent.Days.Two
{
    public class SubCommand
    {
        public enum CommandType
        {
            Forward,
            Up,
            Down
        }

        public SubCommand(CommandType command, int amount)
        {
            Command = command;
            Amount = amount;
        }

        public CommandType Command {get;}
        public int Amount {get;}

        public static SubCommand Parse(string inputLine)
        {
            if(inputLine == null)
            {
                throw new ArgumentNullException(nameof(inputLine));
            }

            var parts = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length != 2)
            {
                throw new ArgumentException($"Input line must have exactly two parts: {inputLine}");
            }

            if(!Enum.TryParse(typeof(CommandType), parts[0], true, out object command))
            {
                throw new ArgumentException($"Could not parse sub command {parts[0]}");
            }

            if(!int.TryParse(parts[1], out int amount))
            {
                throw new ArgumentException($"Could not parse amount {parts[1]}");
            }

            return new SubCommand((CommandType) command, amount);
        }
    }

    public class First : Puzzle
    {
        public override int Day => 2;
        public override int Part => 1;

        private int GetFinalPosition(List<SubCommand> commands)
        {
            int position = 0;
            int depth = 0;

            foreach(SubCommand command in commands)
            {
                switch(command.Command)
                {
                    case SubCommand.CommandType.Down:
                        depth += command.Amount;
                        break;
                    case SubCommand.CommandType.Up:
                        depth -= command.Amount;
                        if(depth < 0)
                        {
                            throw new Exception($"Depth is below zero (last command: {command.Command} {command.Amount})");
                        }
                        break;
                    case SubCommand.CommandType.Forward:
                        position += command.Amount;
                        break;
                    default:
                        throw new Exception($"Unsupported command type {command.Command}");
                }
            }

            return position * depth;
        }

        public override List<TestResult> Test()
        {
            return new List<TestResult>
            {
                RunTest("Final Position - Test", () => ("150", GetFinalPosition(ReadInput<SubCommand>(true, SubCommand.Parse)).ToString())),
                RunTest("Final Position - Official", () => ("2322630", GetFinalPosition(ReadInput<SubCommand>(false, SubCommand.Parse)).ToString()))
            };
        }
    }

    public class Second : Puzzle
    {
        public override int Day => 2;
        public override int Part => 2;

        private int GetFinalPosition(List<SubCommand> commands)
        {
            int position = 0;
            int depth = 0;
            int aim = 0;

            foreach(SubCommand command in commands)
            {
                switch(command.Command)
                {
                    case SubCommand.CommandType.Down:
                        aim += command.Amount;
                        break;
                    case SubCommand.CommandType.Up:
                        aim -= command.Amount;
                        break;
                    case SubCommand.CommandType.Forward:
                        position += command.Amount;
                        depth += aim * command.Amount;
                        break;
                    default:
                        throw new Exception($"Unsupported command type {command.Command}");
                }
            }

            return position * depth;
        }

        public override List<TestResult> Test()
        {
            return new List<TestResult>
            {
                RunTest("Final Position - Test", () => ("900", GetFinalPosition(ReadInput<SubCommand>(true, SubCommand.Parse)).ToString())),
                RunTest("Final Position - Official", () => ("2105273490", GetFinalPosition(ReadInput<SubCommand>(false, SubCommand.Parse)).ToString()))
            };
        }
    }
}
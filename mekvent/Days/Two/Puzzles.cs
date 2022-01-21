using System;
using System.Collections.Generic;
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

            var command = Enum.ParseEnum<CommandType>(parts[0]);
            if(!int.TryParse(parts[1], out int amount))
            {
                throw new ArgumentException($"Could not parse amount {parts[1]}");
            }

            return new SubCommand(command, amount);
        }
    }

    public class PartOne
    {
        public int GetFinalPosition(List<string> inputs)
        {
            int position = 0;
            int depth = 0;

            List<SubCommand> commands = inputs.Select(SubCommand.Parse).ToList();
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
    }

    public class PartTwo
    {
        public int GetFinalPosition(List<string> inputs)
        {
            int position = 0;
            int depth = 0;
            int aim = 0;

            List<SubCommand> commands = inputs.Select(SubCommand.Parse).ToList();
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
    }
}
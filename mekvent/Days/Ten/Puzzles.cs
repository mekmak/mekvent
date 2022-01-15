using System;
using System.Collections.Generic;
using System.Linq;

namespace mekvent.Days.Ten
{
    public class Syntax
    {
        public static int GetCompletionCharPoints(char closer)
        {
            switch(closer)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;
                default:
                    throw new Exception($"Cannot determine score for closer '{closer}'");
            }
        }

        public static int GetIllegalCharPoints(char illegalChar)
        {
            switch(illegalChar)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    throw new Exception($"Cannot determine point value for illegal character '{illegalChar}'");
            }
        }

        public static bool IsOpener(char character)
        {
            switch(character)
            {
                case '(':
                case '[':
                case '{':
                case '<':
                    return true;
            }

            return false;
        }

        public static char GetOpener(char closer)
        {
            switch(closer)
            {
                case ')':
                    return '(';
                case ']':
                    return '[';
                case '}':
                    return '{';
                case '>':
                    return '<';
                default:
                    throw new Exception($"Cannot determine opener for closer '{closer}'");
            }
        }

        public static char GetCloser(char opener)
        {
            switch(opener)
            {
                case '(':
                    return ')';
                case '[':
                    return ']';
                case '{':
                    return '}';
                case '<':
                    return '>';
                default:
                    throw new Exception($"Cannot determine closer for opener '{opener}'");
            }
        }
    }

    public class PartOne
    {
        public int CalculateSyntaxErrorScore(List<string> inputs)
        {
            var firstIllegal = new List<char>();
            foreach(string input in inputs)
            {
                var openers = new Stack<char>();
                foreach(char character in input)
                {
                    if(Syntax.IsOpener(character))
                    {
                        openers.Push(character);
                        continue;
                    }

                    char expectedOpener = Syntax.GetOpener(character);
                    char lastOpener = openers.Pop();
                    if(expectedOpener == lastOpener)
                    {
                        continue;
                    }

                    firstIllegal.Add(character);
                    break;
                }
            }

            return firstIllegal.Select(Syntax.GetIllegalCharPoints).Sum();
        }
    }

    public class PartTwo
    {
        public decimal FindMiddleCompletionScore(List<string> inputs)
        {
            var incompleteOpenerSets = new List<Stack<char>>();
            foreach(string input in inputs)
            {
                bool isCorrupted = false;
                var openers = new Stack<char>();
                foreach(char character in input)
                {
                    if(Syntax.IsOpener(character))
                    {
                        openers.Push(character);
                        continue;
                    }

                    char expectedOpener = Syntax.GetOpener(character);
                    char lastOpener = openers.Pop();
                    if(expectedOpener == lastOpener)
                    {
                        continue;
                    }

                    isCorrupted = true;
                    break;
                }

                if(!isCorrupted)
                {
                    incompleteOpenerSets.Add(openers);
                }
            }

            var completions = new List<List<char>>();
            foreach(var incompleteOpenerSet in incompleteOpenerSets)
            {
                var completion = new List<char>();
                while(incompleteOpenerSet.Any())
                {
                    var opener = incompleteOpenerSet.Pop();
                    var closer = Syntax.GetCloser(opener);
                    completion.Add(closer);
                }
                completions.Add(completion);
            }

            decimal CalculateScore(List<char> closers)
            {
                decimal score = 0;
                foreach(var closer in closers)
                {
                    decimal points = Syntax.GetCompletionCharPoints(closer);
                    score *= 5;
                    score += points;
                }
                return score;
            }

            var scores = completions.Select(CalculateScore).OrderBy(s => s).ToArray();
            decimal middleScore = scores[scores.Length / 2];
            return middleScore;
        }
    }
}
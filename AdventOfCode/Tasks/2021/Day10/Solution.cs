using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day010
{
    public class Solution : Solvable
    {
        private Dictionary<char, long> scoreMap = new Dictionary<char, long>();
        private Dictionary<char, long> scoreMap2 = new Dictionary<char, long>();

        public Solution()
        {
            scoreMap.Add(')', 3);
            scoreMap.Add(']', 57);
            scoreMap.Add('}', 1197);
            scoreMap.Add('>', 25137);
            
            scoreMap2.Add(')', 1);
            scoreMap2.Add(']', 2);
            scoreMap2.Add('}', 3);
            scoreMap2.Add('>', 4);
        }

        public long Part1(string input)
        {
            var lines = input.Split("\n");

            var badCharacters = new List<char>();
            foreach (var line in lines)
            {
                var (isValid, errorChar, _) = IsValid(line);
                if (!isValid)
                {
                    badCharacters.Add(errorChar);
                }
            }

            return CalculateScore(badCharacters);
        }

        private long CalculateScore(List<char> chars)
        {
            return chars.Select(c =>
            {
                scoreMap.TryGetValue(c, out var val);
                return val;
            }).Sum();
        }

        private (bool, char, Stack<char>) IsValid(string line)
        {
            var stack = new Stack<char>();
            foreach (char c in line)
            {
                switch (c)
                {
                    case '(':
                        stack.Push(')');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    case '{':
                        stack.Push('}');
                        break;
                    case '<':
                        stack.Push('>');
                        break;
                    default:
                        var charNeeded = stack.Pop();
                        if (c != charNeeded)
                        {
                            return (false, c, null);
                        }
                        break;
                }
            }

            return (true, default(char), stack);
        }

        public long Part2(string input)
        {
            var lines = input.Split("\n");

            var scores = new List<long>();
            foreach (var line in lines)
            {
                var score = 0L;
                var (isValid, _, stack) = IsValid(line);
                if (!isValid) continue;
                while (stack.Count() != 0)
                {
                    var c = stack.Pop();
                    score *= 5;
                    scoreMap2.TryGetValue(c, out var val);
                    score += val;
                } 
                scores.Add(score);
            }
            scores.Sort();
            return scores[scores.Count / 2];
        }
    }
}
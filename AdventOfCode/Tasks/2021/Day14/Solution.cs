using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2021.Day14
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            return Solve(input, 10);
        }

        private static long Solve(string input, int maxSteps)
        {
            var parts = input.Split("\n\n");
            var template = parts[0];
            var rules = parts[1].Split("\n");
            var ruleDict = new Dictionary<string, char>();
            var pairCount = new Dictionary<string, long>();
            foreach (var rule in rules)
            {
                var tempRules = rule.Split(" -> ");
                ruleDict[tempRules[0]] = Convert.ToChar(tempRules[1]);
            }

            for (var i = 0; i < template.Length - 1; i++)
            {
                var subString = $"{template[i]}{template[i + 1]}";
                pairCount[subString] = pairCount.GetValueOrDefault(subString) + 1;
            }

            for (var step = 0; step < maxSteps; step++)
            {
                var newPairs = new Dictionary<string, long>();
                foreach (var (key, value) in pairCount)
                {
                    var insert = ruleDict[key];
                    var p1 = $"{key[0]}{insert}";
                    var p2 = $"{insert}{key[1]}";
                    newPairs[p1] = newPairs.GetValueOrDefault(p1) + value;
                    newPairs[p2] = newPairs.GetValueOrDefault(p2) + value;
                }

                pairCount = newPairs;
            }

            var letterCount = new Dictionary<char, long>();
            foreach (var (key, value) in pairCount)
            {
                letterCount[key[0]] = letterCount.GetValueOrDefault(key[0]) + value;
                letterCount[key[1]] = letterCount.GetValueOrDefault(key[1]) + value;
            }

            letterCount[template[0]]++;
            letterCount[template.Last()]++;

            return (letterCount.Values.Max() - letterCount.Values.Min()) / 2;
        }

        public long Part2(string input)
        {
            return Solve(input, 40);
        }
    }
}
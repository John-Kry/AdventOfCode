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
            return Solve(input, true);
        }

        private static long Solve(string input, bool part1)
        {
            var maxSteps = part1 ? 10 : 40;
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
                string subString = template[i].ToString() + template[i + 1].ToString();
                if (pairCount.ContainsKey(subString))
                {
                    pairCount[subString]++;
                }
                else
                {
                    pairCount[subString] = 1;
                }
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

            foreach (var kvp in pairCount)
            {
                Console.WriteLine($"{kvp.Key}, {kvp.Value}");
            }

            var letterCount = new Dictionary<char, long>();
            foreach (var kvp in pairCount)
            {
                letterCount[kvp.Key[0]] = letterCount.GetValueOrDefault(kvp.Key[0]) + kvp.Value;
                letterCount[kvp.Key[1]] = letterCount.GetValueOrDefault(kvp.Key[1]) + kvp.Value;
            }
            letterCount[parts[1][0]]++;
            letterCount[parts[1].Last()]++;

            return (letterCount.Values.Max() - letterCount.Values.Min()) / 2;
        }

        public long Part2(string input)
        {
            return Solve(input, false);
        }
    }
}
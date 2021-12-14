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
            var maxSteps = part1 ? 5 : 40;
            var parts = input.Split("\n\n");
            var template = parts[0];
            var rules = parts[1].Split("\n");
            var ruleDict = new Dictionary<string, char>();
            foreach (var rule in rules)
            {
                var tempRules = rule.Split(" -> ");
                ruleDict[tempRules[0]] = Convert.ToChar(tempRules[1]);
            }

            for (var step = 0; step < maxSteps; step++)
            {
                var temp = template;
                // sliding window
                var additionalItem = 0;
                for (var i = 0; i < template.Length - 1; i++)
                {
                    string subString = template[i].ToString() + template[i + 1].ToString();
                    if (ruleDict.ContainsKey(subString))
                    {
                        temp = temp.Insert(i + 1 + additionalItem, ruleDict[subString].ToString());
                        additionalItem++;
                    }
                    Console.WriteLine(i);
                }

                Console.WriteLine(step);
                template = temp;
            }

            var resultAsArray = template.ToList();
            var query = resultAsArray.GroupBy(item => item).OrderByDescending(g => g.Count());
            var most = query.First().Count();
            var least = query.Last().Count();
            Console.WriteLine($"most: {most}, least {least}");
            return most - least;
        }

        public long Part2(string input)
        {
            return Solve(input, false);
        }
    }
}
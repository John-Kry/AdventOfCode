using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2015.Day07
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            var registers = new Dictionary<char, long>();
            var items = new char  ['d', 'e', 'f', 'g', 'h', 'i', 'x', 'y'];
            foreach (var c in items)
            {
                registers.TryAdd(c, 0);
            }
            var lines = input.Split("\n");
            foreach (var line in lines)
            {
                // var regex = new Regex(@"^\d+ -> .");
                // var match = regex.Matches(line);

                if (Regex.Match(line, @"^\d+ -> .").Success)
                {
                    Console.WriteLine(line);
                }

                if (Regex.Match(line, "AND").Success)
                {
                    Console.WriteLine(line);
                }

                if (Regex.Match(line, "OR").Success)
                {
                    Console.WriteLine(line);
                }

                if (Regex.Match(line, "LSHIFT").Success)
                {
                    Console.WriteLine(line);
                }

                if (Regex.Match(line, "RSHIFT").Success)
                {
                    Console.WriteLine(line);
                }
                
                if (Regex.Match(line, "NOT").Success)
                {
                    Console.WriteLine(line);
                }
                // var 
            }

            return 0;
        }



        public long Part2(string input)
        {
            return 0;
        }
    }
}
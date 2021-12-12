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
            var registers = new Dictionary<string, short>();
            var lines = input.Split("\n");
            foreach (var line in lines)
            {
                // 123 -> x;
                if (Regex.Match(line, @"^\d+ -> .").Success)
                {
                    var number = Int16.Parse(Regex.Match(line, @"^\d+").Value);
                    SaveIntoRegister(registers, line, number);
                }

                if (Regex.Match(line, "AND").Success)
                {
                    var matches = Regex.Matches(line, @"[a-z]+");
                    var number = registers[matches[0].Value] & registers[matches[1].Value];
                    SaveIntoRegister(registers, line, number);
                }

                if (Regex.Match(line, "OR").Success)
                {
                    var matches = Regex.Matches(line, @"[a-z]+");
                    var number = registers[matches[0].Value] | registers[matches[1].Value];
                    SaveIntoRegister(registers, line, number);
                }

                if (Regex.Match(line, "LSHIFT").Success)
                {
                    var matches = Regex.Matches(line, @"[a-z]+");
                    var number = registers[matches[0].Value] << Int16.Parse(Regex.Match(line, @"\d+").Value);
                    SaveIntoRegister(registers, line, number);
                }

                if (Regex.Match(line, "RSHIFT").Success)
                {
                    var matches = Regex.Matches(line, @"[a-z]+");
                    var number = registers[matches[0].Value] >> Int16.Parse(Regex.Match(line, @"\d+").Value);
                    SaveIntoRegister(registers, line, number);
                }

                if (Regex.Match(line, "NOT").Success)
                {
                    var matches = Regex.Matches(line, @"[a-z]+");
                    int number = ~registers[matches[0].Value];
                    SaveIntoRegister(registers, line, number);
                }
            }
            Console.WriteLine(registers["a"]);

            return 0;
        }

        private static void SaveIntoRegister(Dictionary<string, short> registers, string line, int number)
        {
            registers[Regex.Match(line, @"\w+$").Value] = (short)number;
        }


        public long Part2(string input)
        {
            return 0;
        }
    }
}
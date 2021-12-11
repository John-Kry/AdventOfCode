using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace AdventOfCode.Tasks.Year2021.Day08
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            var lines = input.Split("\n");
            var count = 0L;
            foreach (var line in lines)
            {
                var output = line.Split("|")[1].Trim().Split(" ");
                foreach (var characters in output)
                {
                    // Console.WriteLine(characters);
                    var digit = GetNumber(characters);
                    if (digit != 0) count++;
                }
            }

            return count;
        }

        private int GetNumber(string s)
        {
            if (s.Length == 2) return 1;
            if (s.Length == 4) return 4;
            if (s.Length == 3) return 7;
            if (s.Length == 7) return 8;
            return 0;
        }

        public long Part2(string input)
        {
            var lines = input.Split("\n");
            var sum = 0L;
            foreach (var line in lines)
            {
                var output = line.Split("|")[0].Trim().Split(" ");
                var outputAnswer = line.Split("|")[1].Trim().Split(" ");
                var digitToString = new Dictionary<int, List<char>>();
                var unsolvedScreens = new List<List<char>>();
                foreach (var characters in output)
                {
                    // Console.WriteLine(characters);
                    var digit = GetNumber(characters);
                    if (digit != 0)
                    {
                        digitToString.Add(digit, characters.ToList());
                    }
                    else
                    {
                        unsolvedScreens.Add(characters.ToList());
                    }
                }

                // unsolvedScreens
                var nine = unsolvedScreens.FindAll(chars =>
                    Enumerable.Intersect(chars, digitToString[4]).Count() == 4 && chars.Count() == 6);
                LogCount(nine);
                digitToString[9] = nine[0];
                unsolvedScreens.Remove(digitToString[9]);

                var two = unsolvedScreens.FindAll(chars =>
                    chars.Count() == 5 && Enumerable.Intersect(chars, digitToString[9]).Count() == 4);
                LogCount(two);
                digitToString[2] = two[0];
                unsolvedScreens.Remove((digitToString[2]));

                var zero = unsolvedScreens.FindAll(chars =>
                    chars.Count() == 6 && Enumerable.Intersect(chars, digitToString[7]).Count() == 3);
                LogCount(zero);
                digitToString[0] = zero[0];
                unsolvedScreens.Remove(digitToString[0]);

                var six = unsolvedScreens.FindAll(chars =>
                    chars.Count() == 6 && Enumerable.Intersect(chars, digitToString[8]).Count() == 6);
                LogCount(six);
                digitToString[6] = six[0];
                unsolvedScreens.Remove(digitToString[6]);

                var five = unsolvedScreens.FindAll(chars =>
                    chars.Count() == 5 && Enumerable.Intersect(chars, digitToString[9]).Count() == 5 &&
                    Enumerable.Intersect(chars, digitToString[8]).Count() == 5 &&
                    Enumerable.Intersect(chars, digitToString[1]).Count() == 1);
                digitToString[5] = five[0];
                LogCount(five);
                unsolvedScreens.Remove(digitToString[5]);

                digitToString[3] = unsolvedScreens[0];
                unsolvedScreens.Remove(digitToString[3]);
                foreach (var digit in digitToString)
                {
                    // Console.WriteLine($"{digit.Key}: {string.Join("", digit.Value)}");
                }

                foreach (var digit in unsolvedScreens)
                {
                    // Console.WriteLine(string.Join("",digit));
                }

                var outputString = "";
                foreach (var answer in outputAnswer)
                {
                    foreach (var kvp in digitToString)
                    {
                        if (kvp.Value.Count == answer.Length &&
                            kvp.Value.All(x => answer.Contains(x))
                           )
                        {
                            outputString = outputString + kvp.Key;
                            break;
                        }
                    }
                }

                // Console.WriteLine(outputString);
                sum += long.Parse(outputString);
            }

            return sum;
        }

        private static void LogCount(List<List<char>> two)
        {
            Console.WriteLine(two.Count());
            if (two.Count() != 1)
            {
                Console.WriteLine("WHAT");
            }
        }
    }
}
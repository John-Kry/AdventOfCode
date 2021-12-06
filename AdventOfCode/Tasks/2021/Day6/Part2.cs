using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks.Year2021.Day6
{
    public class Part2 : ITask<long>
    {
        public long Solution(string input)
        {
            var inputLines = input.Split(",").Select(int.Parse);
            var howManyAt = new Dictionary<int, long>();
            for (var i = 0; i < 9; i++)
            {
                howManyAt.TryAdd(i, 0);
            }
            foreach (var fish in inputLines)
            {
                howManyAt[fish]++;
            }

            for (var i = 0; i < 256; i++)
            {
                howManyAt.TryGetValue(0, out var temp); 
                for (var j = 0; j < 8; j++)
                {
                    howManyAt[j] = howManyAt[j + 1];
                }

                howManyAt[6] += temp;
                howManyAt[8] = temp;
            }

            long sum = 0;
            foreach (var kvp in howManyAt)
            {
                sum += kvp.Value;
            }

            return sum;
        }
    }
}
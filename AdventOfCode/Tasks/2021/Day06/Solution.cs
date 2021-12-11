using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day6
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            var inputLines = input.Split(",").Select(int.Parse);
            var _fishies = new List<int>();
            foreach (var fish  in inputLines)
            {
                _fishies.Add(fish);
            }
            for (var i = 0; i < 80; i++)
            {
                var count = _fishies.Count;
                for (var j = 0; j < count; j++)
                {
                    if (_fishies[j] == 0)
                    {
                        _fishies[j] = 6;
                        _fishies.Add(8);
                    }
                    else
                    {
                        _fishies[j] -= 1;
                    }
                }
            }
            
            return _fishies.Count;
        }

        public long Part2(string input)
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
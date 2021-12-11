using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks.Year2021.Day6
{
    public class Part1 : ITask<int>
    {
        public int Solution(string input)
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
    }

    public class LanternFish
    {
        public int state;
    }
}
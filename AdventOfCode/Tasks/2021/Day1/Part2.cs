using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day1
{
    public class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            var numbers = input.Split("\n").Select(int.Parse).ToList();
            var previous = numbers[0] + numbers[1] + numbers[2];
            var increasedCount = 0;
            var decreasedCount = 0;
            for(var i = 1; i <numbers.Count - 2; i++)
            {
                var sum = numbers[i] + numbers[i + 1] + numbers[i + 2];
                if (sum > previous)
                {
                    increasedCount++;
                }
                else
                {
                    decreasedCount++;
                }

                previous = sum;
            }


            return increasedCount;
        }
    }
}
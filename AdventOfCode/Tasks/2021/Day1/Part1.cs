using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day1
{
    public class Part1 : ITask<int>
    {
        public int Solution(string input)
        {
            var numbers = input.Split("\n").Select(int.Parse).ToList();
            var previous = numbers[0];
            var increasedCount = 0;
            var decreasedCount = 0;
            for(var i = 1; i <numbers.Count; i++)
            {
                if (numbers[i] > previous)
                {
                    increasedCount++;
                }
                else
                {
                    decreasedCount++;
                }

                previous = numbers[i];
            }


            return increasedCount;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day07
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            return Solve(input, false);
        }

        public long Part2(string input)
        {
            return Solve(input, true);
        }

        private long Solve(string input, bool increasePerStep)
        {
            var positions = GetPositions(input);

            var bestCost = long.MaxValue;
            for (var i = 0; i <= positions.Max(); i++)
            {
                bestCost = Math.Min(bestCost, TotalCost(i, increasePerStep, positions));
            }

            return bestCost;
        }

        private List<long> GetPositions(string input)
        {
            var positionsEnumerable = input.Split(",").Select(long.Parse);
            return positionsEnumerable.ToList();
        }

        private long TotalCost(long target, bool increasePerStep, List<long> positions)
        {
            var runningTotal = 0L;
            foreach (var position in positions)
            {
                if (increasePerStep)
                {
                    var increaseTarget = Math.Abs(target - position);
                    var cost = ((Math.Pow(increaseTarget, 2) + increaseTarget) / 2);
                    // (n^2 + n)
                    //  -------
                    //     2
                    // For the addition factorial
                    runningTotal += (long)cost;
                }
                else
                {
                    runningTotal += Math.Abs(target - position);
                }
            }

            return runningTotal;
        }
    }
}
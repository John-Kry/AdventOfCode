using System;
using System.Collections.Generic;

namespace AdventOfCode.Tasks.Year2021.Day3
{
    public class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            var bytes = input.Split("\n");
            var countOn = new Dictionary<int, int>();
            foreach (var by in bytes)
            {
                for (var i = 0; i < by.Length; i++)
                {
                    if (by[i] == '1')
                    {
                        if (countOn.ContainsKey(i))
                        {
                            int currentCount;
                            countOn.TryGetValue(i, out currentCount);
                            countOn[i] = currentCount + 1;
                        }
                        else
                        {
                            countOn.Add(i, 1);
                        }
                    }
                }
            }

            var gammaString = "";
            var epsilonString = "";
            for (var i = 0; i < bytes[0].Length; i++)
            {
                var count = countOn.GetValueOrDefault(i);
                if (count > bytes.Length - count)
                {
                    gammaString = gammaString + "1";
                    epsilonString = epsilonString + "0";
                }
                else
                {
                    gammaString = gammaString + "0";
                    epsilonString = epsilonString + "1";
                }
            }
            Console.WriteLine(gammaString);
            Console.WriteLine(epsilonString);
            var gamma = Convert.ToInt32(gammaString, 2);
            var epsilon = Convert.ToInt32(epsilonString, 2);
            return gamma * epsilon;
        }
    }
}
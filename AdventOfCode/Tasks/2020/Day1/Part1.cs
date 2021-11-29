using System;
using System.Collections.Generic;

namespace AdventOfCode.Tasks.Year2020.Day1
{
    public class Part1 : ITask<int>
    {
        public int Solution(string input)
        {
            var numbers = input.Split("\n");
            Console.WriteLine(numbers[0]);
            var compliments = new HashSet<int>();
            foreach (string number in numbers)
            {
                var actualNumber = Int32.Parse(number);
		var compliment = 2020 - actualNumber;
                if (compliments.Contains(actualNumber))
                {
                    return actualNumber * compliment;
                }
                compliments.Add(compliment);
            }
            return 10;
        }
    }
}

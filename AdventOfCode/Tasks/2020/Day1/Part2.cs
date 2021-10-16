using System;
using System.Collections.Generic;

namespace AdventOfCode.Tasks.Year2020.Day1
{
    public class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            var numbers = input.Split("\n");
            Console.WriteLine(numbers[0]);
            var compliments = new HashSet<int>();
            foreach (string number in numbers)
            {
		foreach (string number2 in numbers){
                var actualNumber1 = Int32.Parse(number);
                var actualNumber2 = Int32.Parse(number2);
		var compliment = 2020 - actualNumber1 - actualNumber2;		
                if (compliments.Contains(compliment))
                {
                    return actualNumber2 * actualNumber1 * compliment;
                }
		compliments.Add(actualNumber1);
		compliments.Add(actualNumber2);
		}
            }
            return 10;
        }
    }
}

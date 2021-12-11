using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day3
{
    public class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            var bytes = input.Split("\r\n");
            var oxygenRating = GetRating(bytes, Rating.Oxygen);
            var co2Rating = GetRating(bytes, Rating.CO2);
            Console.WriteLine(oxygenRating);
            Console.WriteLine(co2Rating);
            return oxygenRating * co2Rating;
        }

        private enum Rating
        {
            Oxygen,
            CO2
        }

        private static int GetRating(string[] bytes, Rating rating)
        {
            List<string> possibleBytes = new List<string>(bytes);
            var positionToSeek = 0;
            while (possibleBytes.Count > 1)
            {
                var countOn = 0;
                foreach (var by in possibleBytes)
                {
                    if (@by[positionToSeek] == '1')
                    {
                        countOn++;
                    }
                }

                bool keepOnes;
                if (countOn == possibleBytes.Count - countOn)
                {
                    keepOnes = true;
                }
                else if (countOn > possibleBytes.Count - countOn)
                {
                    keepOnes = true;
                }
                else
                {
                    keepOnes = false;
                }

                if (rating == Rating.CO2)
                {
                    keepOnes = !keepOnes;
                }

                var i = 0;
                var indexesToRemove = new List<int>();
                foreach (var by in possibleBytes)
                {
                    if (keepOnes && @by[positionToSeek] != '1')
                    {
                        indexesToRemove.Add(i);
                    }
                    else if (!keepOnes && @by[positionToSeek] != '0')
                    {
                        indexesToRemove.Add(i);
                    }

                    i++;
                }

                var indexesToRemove2 = indexesToRemove.OrderByDescending(i => i);
                foreach (var index in indexesToRemove2)
                {
                    possibleBytes.RemoveAt(index);
                }

                positionToSeek++;
            }

            return Convert.ToInt32(possibleBytes[0], 2);
        }
    }
}
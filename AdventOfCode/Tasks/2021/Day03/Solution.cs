using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day03
{
    public class Solution : Solvable
    {
        public long Part1(string input)
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

        public long Part2(string input)
        {
            var bytes = input.Split("\n");
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
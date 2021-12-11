using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day04
{
    public class Part1 : ITask<int>
    {
        public int Solution(string input)
        {
            var inputLines = input.Split("\n");
            var winningNumbers = inputLines[0].Split(",").Select(int.Parse);

            var cardsRaw = new List<string[]>();
            var currentCard = new string[5];
            var j = 0;
            for (var i = 2; i <= inputLines.Length; i++)
            {
                if (i == inputLines.Length || inputLines[i] == "")
                {
                    j = 0;
                    foreach (var item in currentCard)
                    {
                        Console.WriteLine(item.ToString());
                    }

                    Console.WriteLine("Next");
                    string[] currCard = new string[5];
                    currentCard.CopyTo(currCard, 0);
                    cardsRaw.Add(currCard);
                    continue;
                }

                currentCard[j] = inputLines[i];
                j++;
            }

            foreach (var cardsStringArray in cardsRaw)
            {
                foreach (var cardString in cardsStringArray)
                {
                    Console.WriteLine(cardString);

                }

            }

            var game = new Game();
            game.Initialize(cardsRaw);
            foreach (var winningNumber in winningNumbers)
            {
                var (isWin, answer) = game.MarkWinner(winningNumber);
                if (isWin)
                {
                    return answer;
                }
            }

            return 0;
        }
    }
}
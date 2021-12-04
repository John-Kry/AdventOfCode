using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day4
{
    public class Part2 : ITask<int>
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
                if (i==inputLines.Length || inputLines[i] == "")
                {
                    j = 0;
                    foreach(var item in currentCard)
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

    public class Point
    {
        public int x;
        public int y;
    }

    public class Game
    {
        private List<BingoCard> _bingoCards = new List<BingoCard>();


        public (bool isWin, int answer) MarkWinner(int winningNumber)
        {
            foreach (var card in _bingoCards)
            {
                var result = card.MarkSpaceAndCheckForWin(winningNumber);
                if (result)
                {
                    var sum = card.CalculateScore();
                    var answer = sum * winningNumber;
                    Console.WriteLine($"Sum: {sum}, WinningNumber: {winningNumber}, Answer: {answer}");
                    return (true, answer);
                }
            }

            return (false, 0);
        }

        private void PrintCards()
        {
            foreach (var bingoCard in _bingoCards)
            {
                bingoCard.PrintCard();
                Console.WriteLine("----------------");
            }
        }
        public void Initialize(List<string[]> rawCards)
        {
            foreach (var card in rawCards)
            {
                var y = 0;
                var currBingoCard = new BingoCard();
                foreach (var cardString in card)
                {
                    var numbers = cardString.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var x = 0;
                    foreach (var number in numbers)
                    {
                        Console.WriteLine(number);
                        currBingoCard.state[x, y] = new BingoSpace();
                        currBingoCard.state[x, y].number = int.Parse(number);
                        x++;
                    }

                    y++;
                } 
                _bingoCards.Add(currBingoCard);
            }
            this.PrintCards();
        }
    }


    public class BingoCard
    {
        public BingoSpace[,] state;
        public BingoCard()
        {
            state = new BingoSpace[5, 5];

        }

        public int CalculateScore()
        {
            var sum = 0;
            for(var y=0; y<5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (!state[x, y].isWin)
                    {
                        sum += state[x, y].number;
                    }
                }
            }

            return sum;
        }


        public bool CheckForWin()
        {
            for (var x = 0; x < 5; x++)
            {
                var winVertical = 0;
                for (var y = 0; y < 5; y++)
                {
                    if (state[x, y].isWin)
                    {
                        winVertical++;
                    }
                }
                if (winVertical == 5)
                {
                    return true;
                }
            }

            for (var y = 0; y < 5; y++)
            {
                var winHorizontal = 0;
                for (var x = 0; x < 5; x++)
                {
                    if (state[x, y].isWin)
                    {
                        winHorizontal++;
                    }
                }

                if (winHorizontal == 5)
                {
                    return true;
                }
            }

            return false;
        }
        //TODO only mark recent winning numbers
        public bool MarkSpaceAndCheckForWin(int winningNumber)
        {
            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (state[x, y].number == winningNumber)
                    {
                        state[x, y].isWin = true;
                    }
                }
            }

            return CheckForWin();
        }

        public void PrintCard()
        {
            for(var y=0; y<5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    
                    string currNumber = state[x, y].number.ToString();
                    Console.Write($"{currNumber.PadLeft(2)} ");
                }
                Console.Write("\n");
            }
        } 
    }
}
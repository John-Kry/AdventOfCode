using System;
using System.Collections.Generic;

namespace AdventOfCode.Tasks.Year2021.Day4
{
    public class BingoSpace
    {
        public int number { get; set; }
        public bool isWin { get; set; }
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
            for (var y = 0; y < 5; y++)
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
            for (var y = 0; y < 5; y++)
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
        public (bool isLast, int answer) MarkWinnerPart2(int winningNumber)
        {
            var cardsToRemove = new List<BingoCard>();
            foreach (var card in _bingoCards)
            {
                var isWin = card.MarkSpaceAndCheckForWin(winningNumber);
                if (isWin && _bingoCards.Count != 1)
                {
                    cardsToRemove.Add(card);
                }
                else if (isWin && _bingoCards.Count == 1)
                {
                    var sum = card.CalculateScore();
                    var answer = sum * winningNumber;
                    Console.WriteLine($"Sum: {sum}, WinningNumber: {winningNumber}, Answer: {answer}");
                    return (true, answer);
                }
            }

            foreach (var card in cardsToRemove)
            {
                _bingoCards.Remove(card);
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
}
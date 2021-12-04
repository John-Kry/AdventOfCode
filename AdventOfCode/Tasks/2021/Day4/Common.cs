using System;

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
}
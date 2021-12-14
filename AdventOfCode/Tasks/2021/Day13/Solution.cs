using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2021.Day13
{
    public class Solution : Solvable
    {
        private readonly int _size = 10000;

        public long Part1(string input)
        {
            return Solve(input, true);
        }

        private long Solve(string input, bool isPart1)
        {
            var lines = input.Split("\n\n")[0].Split("\n");
            var instructions = input.Split("\n\n")[1].Split("\n");

            var grid = new Point[_size, _size];
            var maxX = 0;
            var maxY = 0;
            foreach (var line in lines)
            {
                var inputs = line.Split(",").Select(int.Parse).ToList();
                maxX = Math.Max(maxX, inputs[0] + 1);
                maxY = Math.Max(maxY, inputs[1] + 1);
                if (maxX > _size || maxY > _size)
                {
                    throw new Exception("pog");
                }

                grid[inputs[0], inputs[1]] = new Point {isDot = true};
            }

            grid = ResizeArray(grid, maxX, maxY);
            foreach (var instructionString in instructions)
            {
                var flipOnX = 0;
                var flipOnY = 0;
                if (instructionString.Contains("x"))
                {
                    flipOnX = int.Parse(Regex.Match(instructionString, @"\d+$").Value);
                    // point at 7,5 in 10 grid
                    // flip left at  x=5
                    // x will be intital (x - x)
                    // 5 - (7 - 5)
                    grid.ForEachItem((x, y) =>
                    {
                        if (grid[x, y]?.isDot == true && x > flipOnX)
                        {
                            grid[flipOnX - (x - flipOnX), y] = new Point {isDot = true};
                        }
                    });
                    var newGridXLength = flipOnX + 1;
                    grid = ResizeArray(grid, newGridXLength, grid.GetLength(1));
                }
                else
                {
                    flipOnY = int.Parse(Regex.Match(instructionString, @"\d+$").Value);
                    grid.ForEachItem((x, y) =>
                    {
                        if (grid[x, y]?.isDot == true && y > flipOnY)
                        {
                            grid[x, flipOnY - (y - flipOnY)] = new Point {isDot = true};
                        }
                    });

                    var newGridYLength = flipOnY + 1;
                    grid = ResizeArray(grid, grid.GetLength(0), newGridYLength);
                }

                if (isPart1)
                    break;
            }

            var numberOfDots = 0;
            grid.ForEachItem((x, y) =>
            {
                if (grid[x, y]?.isDot == true)
                {
                    numberOfDots++;
                }
            });
            if (!isPart1)
            {
                // FAGURZHE
                grid.Print();
            }

            return numberOfDots;
        }

        private T[,] ResizeArray<T>(T[,] original, int xMax, int yMax)
        {
            var newArray = new T[xMax, yMax];
            int minRows = Math.Min(xMax, original.GetLength(0));
            int minCols = Math.Min(yMax, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
            for (int j = 0; j < minCols; j++)
                newArray[i, j] = original[i, j];
            return newArray;
        }

        class Point
        {
            public bool isDot;

            public override string ToString()
            {
                return isDot ? '\u2588'.ToString() : ".";
            }
        }


        public long Part2(string input)
        {
            Solve(input, false);
            return 0;
        }
    }
}
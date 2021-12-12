using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2021.Day11
{
    public class Solution : Solvable
    {
        public Solution()
        {
        }

        private int TotalFlashes;
        public long Part1(string input)
        {
            var lines = input.Split("\n");
            var grid = new Octopus[lines[0].Length, lines.Length];
            InitGrid(lines, grid);
            
            RunSteps(grid, false);

            return TotalFlashes;
        }

        private int RunSteps(Octopus[,] grid, bool findAllFlashStep)
        {
            var stepMax = 100;
            if (findAllFlashStep)
            {
                stepMax = int.MaxValue;
            } 
            for (var step = 0; step < stepMax; step++)
            {
                //step 1
                // for (var x = 0; x < grid.GetLength(0); x++)
                // {
                //     for (var y = 0; y < grid.GetLength(1); y++)
                //     {
                //         grid[x, y].Energy += 1;
                //     }
                // }
                grid.ForEachItem((x,y)=> grid[x,y].Energy +=1);

                grid.ForEachItem((x,y)=> ExecuteFlashIfNeeded(grid,x,y));
                //step 2
                // for (var x = 0; x < grid.GetLength(0); x++)
                // {
                //     for (var y = 0; y < grid.GetLength(1); y++)
                //     {
                //         ExecuteFlashIfNeeded(grid, x, y);
                //     }
                // }

                var allFlashed = true;
                // Clear for next round
                // for (var x = 0; x < grid.GetLength(0); x++)
                // {
                //     for (var y = 0; y < grid.GetLength(1); y++)
                //     {
                //         if (!grid[x, y].HasFlashed)
                //         {
                //             allFlashed = false;
                //         }
                //         grid[x, y].HasFlashed = false;
                //     }
                // }
                grid.ForEachItem((x, y) =>
                {
                    if (!grid[x, y].HasFlashed)
                    {
                        allFlashed = false;
                    }
                    grid[x, y].HasFlashed = false; 
                });

                if (findAllFlashStep && allFlashed) return step;
            }

            return 0;
        }

        private void ExecuteFlashIfNeeded(Octopus[,] grid, int x, int y)
        {
            if (grid[x, y].Energy > 9 && !grid[x, y].HasFlashed)
            {
                TotalFlashes++;
                grid[x, y].HasFlashed = true;
                grid[x, y].Energy = 0;
                for (var i = -1; i <= 1; i++)
                {
                    for (var j = -1; j <= 1; j++)
                    {
                        if (j == 0 && i == 0)
                        {
                            continue;
                        }

                        if (x + i >= grid.GetLength(0) || x + i < 0 || y + j < 0 || y + j >= grid.GetLength(1))
                        {
                            continue;
                        }
                        if(!grid[x+i, y+j].HasFlashed)
                            grid[x + i, y + j].Energy += 1;
                        ExecuteFlashIfNeeded(grid, x+i,y+j);
                    }
                }
            }
        }

        private class Octopus
        {
            public int Energy;
            public bool HasFlashed;
            public override string ToString()
            {
                return Energy.ToString();
            }
        }

        private static void InitGrid(string[] lines, Octopus[,] grid)
        {
            Helper.CreateTwoDArrayFromString(lines, (c, x, y) =>
            {
                return new Octopus {Energy = int.Parse(c.ToString()), HasFlashed = false};
            }, grid);
            // var y = 0;
            // foreach (var line in lines)
            // {
            //     var x = 0;
            //     foreach (var c in line)
            //     {
            //         grid[x, y] = new Octopus {Energy = int.Parse(c.ToString()), HasFlashed = false};
            //         x++;
            //     }
            //     y++;
            // }
        }

        public long Part2(string input)
        {
            var lines = input.Split("\n");
            var grid = new Octopus[lines[0].Length, lines.Length];
            InitGrid(lines, grid);
            
            var answer = RunSteps(grid, true);

            return answer +1;
        }
    }
}
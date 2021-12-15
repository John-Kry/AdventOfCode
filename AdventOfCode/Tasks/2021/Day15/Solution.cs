using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2021.Day15
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            return Solve(input, false);
        }

        public long Part2(string input)
        {
            return Solve(input, true);
        }

        public long Solve(string input, bool duplicateBy5)
        {
            var rr = new int[] {0, 1, 0, -1};
            var cc = new int[] {1, 0, -1, 0};
            var pathCost = new Dictionary<(int, int), long>();
            var queue = new Queue<(int, int)>();
            var grid = GetGrid(input, queue, pathCost, duplicateBy5);
            var source = (0, 0);
            var destination = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
            pathCost[source] = 0;
            while (queue.Count >0)
            {
                // var u = pathCost.Min().Key;
                var u = queue.Dequeue();
                for (var i = 0; i < rr.Length; i++)
                {
                    var (x, y) = u;
                    y += cc[i];
                    x += rr[i];
                    if (!queue.Contains((x, y)))
                    {
                        continue;
                    }

                    var localVertex = (x, y);
                    var alt = pathCost[u] + grid[x, y];
                    if (alt < pathCost[localVertex])
                    {
                        pathCost[localVertex] = alt;
                    }
                }
            }

            return pathCost[destination] - grid[source.Item1, source.Item2] + 1;
        }

        private long[,] GetGrid(string input, Queue<(int,int)> queue, Dictionary<(int, int), long> pathCost, bool duplicateBy5)
        {
           
            var lines = input.Split("\n");
            var gridStart = new long[lines[0].Length, lines.Length];
            
            Helper.CreateTwoDArrayFromString(lines, (c, x, y) => { return long.Parse(c.ToString()); }, gridStart);
            // gridStart.Print();
            long[,] grid;
            if (duplicateBy5)
            {
                grid = new long[lines[0].Length * 5, lines.Length * 5]; 
                var width = lines[0].Length;
                gridStart.ForEachItem((x, y) =>
                {
                    for (var xx = 0; xx < 5; xx++)
                    {
                        for (var yy = 0; yy < 5; yy++)
                        {
                            grid[x + width * xx, y + width * yy] = GetNewGridValue(gridStart, x, y, xx, yy);
                        }
                    }
                });
            }
            else
            {
                grid = gridStart;
            }
            
            grid.ForEachItem((x, y) =>
            {
                pathCost[(x, y)] = long.MaxValue;
                queue.Enqueue((x, y));
            });
            
            return grid;
        }

        private static long GetNewGridValue(long[,] grid, int x, int y, int xx, int yy)
        {
            return (grid[x, y] + xx + yy - 1) % 9 + 1;
        }
    }
}
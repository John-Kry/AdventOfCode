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
            return Solve(input);
        }

        private static long Solve(string input)
        {
            var lines = input.Split("\n");
            var grid = new long[lines[0].Length, lines.Length];
            var pathCost = new Dictionary<(int, int), long>();
            var prev = new Dictionary<(int, int), long>();
            var queue = new Dictionary<(int, int), bool>();
            Helper.CreateTwoDArrayFromString(lines, (c, x, y) => { return long.Parse(c.ToString()); }, grid);

            grid.ForEachItem((x, y) =>
            {
                pathCost[(x, y)] = long.MaxValue;
                // prev[(x, y)] = null;
                queue.TryAdd((x, y), true);
            });
            var source = (0, 0);
            var destination = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
            pathCost[source] = 1;
            while (queue.Any())
            {
                var u = queue.First().Key;
                // var u = ui.First().Key;
                // Console.WriteLine(u);
                if (u == destination)
                {
                    break;
                }

                queue.Remove(u);
                var rr = new int[] {0, 1, 0, -1};
                var cc = new int[] {1, 0, -1, 0};
                for (var i = 0; i < rr.Length; i++)
                {
                    var (x, y) = u;
                    y += cc[i];
                    x += rr[i];
                    if (!queue.GetValueOrDefault((x, y)))
                    {
                        continue;
                    }

                    var localVertex = (x, y);
                    var alt = pathCost[u] + grid[x, y];
                    if (alt < pathCost[localVertex])
                    {
                        pathCost[localVertex] = alt;
                        prev[localVertex] = grid[u.Item1, u.Item2];
                    }
                }
            }

            return pathCost[destination] - 1;
        }


        public long Part2(string input)
        {
            // return 0;
            var lines = input.Split("\n");
            var gridStart = new long[lines[0].Length, lines.Length];
            var pathCost = new Dictionary<(int, int), long>();
            var prev = new Dictionary<(int, int), long>();
            var queue = new Dictionary<(int, int), bool>();
            Helper.CreateTwoDArrayFromString(lines, (c, x, y) => { return long.Parse(c.ToString()); }, gridStart);
            // gridStart.Print();
            var grid = new long[lines[0].Length * 5, lines.Length * 5];
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

            // grid.Print();
            grid.ForEachItem((x, y) =>
            {
                pathCost[(x, y)] = long.MaxValue;
                prev[(x, y)] = 0;
                queue.TryAdd((x, y), true);
            });
            var source = (0, 0);
            var destination = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
            pathCost[source] = 1;
            while (queue.Any())
            {
                // var u = pathCost.Min().Key;
                var u = queue.OrderBy((pair => pathCost.GetValueOrDefault(pair.Key))).First().Key;

                if (u == destination)
                {
                    break;
                }

                queue.Remove(u);
                var rr = new int[] {0, 1, 0, -1};
                var cc = new int[] {1, 0, -1, 0};
                for (var i = 0; i < rr.Length; i++)
                {
                    var (x, y) = u;
                    y += cc[i];
                    x += rr[i];
                    if (!queue.GetValueOrDefault((x, y)))
                    {
                        continue;
                    }

                    var localVertex = (x, y);
                    var alt = pathCost[u] + grid[x, y];
                    if (alt < pathCost[localVertex])
                    {
                        pathCost[localVertex] = alt;
                        prev[localVertex] = grid[u.Item1, u.Item2];
                    }
                }
            }

            return pathCost[destination] - 1;
        }

        private static long GetNewGridValue(long[,] grid, int x, int y, int xx, int yy)
        {
            return (grid[x, y] + xx + yy - 1) % 9 + 1;
        }
    }
}
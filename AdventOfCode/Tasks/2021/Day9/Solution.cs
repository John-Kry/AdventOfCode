using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace AdventOfCode.Tasks.Year2021.Day9
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            var lines = input.Split("\n");
            var heightMap = new int[lines[0].Length, lines.Length];

            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var height in line)
                {
                    // Console.WriteLine($"x {x}, y {y}");
                    heightMap[x, y] = int.Parse(height.ToString());
                    x++;
                }

                y++;
            }

            var points = FindLowPoints(heightMap);
            return points.Sum();
        }

        private List<int> FindLowPoints(int[,] heightMap)
        {
            var lowPoints = new List<int>();
            for (var y = 0; y < heightMap.GetLength(1); y++)
            {
                for (var x = 0; x < heightMap.GetLength(0); x++)
                {
                    if (IsLowPoint(x, y, heightMap)) lowPoints.Add(heightMap[x, y]);
                }
            }

            return lowPoints.Select(point => point + 1).ToList();
        }

        private List<(int, int)> FindLowPointsPart2(Point[,] heightMap)
        {
            var lowPoints = new List<(int, int)>();
            for (var y = 0; y < heightMap.GetLength(1); y++)
            {
                for (var x = 0; x < heightMap.GetLength(0); x++)
                {
                    if (IsLowPointPart2(x, y, heightMap)) lowPoints.Add((x, y));
                }
            }

            return lowPoints;
        }

        private bool IsLowPoint(int x, int y, int[,] heightMap)
        {
            var target = heightMap[x, y];
            // for(var xDiff = -1)
            var points = new List<int>();

            //left
            points.Add(GetPoint(x - 1, y, heightMap));
            //right
            points.Add(GetPoint(x + 1, y, heightMap));
            //up
            points.Add(GetPoint(x, y - 1, heightMap));
            //down 
            points.Add(GetPoint(x, y + 1, heightMap));
            var count = points.Count(item => target < item);
            if (count == 4) return true;
            return false;
        }

        private int GetPoint(int x, int y, int[,] heightMap)
        {
            if (x > heightMap.GetUpperBound(0) || x < heightMap.GetLowerBound(0) ||
                y > heightMap.GetUpperBound(1) || y < heightMap.GetLowerBound(1))
            {
                return 10;
            }

            return heightMap[x, y];
        }
        private int GetPointPart2(int x, int y, Point[,] heightMap)
        {
            if (x > heightMap.GetUpperBound(0) || x < heightMap.GetLowerBound(0) ||
                y > heightMap.GetUpperBound(1) || y < heightMap.GetLowerBound(1))
            {
                return 10;
            }

            return heightMap[x, y].value;
        }

        private void Print(int[,] heightMap)
        {
            for (var y = 0; y < heightMap.GetLength(1); y++)
            {
                for (var x = 0; x < heightMap.GetLength(0); x++)
                {
                    Console.Write(heightMap[x, y]);
                }

                Console.WriteLine();
            }
        }


        public long Part2(string input)
        {
            {
                var lines = input.Split("\n");
                var heightMap = new Point[lines[0].Length, lines.Length];

                var y = 0;
                foreach (var line in lines)
                {
                    var x = 0;
                    foreach (var height in line)
                    {
                        // Console.WriteLine($"x {x}, y {y}");
                        heightMap[x, y] = new Point(int.Parse(height.ToString()));
                        x++;
                    }

                    y++;
                }

                var points = FindLowPointsPart2(heightMap);

                var basins = new List<int>();
                foreach (var point in points)
                {
                    basins.Add(FindBasinLength(point.Item1, point.Item2, heightMap, heightMap[point.Item1, point.Item2].value-1));
                }
                
                // return
                foreach (var VARIABLE in basins)
                {
                    Console.WriteLine(VARIABLE);
                }
                
                return basins.OrderByDescending(a=>a).Take(3).Aggregate(1, (a,b)=>a*b);
            }
        }

        private int FindBasinLength(int x, int y, Point[,] heightMap, int target)
        {
            if (x > heightMap.GetUpperBound(0) || x < heightMap.GetLowerBound(0) ||
                y > heightMap.GetUpperBound(1) || y < heightMap.GetLowerBound(1))
            {
                return 0;
            }

            if (heightMap[x, y].value == 9)
            {
                return 0;
            }

            if (heightMap[x, y].seen)
            {
                return 0;
            }

            if (heightMap[x, y].value <= target)
            {
                return 0;
            }

            if (heightMap[x, y].value > target)
            {
                heightMap[x, y].seen = true;
            }

            return FindBasinLength(x - 1, y, heightMap, heightMap[x, y].value) +
                   FindBasinLength(x + 1, y, heightMap, heightMap[x, y].value) +
                   FindBasinLength(x, y - 1, heightMap, heightMap[x, y].value) +
                   FindBasinLength(x, y + 1, heightMap, heightMap[x, y].value) + 1;
        }
        private bool IsLowPointPart2(int x, int y, Point[,] heightMap)
        {
            var target = heightMap[x, y];
            // for(var xDiff = -1)
            var points = new List<int>();

            //left
            points.Add(GetPointPart2(x - 1, y, heightMap));
            //right
            points.Add(GetPointPart2(x + 1, y, heightMap));
            //up
            points.Add(GetPointPart2(x, y - 1, heightMap));
            //down 
            points.Add(GetPointPart2(x, y + 1, heightMap));
            var count = points.Count(item => target.value < item);
            if (count == 4) return true;
            return false;
        }

    }

    class Point
    {
        public bool seen;
        public int value;

        public Point(int value)
        {
            this.value = value;
        }
    }
}
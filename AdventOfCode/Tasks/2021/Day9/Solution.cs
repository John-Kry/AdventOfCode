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
            var heightMap = GetHeightMap(lines);

            var points = FindLowPoints(heightMap);
            return points.Select(point =>point.value).Sum();
        }

        private static Point[,] GetHeightMap(string[] lines)
        {
            var heightMap = new Point[lines[0].Length, lines.Length];

            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var height in line)
                {
                    heightMap[x, y] = new Point
                    {
                        seen = false,
                        value = int.Parse(height.ToString()),
                        x = x,
                        y = y
                    };
                    x++;
                }

                y++;
            }

            return heightMap;
        }


        private List<Point> FindLowPoints(Point[,] heightMap)
        {
            var lowPoints = new List<Point>();
            for (var y = 0; y < heightMap.GetLength(1); y++)
            {
                for (var x = 0; x < heightMap.GetLength(0); x++)
                {
                    var lowPoint = GetLowPoint(x, y, heightMap);
                    if (lowPoint != null)
                    {
                        lowPoints.Add(lowPoint);
                    }
                }
            }

            return lowPoints;
        }
        private int GetPoint(int x, int y, Point[,] heightMap)
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
                var heightMap = GetHeightMap(lines);
                
                var points = FindLowPoints(heightMap);

                var basins = new List<int>();
                foreach (var point in points)
                {
                    basins.Add(FindBasinLength(point.x, point.y, heightMap, heightMap[point.x, point.y].value-1));
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
        private Point GetLowPoint(int x, int y, Point[,] heightMap)
        {
            var target = heightMap[x, y];
            var points = new List<int>();

            //left
            points.Add(GetPoint(x - 1, y, heightMap));
            //right
            points.Add(GetPoint(x + 1, y, heightMap));
            //up
            points.Add(GetPoint(x, y - 1, heightMap));
            //down 
            points.Add(GetPoint(x, y + 1, heightMap));
            var count = points.Count(item => target.value < item);
            if (count == 4) return heightMap[x,y];
            return null;
        }

    }

    class Point
    {
        public bool seen;
        public int value;
        public int x;
        public int y;
    }
}
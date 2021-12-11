using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day9
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            var lines = input.Split("\n");
            var heightMap = GetHeightMap(lines);

            var points = FindLowPoints(heightMap);
            return points.Select(point => point.value + 1).Sum();
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
                    basins.Add(FindBasinLength(point.x, point.y, heightMap));
                }

                return basins.OrderByDescending(a => a).Take(3).Aggregate(1, (a, b) => a * b);
            }
        }

        private int FindBasinLength(int x, int y, Point[,] heightMap)
        {
            var size = 0;
            var queue = new Queue<Point>();

            queue.Enqueue(heightMap[x, y]);

            while (queue.Count > 0)
            {
                var currPoint = queue.Dequeue();
                if (currPoint.seen)
                {
                    continue;
                }

                currPoint.seen = true;
                size += 1;
                var dirRow = new int[] {-1, 0, 1, 0};
                var dirCol = new int[] {0, 1, 0, -1};
                foreach (int val in Enumerable.Range(0, 4))
                {
                    var tempX = dirCol[val] + currPoint.x;
                    var tempY = dirRow[val] + currPoint.y;
                    if (tempX > heightMap.GetUpperBound(0) || tempX < heightMap.GetLowerBound(0) ||
                        tempY > heightMap.GetUpperBound(1) || tempY < heightMap.GetLowerBound(1))
                    {
                        continue;
                    }
                    if (heightMap[tempX, tempY].value != 9)
                    {
                        queue.Enqueue(heightMap[tempX, tempY]);
                    }
                }
            }

            return size;
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
                if (count == 4) return heightMap[x, y];
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
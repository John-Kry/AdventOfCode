using System;
using AdventOfCode.Tasks.Year2021.Day11;

namespace AdventOfCode.Tasks._2021
{
    public static class Helper
    {
        public static void Print<T>(this T[,] heightMap)
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

        public static void ForEachItem<T>(this T[,] heightMap, Action<int, int> function)
        {
            for (var x = 0; x < heightMap.GetLength(0); x++)
            {
                for (var y = 0; y < heightMap.GetLength(1); y++)
                {
                    function(x,y);
                }
            }
        }
    }
}
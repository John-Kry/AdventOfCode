using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Helpers;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var year = 2021;
            var day = 04;
            var part = 2;
            Type taskType = Type.GetType($"AdventOfCode.Tasks.Year{year}.Day{day}.Part{part}");
            if (taskType is null)
            {
                Console.WriteLine($"Task with year: {year}, day: {day} and part: {part} doesn't exist.");
                return;
            }

            string input = ReadInputHelper.ReadTaskInput(year, day);
            object task = Activator.CreateInstance(taskType);
            MethodInfo solutionMethod = taskType.GetMethod("Solution");

            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = solutionMethod.Invoke(task, new object[] { input });
            stopwatch.Stop();

            Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} miliseconds");
            Console.WriteLine($"Result: {result}");
        }
    }
}

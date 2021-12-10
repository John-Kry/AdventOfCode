using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Helpers;

namespace AdventOfCode
{
    class Program
    {
        private static int _year = 2021;
        private static int _day = 10;

        static void Main(string[] args)
        {
            // var _day = DateTime.Now._Day;
            if (ReadInputHelper.FileExists(_year, _day, "Solution.cs"))
            {
                // new run part
                RunNewParts();
            }

            else
            {
                RunPart(_year, _day, 1);
                RunPart(_year, _day, 2);
            }
        }

        private static void RunNewParts()
        {
            Type taskType = Type.GetType($"AdventOfCode.Tasks.Year{_year}.Day{_day}.Solution");
            ExecutePart(taskType, true);
            ExecutePart(taskType, false);
        }

        private static void ExecutePart(Type taskType, bool isExample)
        {
            string fileName = isExample ? "example-input.txt" : "input.txt";
            string input = ReadInputHelper.ReadTaskInput(_year, _day, fileName);
            object task = Activator.CreateInstance(taskType);
            MethodInfo solutionMethod1 = taskType.GetMethod("Part1");
            MethodInfo solutionMethod2 = taskType.GetMethod("Part2");

            RunAndLogPart(isExample, solutionMethod1, task, input, fileName);
            RunAndLogPart(isExample, solutionMethod2, task, input, fileName);
        }

        private static void RunAndLogPart(bool isExample, MethodInfo solutionMethod, object task, string input, string fileName)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = solutionMethod.Invoke(task, new object[] {input});
            stopwatch.Stop();
            if (isExample)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($"Filename: {fileName}");
            Console.WriteLine($"{solutionMethod.Name}, Result: {result}");
            Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("------------------------------------");
        }


        private static void RunPart(int _year, int _day, int part)
        {
            Type taskType = Type.GetType($"AdventOfCode.Tasks._Year{_year}._Day{_day}.Part{part}");
            if (taskType is null)
            {
                Console.WriteLine($"Task with _year: {_year}, _day: {_day} and part: {part} doesn't exist.");
                return;
            }

            var exampleFilename = "example-input.txt";
            if (ReadInputHelper.FileExists(_year, _day, exampleFilename))
            {
                ReadInput(part, taskType, exampleFilename);
            }

            var filename = "input";
            if (ReadInputHelper.FileExists(_year, _day, filename))
            {
                ReadInput(part, taskType, filename);
            }
        }

        private static void ReadInput(int part, Type taskType, string fileName)
        {
            string input = ReadInputHelper.ReadTaskInput(_year, _day, fileName);
            object task = Activator.CreateInstance(taskType);
            MethodInfo solutionMethod = taskType.GetMethod("Solution");

            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = solutionMethod.Invoke(task, new object[] {input});
            stopwatch.Stop();
            if (fileName == "example-input")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($"Filename: {fileName}");
            Console.WriteLine($"Part {part}, Result: {result}");
            Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("------------------------------------");
        }
    }
}
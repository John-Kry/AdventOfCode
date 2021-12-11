using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AdventOfCode.Helpers;

namespace AdventOfCode
{
    class Program
    {
        private static string _year = "2021";
        private static string _day = "11";
        private static bool runExample = false;
        private static bool runInput = false;
        static void Main(string[] args)
        {
            if (args.Contains("example"))
            {
                runExample = true;
            }

            if (args.Contains("input"))
            {
                runInput = true;
            }

            if (args.Contains("all"))
            {
                for (var i = 0; i < 12; i++)
                {
                    _day = i.ToString("00");
                    ExecuteDay();
                }
            }
            else
            {
                _day = DateTime.Now.Day.ToString("00");
                ExecuteDay();
            }
        }

        private static void ExecuteDay()
        {
            if (ReadInputHelper.FileExists(_year, _day, "Solution.cs"))
            {
                // new run part
                RunNewParts();
            }

            else
            {
                RunPart(1);
                RunPart(2);
            }
        }

        private static void RunNewParts()
        {
            Type taskType = Type.GetType($"AdventOfCode.Tasks.Year{_year}.Day{_day}.Solution");
            if (runExample)
            {
                ExecutePart(taskType, true);
            }
            if (runInput)
            {
                ExecutePart(taskType, false);
            }
        }

        private static void ExecutePart(Type taskType, bool isExample)
        {
            string fileName = isExample ? "example-input.txt" : "input.txt";
            if (!ReadInputHelper.FileExists(_year, _day, fileName))
            {
                return;
            }

            string input = ReadInputHelper.ReadTaskInput(_year, _day, fileName);
            object task = Activator.CreateInstance(taskType);
            MethodInfo solutionMethod1 = taskType.GetMethod("Part1");
            MethodInfo solutionMethod2 = taskType.GetMethod("Part2");

            RunAndLogPart(isExample, solutionMethod1, task, input, fileName);
            RunAndLogPart(isExample, solutionMethod2, task, input, fileName);
        }

        private static void RunAndLogPart(bool isExample, MethodInfo solutionMethod, object task, string input,
            string fileName)
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


        private static void RunPart(int part)
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
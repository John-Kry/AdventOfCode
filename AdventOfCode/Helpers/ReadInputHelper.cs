using System;
using System.IO;

namespace AdventOfCode.Helpers
{
    static class ReadInputHelper
    {
        public static string ReadTaskInput(int year, int day)
        {
            string baseDirectory = AppContext.BaseDirectory;

            int index = baseDirectory.IndexOf("bin");

            string projectRoot = baseDirectory;
            // If bin directory index is found
            if (index >= 0)
            {
                projectRoot = baseDirectory.Substring(0, index);
            }
            string input = File.ReadAllText(Path.Combine(projectRoot, $"Tasks/{year}/Day{day}/input.txt"));
            input = input.TrimEnd();
            return input;
        }   
    }
}

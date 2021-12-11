using System;
using System.IO;

namespace AdventOfCode.Helpers
{
    static class ReadInputHelper
    {
        public static string ReadTaskInput(string year, string day, string fileName)
        {
            var projectRoot = GetProjectRoot();

            string input = File.ReadAllText(Path.Combine(projectRoot, $"Tasks/{year}/Day{day}/{fileName}"));
            input = input.TrimEnd();
            return input;
        }

        public static bool FileExists(string year, string day, string fileName)
        {
           return File.Exists(Path.Combine(GetProjectRoot(), $"Tasks/{year}/Day{day}/{fileName}"));
        }
        private static string GetProjectRoot()
        {
            string baseDirectory = AppContext.BaseDirectory;

            int index = baseDirectory.IndexOf("bin");

            string projectRoot = baseDirectory;
            // If bin directory index is found
            if (index >= 0)
            {
                projectRoot = baseDirectory.Substring(0, index);
            }

            return projectRoot;
        }
    }
}

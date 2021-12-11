using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2021.Day02
{
    public class Part1 : ITask<int>
    {
        public int Solution(string input)
        {
            var commands = input.Split("\n");

            var x = 0;
            var depth = 0;

            foreach (var command in commands)
            {
                var commandSplit = command.Split(" ");
                var direction = commandSplit[0];
                var amount = int.Parse(commandSplit[1]);


                switch (direction)
                {
                    case "forward":
                        x += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    default:
                        throw new NotImplementedException();
                }

            }
            return x * depth;
        }
    }
}
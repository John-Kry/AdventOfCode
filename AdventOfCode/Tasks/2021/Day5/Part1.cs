using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks.Year2021.Day5
{
    public class Part1 : ITask<int>
    {
        Dictionary<(int,int),int> _positions = new Dictionary<(int,int), int>();
        private int _maxX = 0;
        private int _maxY = 0;
        public int Solution(string input)
        {
            var inputLines = input.Split("\n");
            var lines = new List<Line>();
            Regex rx = new Regex(@"\d+",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var line in inputLines)
            {
                MatchCollection matches = rx.Matches(line);
                lines.Add(new Line
                {
                    startPosition = new Position
                    {
                        x = Convert.ToInt32(matches[0].Value),
                        y = Convert.ToInt32(matches[1].Value)
                    },
                    endPosition = new Position
                    {
                        x = Convert.ToInt32(matches[2].Value),
                        y = Convert.ToInt32(matches[3].Value)
                    }
                });
                var lastLine = lines.Last();
                if (lastLine.endPosition.x > _maxX)
                {
                    _maxX = lastLine.endPosition.x;
                }
                if (lastLine.startPosition.x > _maxX)
                {
                    _maxX = lastLine.startPosition.x;
                }
                if (lastLine.startPosition.y > _maxY)
                {
                    _maxY = lastLine.startPosition.y;
                }
                if (lastLine.endPosition.y > _maxY)
                {
                    _maxY = lastLine.endPosition.y;
                }
                Console.WriteLine(lines.Last());
            }

            foreach (var line in lines)
            {
                if (line.startPosition.x != line.endPosition.x && line.startPosition.y != line.endPosition.y)
                {
                    continue;
                }
                // horizontal
                if (line.startPosition.x == line.endPosition.x)
                {
                    var start = Math.Min(line.endPosition.y, line.startPosition.y);
                    for (var i =0 ; i <= Math.Abs(line.endPosition.y - line.startPosition.y); i++)
                    {
                        IncrementPosition(new Position {x = line.startPosition.x, y = start +i});
                    }
                }
                // vertical
                if (line.startPosition.y == line.endPosition.y)
                {
                    var start = Math.Min(line.endPosition.x, line.startPosition.x);
                    for (var i = 0; i <= Math.Abs(line.endPosition.x -line.startPosition.x); i++)
                    {
                        IncrementPosition(new Position {x = start + i, y = line.startPosition.y});
                    } 
                }
            }
           
            PrintPositions();

            return CountDangerousAreas();
        }

        private void PrintPositions()
        {
            for (var y = 0; y <= _maxY; y++)
            {
                for (var x = 0; x <= _maxX; x++)
                {
                    int number;
                    _positions.TryGetValue((x, y), out number);
                    Console.Write(number);
                }
                Console.Write("\n");
            }
        }

        private int CountDangerousAreas()
        {
            var count = 0;
            foreach (var kvp in _positions)
            {
                if (kvp.Value >= 2)
                {
                    count++;
                }
            }

            return count;
        }

        private void IncrementPosition(Position position)
        {
            if (_positions.ContainsKey(position.toTuple()))
            {
                _positions[position.toTuple()] += 1;
            }
            else
            {
                _positions[position.toTuple()] = 1;
            }
        }
        class Position
        {
            public int x;
            public int y;

            public (int, int) toTuple()
            {
                return (x, y);
            }
        }

        class Line
        {
            public Position startPosition;
            public Position endPosition;

            public override string ToString()
            {
                return $"{startPosition.x},{startPosition.y}->{endPosition.x},{endPosition.y}";
            }
        }
    }
}
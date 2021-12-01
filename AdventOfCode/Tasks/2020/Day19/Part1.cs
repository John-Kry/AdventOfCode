using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Tasks.Year2020.Day19
{
    public partial class Part1 : ITask<long>
    {
        private string postfix;
        public long Solution(string solInput)
        {
            var sections =  solInput.Split("\r\n\r\n");
            var possibilites = sections[1];
            var messages = new List<string>();
            var inputs     = sections[0].Split("\r\n");
            foreach (string input in inputs)
            {
                string[] bits = input.Split(new char[] { ':', '|' });
                if (bits.Length == 1)
                {
                    messages.Add(bits[0]);
                }
                else
                {
                    int ruleNumber = int.Parse(bits[0]);
                    string rule1 = bits[1];
                    if (bits.Length == 2)
                    {
                        if (!rule1.Any(char.IsDigit))
                        {
                            
                        }
                    }
                }
            }
            List<(int, List<string>)> unresolved = new List<(int, List<string>)>();
            Queue<(int, List<string>)> resolved = new Queue<(int, List<string>)>();
            

            return 0;
        }
        private int Precedent(char c){
            if (c == '+' || c == '*')
            {
                return 1;
            }

            return 0;
        }
    }
}

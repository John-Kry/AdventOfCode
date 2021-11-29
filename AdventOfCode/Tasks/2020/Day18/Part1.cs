using System;
using System.Collections.Generic;
namespace AdventOfCode.Tasks.Year2020.Day18
{
    public class Part1 : ITask<long>
    {
        private Stack<char> stack = new Stack<char>();
        private string postfix;
        public long Solution(string input)
        {
            var equations = input.Split("\n");
            Console.WriteLine(equations[0]);
            var sum = 0L;
            foreach (var equation in equations){
                var value = Evaluate(equation.Replace(" ", string.Empty));
                Console.WriteLine(value);
                sum += value;
            }
            return sum;
        }
        private long Evaluate(string equation){
            foreach(char c in equation){
                if(char.IsDigit(c)){
                    postfix+=c;
                } else if (c == '('){
                    stack.Push('(');
                } else if (c == ')'){
                    while(stack.Count > 0 && stack.Peek() != '('){
                        postfix+= stack.Pop();
                    }
                    stack.TryPop(out _);
                } else {
                    while(stack.Count > 0 && Precedent(c) <= Precedent(stack.Peek())){
                        postfix += stack.Pop();
                    }
                    stack.Push(c);
                }
            }
            while(stack.Count >0){
                postfix+=stack.Pop();
            }
            Console.WriteLine(postfix);
            Stack<long> expressionStack = new Stack<long>();
            foreach (char c in postfix){
                if(char.IsDigit(c)){
                    expressionStack.Push((long) char.GetNumericValue(c));
                }
                else {
                    var a = expressionStack.Pop();
                    var b = expressionStack.Pop();
                    if(c == '+'){
                        expressionStack.Push(a+b);
                    }
                    if(c == '*'){
                        expressionStack.Push(a * b);
                    }
                }
            }
            // 26
            return expressionStack.Pop();
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

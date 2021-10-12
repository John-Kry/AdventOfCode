using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tasks
{ 
    public interface ITask<T>
    {
        T Solution(string input);
    }
}

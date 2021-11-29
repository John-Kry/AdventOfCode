using System;
namespace AdventOfCode.Tasks.Year2020.Day17
{
    public class Position
    {
        public Position(int x, int y, int z, int w){
           X=x;
           Y=y;
           Z=z;
           W=w;
        }
        public Position(int x, int y, int z){
           X=x;
           Y=y;
           Z=z;
        }
     public int X {get; set;}
     public   int Y {get; set;}
        public int Z {get; set;}
     public int W {get; set;}
    }
}

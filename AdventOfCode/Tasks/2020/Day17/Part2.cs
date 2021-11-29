using System;
namespace AdventOfCode.Tasks.Year2020.Day17
{
    public class Part2 : ITask<int>
    {
        const int size = 26;
        const int start = 13;
        const int iterations = 6;
        public int Solution(string input)
        {
            var world = new bool[size,size, size, size];
            PopulateInitialWorld(world, input);
            for(int i =1; i<= iterations; i++){
                Console.WriteLine($"Iteration: {i}");
                world = Simulate(world);
            }
            return CountActive(world);
        }
        private int CountActive(bool[,,,] world){
            var count = 0;
            foreach(bool isActive in world){
                if(isActive)
                    count++;
            }
            return count;
        }
        private void PopulateInitialWorld(bool[,,,] world, string input){
            var numbers = input.Split("\n");
            var x = 0;
            foreach (string line in numbers){
                var y = 0;
                foreach(char c in line){
                    world[x +start,y+start,0+start, 0+start] =  GetAsBool(c);
                    y++;
                }
                x++;
            }
        }
        private bool[,,,] Simulate(bool[,,,] world){
            var newWorld = new bool[size, size, size, size];
            for(int x=0; x < size; x++){
                for(int y=0; y< size; y++){
                    for(int z=0; z<size; z++){
                        for(int w=0; w<size; w++){
                            var isActive = world[x,y,z,w];
                            newWorld[x,y,z,w] = isActive;
                            var countActiveNearby = NumberOfActiveNeighbors(world,x,y,z,w);
                            if(isActive){
                                if( countActiveNearby == 2 || countActiveNearby == 3){
                                    // do nothing
                                }else{
                                    newWorld[x,y,z,w] = false;
                                }
                            }else{
                                if(countActiveNearby ==3){
                                    newWorld[x,y,z,w] = true;
                                }
                            }
                        }
                    }
                }
            }
            return newWorld;
        }
        private int NumberOfActiveNeighbors(bool[,,,] world, int x, int y, int z, int w){
            var count = 0;
            for(int dx = -1; dx <=1; dx++){
                for(int dy = -1; dy <=1; dy++){
                    for(int dz = -1; dz <=1; dz++){
                        for(int dw = -1; dw <=1; dw++){
                        if(dx == 0 && dy == 0 && dz == 0 && dw == 0){
                            continue;
                        }
                        var pos = new Position(x + dx, y + dy, z +dz, w + dw);
                        if((pos.X <0 || pos.X>=size ) || (pos.Y <0 || pos.Y>=size) || (pos.Z<0 || pos.Z>=size)  || (pos.W<0 || pos.W>=size)){
                            continue;
                            }
                            if(world[pos.X,pos.Y,pos.Z, pos.W]){
                                count++;
                       } 
                        }
                    }
                }
            }
            return count;
        }
        private bool GetAsBool(char c){
            return c == '#';
        }
        private void PrintWorld(bool[,,] world){
            foreach (var value in world){
                Console.WriteLine(GetAsChar(value));
            }
        }
        private char GetAsChar(bool b){
            if(b)
                return '#';
            return '.';
        }
    }
}

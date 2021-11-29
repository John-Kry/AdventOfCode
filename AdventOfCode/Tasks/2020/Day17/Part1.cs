using System;
namespace AdventOfCode.Tasks.Year2020.Day17
{
    public class Part1 : ITask<int>
    {
        const int size = 40;
        const int start = 20;
        const int iterations = 6;
        public int Solution(string input)
        {
            var world = new bool[size,size, size];
            PopulateInitialWorld(world, input);
            for(int i =1; i<= iterations; i++){
                world = Simulate(world);
            }
            return CountActive(world);
        }
        private int CountActive(bool[,,] world){
            var count = 0;
            foreach(bool isActive in world){
                if(isActive)
                    count++;
            }
            return count;
        }
        private void PopulateInitialWorld(bool[,,] world, string input){
            var numbers = input.Split("\n");
            var x = 0;
            foreach (string line in numbers){
                var y = 0;
                foreach(char c in line){
                    world[x +start,y+start,0+start] =  GetAsBool(c);
                    y++;
                }
                x++;
            }
        }
        private bool[,,] Simulate(bool[,,] world){
            var newWorld = new bool[size, size, size];
            for(int x=0; x < size; x++){
                for(int y=0; y< size; y++){
                    for(int z=0; z<size; z++){
                        var isActive = world[x,y,z]; 
                        newWorld[x,y,z] = isActive;
                        var countActiveNearby = NumberOfActiveNeighbors(world,x,y,z);
                        if(isActive){
                            if(countActiveNearby == 2 || countActiveNearby == 3){
                                // do nothing
                            }else{
                                newWorld[x,y,z] = false;
                            }
                        }else{
                            if(countActiveNearby ==3){
                                newWorld[x,y,z] = true;
                            }
                        }
                    }
                }
            }
            return newWorld;
        }
        private int NumberOfActiveNeighbors(bool[,,] world, int x, int y, int z){
            var count = 0;
            for(int dx = -1; dx <=1; dx++){
                for(int dy = -1; dy <=1; dy++){
                    for(int dz = -1; dz <=1; dz++){
                        if(dx == 0 && dy == 0 && dz == 0){
                            continue;
                        }
                        var pos = new Position(x + dx, y + dy, z +dz);
                        if((pos.X <0 || pos.X>=size ) || (pos.Y <0 || pos.Y>=size) || (pos.Z<0 || pos.Z>=size)){
                            continue;
                            }
                            if(world[pos.X,pos.Y,pos.Z]){
                                count++;
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

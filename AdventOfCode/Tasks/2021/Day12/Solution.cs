using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tasks._2021;

namespace AdventOfCode.Tasks.Year2021.Day12
{
    public class Solution : Solvable
    {
        public long Part1(string input)
        {
            return 0;
            var lines = input.Split("\n");
            var graph = new Graph();
            foreach (var line in lines)
            {
                var vertexes = line.Split("-");

                graph.AddVertex(
                    new Vertex
                    {
                        _name = vertexes[0],
                        _isSmallCave = vertexes[0].All(c => !char.IsUpper(c)),
                    });
                graph.AddVertex(
                    new Vertex
                    {
                        _name = vertexes[1],
                        _isSmallCave = vertexes[1].All(c => !char.IsUpper(c)),
                    });

                graph.AddAnEdge(vertexes[0], vertexes[1]);
            }

            graph.DFS("start", "end", new List<string>(), new Dictionary<string, bool>());
            
            foreach (var item in graph.paths)
            {
                foreach (var d in item)
                {
                    Console.Write($"{d}-");
                }
                Console.Write("\n");
            }
            return graph.paths.Count();
        }



        public long Part2(string input)
        {
            var lines = input.Split("\n");
            var graph = new Graph();
            foreach (var line in lines)
            {
                var vertexes = line.Split("-");

                graph.AddVertex(
                    new Vertex
                    {
                        _name = vertexes[0],
                        _isSmallCave = vertexes[0].All(c => !char.IsUpper(c)),
                    });
                graph.AddVertex(
                    new Vertex
                    {
                        _name = vertexes[1],
                        _isSmallCave = vertexes[1].All(c => !char.IsUpper(c)),
                    });

                graph.AddAnEdge(vertexes[0], vertexes[1]);
            }

            graph.DFS("start", "end", new List<string>(), new Dictionary<string, bool>());
            
            foreach (var item in graph.paths)
            {
                foreach (var d in item)
                {
                    Console.Write($"{d}-");
                }
                Console.Write("\n");
            }
            return graph.paths.Count();
        }
    }

    class Vertex
    {
        public bool _isSmallCave;
        public bool _IsVisited;
        public string _name;
        public List<string> _edges = new List<string>();
    }

    class Graph
    {
        private List<Vertex> _adjList = new List<Vertex>();

        public bool AddVertex(Vertex newVertex)
        {
            if (_adjList.Find((vertex) => vertex._name == newVertex._name) != null)
            {
                return true;
            }
            
            _adjList.Add(newVertex);
            return true;
        }

        public int GetNumberOfVertex()
        {
            return _adjList.Count();
        }

        public Vertex GetVertexByName(string name)
        {
            return _adjList.Find(vertex => vertex._name == name);
        }

        public bool AddAnEdge(string vertex1, string vertex2)
        {
            Console.WriteLine($"{vertex1}-{vertex2}");
            _adjList.Find(e=>e._name ==vertex1)!._edges.Add(vertex2);
            _adjList.Find(e=>e._name ==vertex2)!._edges.Add(vertex1);
            return true;
        }

        public List<List<string>> paths = new List<List<string>>();
        public void DFS(string startVertexName, string endVertexName, List<string> pathSoFar,
         Dictionary<string, bool> visited)
        {
            var currentVertex = GetVertexByName(startVertexName);
            pathSoFar.Add(currentVertex._name);
            
            var localVisited = new Dictionary<string, bool>(visited);
            if (currentVertex._name == endVertexName)
            {
                paths.Add(pathSoFar);
            }
            else
            {
                localVisited.TryGetValue(currentVertex._name, out var isVisited);
                if (!currentVertex._isSmallCave || (
                        currentVertex._isSmallCave && !isVisited))
                {
                    localVisited[currentVertex._name] = true;
                    foreach (var vertex in currentVertex._edges)
                    {
                        DFS(vertex, endVertexName, new List<string>(pathSoFar),
                            new Dictionary<string, bool>(localVisited));
                    }
                }
            }
        } 
    }
}
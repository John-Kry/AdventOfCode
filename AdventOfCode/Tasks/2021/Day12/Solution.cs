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
            var lines = input.Split("\n");
            var graph = new Graph();
            InitializeGraph(lines, graph);

            graph.DFS("start", "end", new List<string>(), new Dictionary<string, int>(),"NOTHING");
            
            return graph.paths.Count();
        }

        private static void InitializeGraph(string[] lines, Graph graph)
        {
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
        }


        public long Part2(string input)
        {
            var lines = input.Split("\n");
            var graph = new Graph();
            InitializeGraph(lines, graph);

            foreach (var vertex in graph._adjList)
            {
                if (vertex._name == "start" || vertex._name == "end")
                {
                    continue;
                }
                graph.DFS("start", "end", 
                    new List<string>(),
                    new Dictionary<string, int>(), vertex._name);
            }

            return graph.GetUniquePaths();
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
        public List<Vertex> _adjList = new List<Vertex>();

        public bool AddVertex(Vertex newVertex)
        {
            if (_adjList.Find((vertex) => vertex._name == newVertex._name) != null)
            {
                return true;
            }
            
            _adjList.Add(newVertex);
            return true;
        }

        public Vertex GetVertexByName(string name)
        {
            return _adjList.Find(vertex => vertex._name == name);
        }

        public bool AddAnEdge(string vertex1, string vertex2)
        {
            _adjList.Find(e=>e._name ==vertex1)!._edges.Add(vertex2);
            _adjList.Find(e=>e._name ==vertex2)!._edges.Add(vertex1);
            return true;
        }

        public List<List<string>> paths = new List<List<string>>();

        public int GetUniquePaths()
        {
            var pathsString = new List<string>();
            foreach (var path in paths)
            {
                  pathsString.Add(string.Join("-",path));
            }

            pathsString = pathsString.Distinct().ToList();
            
            return pathsString.Count();
        }
        public void DFS(string startVertexName, string endVertexName, List<string> pathSoFar,
         Dictionary<string, int> visited, string specialCaveName)
        {
            var currentVertex = GetVertexByName(startVertexName);
            pathSoFar.Add(currentVertex._name);
            
            if (currentVertex._name == endVertexName)
            {
                paths.Add(pathSoFar);
            }
            else
            {
                visited.TryGetValue(currentVertex._name, out var isVisited);
                if (!currentVertex._isSmallCave || (
                        currentVertex._isSmallCave && isVisited==0) ||
                    currentVertex._isSmallCave && isVisited==1 && specialCaveName == currentVertex._name)
                {
                    visited[currentVertex._name] = isVisited + 1;
                    foreach (var vertex in currentVertex._edges)
                    {
                        DFS(vertex, endVertexName, new List<string>(pathSoFar),
                            new Dictionary<string, int>(visited), specialCaveName);
                    }
                }
            }
        } 
    }
}
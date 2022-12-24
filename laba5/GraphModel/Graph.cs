using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace laba5.GraphModel
{
    public class Graph
    {
        private int _V;
        private List<int>[] _adjustmentList;
        public List<int>[] adjList => _adjustmentList;
        public int V => _V;

        public Graph(int v, int minVertexDegree, int maxVertexDegree)
        {
            _V = v;
            _adjustmentList = Generate(v, minVertexDegree, maxVertexDegree);
        }
        public Graph(int v, List<int>[] adjustmentList)
        {
            _V = v;
            _adjustmentList = adjustmentList;
        }
        public Graph(string filePath)
        {
            Graph g = ReadFromFile(filePath);
            this._V = g._V;
            this._adjustmentList = g._adjustmentList;
        }

        public static List<int>[] Generate(int v, int minVertexDegree, int maxVertexDegree)
        {
            List<int>[] adjList = new List<int>[v];
            for (int i = 0; i < v; i++)
                adjList[i] = new List<int>();
            List<Vertex> initial = new List<Vertex>(v);
            List<Vertex> vertices = new List<Vertex>(v);
            Random rnd = new Random();
            //decide the degree of each vertex
            for (int i = 0; i < v; i++)
            {
                Vertex vertex = new Vertex(i, rnd.Next(minVertexDegree, maxVertexDegree + 1));
                vertices.Add(vertex);
                initial.Add(vertex);
            }

            int indexOutSet, indexInSet;
            //connect all vertices in graph
            indexInSet = 0;
            vertices.RemoveAt(0);
            for (int i = 0; i < v - 1; i++)
            {
                indexOutSet = rnd.Next(0, vertices.Count);

                AddEdge(adjList, indexInSet, vertices[indexOutSet].index, initial[indexInSet], vertices[indexOutSet]);

                indexInSet = vertices[indexOutSet].index;
                vertices.RemoveAt(indexOutSet);

            }
            // add missing edges
            int index;
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < initial[i].edgeNumber; j++)
                {
                    index = rnd.Next(0, v - 1);
                    if (initial[index].edgeNumber > 0 && index != i && !IsAlreadyInList(adjList, i, index))
                    {
                        AddEdge(adjList, i, index, initial[i], initial[index]);
                    }
                }
            }
            for (int i = 0; i < v; i++)
                adjList[i].Sort();
            return adjList;
        }
        private static void AddEdge(List<int>[] adjList, int v1, int v2, Vertex vertex1, Vertex vertex2)
        {
            adjList[v1].Add(v2);
            adjList[v2].Add(v1);
            vertex1.edgeNumber--;
            vertex2.edgeNumber--;
        }
        private static bool IsAlreadyInList(List<int>[] adjList, int v, int vToConnect)
        {
            for (int i = 0; i < adjList[v].Count; i++)
            {
                if (adjList[v][i] == vToConnect) return true;
            }
            return false;
        }

        public void Display()
        {
            for (int i = 0; i < _V; i++)
            {
                Console.Write(Convert.ToString(i).PadRight(3) + ":");
                for (int j = 0; j < _adjustmentList[i].Count; j++)
                    Console.Write(" " + _adjustmentList[i][j]);
                Console.WriteLine();
            }
        }
        public void WriteToFile(string path)
        {
            string[] lines = new string[_V];
            for (int i=0; i< _V;i++)
            {
                for (int j = 0; j < _adjustmentList[i].Count; j++)
                    lines[i] += Convert.ToString(_adjustmentList[i][j] + " ");
                lines[i].Trim();
            }
            File.WriteAllLines(path, lines);
        }
        public static Graph ReadFromFile(string path)
        {            
            string[]lines = File.ReadAllLines(path);
            int v = lines.Length;
            List<int>[] adjList = new List<int>[v];
            for (int i = 0; i < v; i++)
                adjList[i] = new List<int>();
            for (int i=0; i< v; i++)
            {
                string[] vertices = lines[i].Split();
                for (int j=0; j< vertices.Length; j++)
                {
                    if (vertices[j].Length > 0)
                        adjList[i].Add(Convert.ToInt32(vertices[j]));
                }
            }
            return new Graph(v, adjList);
        }
    }
}

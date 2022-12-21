using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    public class Vertex
    {
        public int index;
        public int edgeNumber;
        public Vertex(int index, int edgeNumber)
        {
            this.index = index;
            this.edgeNumber = edgeNumber;
        }
    }
    public class Graph
    {
        private int _V;
        private List<int> [] _adjustmentList;

        public Graph(int v, int minVertexDegree, int maxVertexDegree)
        {
            this._V = v;
            _adjustmentList = Generate(v, minVertexDegree, maxVertexDegree);
        }
        public static List<int>[] Generate(int v, int minVertexDegree, int maxVertexDegree)
        {
            List<int>[] adjList = new List<int>[v];
            List<Vertex> initial = new List<Vertex>();
            List<Vertex> vertices = new List<Vertex> (v);
            Random rnd = new Random();
            //decide the degree of each vertex
            for (int i=0; i<v; i++)
                vertices[i] = new Vertex(i, rnd.Next(minVertexDegree, maxVertexDegree));
            int index, vertex;
            //connect all vertices in graph
            for (int i=0; i<v-1; i++)
            {
                index = rnd.Next(0, vertices.Count - 1);

                AddEdge(adjList, i, vertices[index].index, vertices[i], vertices[index]);

                initial.Add(vertices[index]);
                vertices.RemoveAt(index);

            }
            // add missing edges
            for (int i = 0;i<v; i++)
            {
                for (int j = 0; j<initial[i].edgeNumber; j++)
                {
                    index = rnd.Next(0, v - 1);
                    if (initial[index].edgeNumber > 0 && !IsAlreadyInList(adjList, i, index))
                    {
                        AddEdge(adjList, i, index, initial[i], initial[index]);                        
                    }
                }
            }
            return adjList;
        }
        public static void AddEdge(List<int>[] adjList, int v1, int v2, Vertex vertex1, Vertex vertex2)
        {
            adjList[v1].Add(v2);
            adjList[v2].Add(v1);
            vertex1.edgeNumber--;
            vertex2.edgeNumber--;
        }
        public static bool IsAlreadyInList(List<int>[] adjList, int v, int vToConnect)
        {
            for(int i=0; i < adjList[v].Count; i++)
            {
                if (adjList[v][i] == vToConnect) return true;
            }
            return false;
        }
    }
}

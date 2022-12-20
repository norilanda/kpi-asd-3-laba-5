using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
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

            return adjList;
        }
    }
}

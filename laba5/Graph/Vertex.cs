using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.Graph
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
}

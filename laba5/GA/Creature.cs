using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.GA
{
    public class Creature
    {
        public static List<int>[] adjList;
        private bool[] _chromosome;
        private int _F;
        private List<int> maxClique;
        public int F
        { 
            get { return _F; }
            set { _F = value; }
        }
        public Creature(bool[] chromosome) 
        {
            _chromosome = chromosome;
            _F = 0;
        }
        private void ExtractMaxClique()
        {
            maxClique = new List<int>();
            // finish!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }
}

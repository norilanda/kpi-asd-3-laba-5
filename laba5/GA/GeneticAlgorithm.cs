using laba5.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.GA
{
    public class GeneticAlgorithm
    {
        int v;
        private Creature[] initialPopulation;

        public GeneticAlgorithm(Graph graph)
        {
            Creature.adjList = graph.adjList;
            v = graph.adjList.Length;
        }
        private void CreateInitialPopulation()
        {
            initialPopulation = new Creature[v];
            bool[] chromosome;
            for (int i=0; i<v; i++)
            {
                chromosome = new bool[v];
                chromosome[i] = true;
                initialPopulation[i] = new Creature(chromosome);
            }
        }
    }
}

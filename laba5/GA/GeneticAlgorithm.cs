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
            CreateInitialPopulation();
        }
        private void CreateInitialPopulation()
        {
            initialPopulation = new Creature[v];
            bool[] chromosome;
            for (int i=0; i<v; i++)
            {
                chromosome = new bool[v];
                chromosome[i] = true;
                if (i < v - 2)
                {
                    chromosome[i + 1] = true;
                    chromosome[i + 2] = true;
                }
                initialPopulation[i] = new Creature(chromosome);
            }
        }
    }
}

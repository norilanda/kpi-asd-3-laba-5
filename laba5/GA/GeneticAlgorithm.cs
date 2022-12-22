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
        public enum SelectionMethod
        {
            BestAndRandom,
            Tournament,
            Proportionate
        }
        int v;
        private SortedList<int, Creature> _currPopulation;
        private Creature bestCreature;
        SelectionMethod selectMethod;

        public GeneticAlgorithm(Graph graph)
        {
            Creature.adjList = graph.adjList;
            v = graph.adjList.Length;
            CreateInitialPopulation();
            bestCreature = _currPopulation.Last().Value;
        }
        private void CreateInitialPopulation()
        {
            _currPopulation = new SortedList<int, Creature>(new DuplicateKeyComparer<int>());
            bool[] chromosome;
            for (int i=0; i<v; i++)
            {
                chromosome = new bool[v];
                chromosome[i] = true;
                Creature creature = new Creature(chromosome);
                _currPopulation.Add(creature.F, creature);
            }
        }
        public void Start()
        {
            int tempItNumber = 100;
            for (int i=0;i<tempItNumber;i++)
            {
                Creature parent1, parent2;
                Creature child1, child2;
                Selection(out parent1, out parent2);
                //Crossover(parent1, parent2, out child1, out child2);
                //Mutation(child1);
                //Mutation(child2);
                //LocalImprovment(child1);
                //LocalImprovment(child2);
            }
        }
        private void Selection(out Creature parent1, out Creature parent2)
        {
            Random rnd = new Random();
            parent1 = bestCreature;
            do
            {
                parent2 = _currPopulation.ElementAt(rnd.Next(0, _currPopulation.Count)).Value;
            } while (parent1 == parent2);
        }
    }
}

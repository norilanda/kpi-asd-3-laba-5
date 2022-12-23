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
        //public enum SelectionMethod
        //{
        //    BestAndRandom,
        //    Tournament,
        //    Proportionate
        //}
        public enum CrossoverMethod
        {
            TwoPoints,
            FivePoints,
            Dynamic
        }
        public enum MutationMethod
        {
            ChangeToOpposite,
            Exchange
        }
        int v;
        private SortedList<int, Creature> _currPopulation;
        private Creature bestCreature;
        private CrossoverMethod crossMethod;
        private MutationMethod mutMethod;
        private int iterations;
        private int currPointNumber;//for dynamic crossover
        //SelectionMethod selectMethod;
        private double mutationPossibility;

        public GeneticAlgorithm(Graph graph, int crossMethod, int mutMethod, double mutationPossibl)
        {
            Creature.adjList = graph.adjList;
            v = graph.adjList.Length;
            CreateInitialPopulation();
            bestCreature = _currPopulation.Last().Value;
            this.crossMethod = (CrossoverMethod)crossMethod;
            this.mutMethod = (MutationMethod)mutMethod;
            iterations = 0;
            if (v > 50)
                currPointNumber = 10;
            else if (v > 18)
                currPointNumber = 5;
            else
                currPointNumber = 3;
            mutationPossibility = mutationPossibl;
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
                Crossover(parent1, parent2, out child1, out child2);
                Mutation(ref child1);
                Mutation(ref child2);
                //LocalImprovment(child1);
                //LocalImprovment(child2);
                iterations++;
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
        private void Crossover(Creature parent1, Creature parent2, out Creature child1, out Creature child2)
        {
            switch (crossMethod)
            {
                case CrossoverMethod.TwoPoints:
                    C_kPoints(parent1, parent2, out child1, out child2, 2);
                    break;
                case CrossoverMethod.FivePoints:
                    C_kPoints(parent1, parent2, out child1, out child2, 5);
                    break;
                case CrossoverMethod.Dynamic:
                    C_Dynamic(parent1, parent2, out child1, out child2);
                    break;
                default:
                    C_Dynamic(parent1, parent2, out child1, out child2);
                    break;
            }
        }
        private void C_kPoints(Creature parent1, Creature parent2, out Creature child1, out Creature child2, int pointNumber)
        {
            bool[] childChromosome1 = new bool[v];
            bool[] childChromosome2 = new bool[v];
            List<int> indices = GenerateDifferentNumbers(0, v, pointNumber);
            int start = 0, genNum;
            int i = 0;
            do
            {
                genNum = indices[i] - start;
                Array.Copy(parent1.Chromosome, start, childChromosome1, start, genNum);
                Array.Copy(parent2.Chromosome, start, childChromosome2, start, genNum);
                start = indices[i];
                i++;
            }
            while (i < pointNumber);
            //copy the rest
            genNum = v - start;
            Array.Copy(parent1.Chromosome, start, childChromosome1, start, genNum);
            Array.Copy(parent2.Chromosome, start, childChromosome2, start, genNum);

            child1 = new Creature(childChromosome1);
            child2 = new Creature(childChromosome2);
        }
        private void C_Dynamic(Creature parent1, Creature parent2, out Creature child1, out Creature child2)
        {
            C_kPoints(parent1, parent2, out child1, out child2, currPointNumber);
            if (currPointNumber>2 && iterations%3 == 0)
                currPointNumber--;
        }
        private static List<int> GenerateDifferentNumbers(int start,int end, int number)
        {
            List<int> numbers = new List<int>();
            Random rnd = new Random();
            int index;
            while (numbers.Count < number)
            {
                index = rnd.Next(start, end);
                if (!numbers.Contains(index))
                    numbers.Add(index);
            }
            numbers.Sort();
            return numbers;
        }
        private void Mutation(ref Creature child)
        {
            switch (mutMethod)
            {
                case MutationMethod.ChangeToOpposite:
                    M_ChangeToOpposite(ref child);
                    break;
                case MutationMethod.Exchange:
                    M_Exchange(ref child);
                    break;
                default: throw new NotImplementedException();
            }
        }
        private void M_ChangeToOpposite(ref Creature child)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= mutationPossibility)
            {
                int gen;
                gen = rnd.Next(0, v);
                child.Chromosome[gen] = !child.Chromosome[gen];
            }
        }
        private void M_Exchange(ref Creature child)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= mutationPossibility)
            {
                int gen1, gen2;
                gen1 = rnd.Next(0, v);
                do
                {
                    gen2 = rnd.Next(0, v);
                } while (gen1 == gen2);
                bool temp = child.Chromosome[gen1];
                child.Chromosome[gen1] = child.Chromosome[gen2];
                child.Chromosome[gen2] = temp;            
            }
        }
    }
}

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
        public enum LocalImprMethod
        {
            AddVerticesToCliqueStraight,
            AddVerticesToCliqueRandom
        }
        public enum TerminationCondition
        {
            Iterations,
            Stagnancy
        }
        int v;
        private SortedList<int, Creature> _currPopulation;
        private Creature bestCreature;
        private CrossoverMethod crossMethod;
        private MutationMethod mutMethod;
        private LocalImprMethod imprMethod;
        private TerminationCondition terminationCondition;
        private int iterations;
        private int currPointNumber;//for dynamic crossover

        private double mutationPossibility;
        private int terminationNumber;

        public Creature BestCreature => bestCreature;

        public GeneticAlgorithm(Graph graph, int crossMethod, int mutMethod, double mutationPossibl, int imprMethod, int termCondition, int terminationNumber)
        {
            Creature.adjList = graph.adjList;
            v = graph.adjList.Length;
            CreateInitialPopulation();
            bestCreature = _currPopulation.Last().Value;
            this.crossMethod = (CrossoverMethod)crossMethod;
            this.mutMethod = (MutationMethod)mutMethod;
            this.imprMethod = (LocalImprMethod)imprMethod;
            this.terminationCondition = (TerminationCondition)termCondition;
            iterations = 0;
            if (v > 50)
                currPointNumber = 10;
            else if (v > 18)
                currPointNumber = 5;
            else
                currPointNumber = 3;
            mutationPossibility = mutationPossibl; 
            this.terminationNumber = terminationNumber;
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
                creature.ExtractCliqueAndSetF();
                _currPopulation.Add(creature.F, creature);
            }
        }
        public void Start()
        {
            //int tempItNumber = 100;
            int lastBestF;            

            int currIterationNumberOrStagnancy = 0;//iteration number or stagnancy
            while (currIterationNumberOrStagnancy < terminationNumber)
            {
                lastBestF = bestCreature.F;//for stagnancy condition

                Creature parent1, parent2;
                Creature child1, child2;
                Selection(out parent1, out parent2);
                Crossover(parent1, parent2, out child1, out child2);
                Mutation(ref child1);
                Mutation(ref child2);
                child1.ExtractCliqueAndSetF();
                child2.ExtractCliqueAndSetF() ;
                LocalImprovement(child1);
                LocalImprovement(child2);
                AddChildToPopulation(child1);
                AddChildToPopulation(child2);

                if (terminationCondition == TerminationCondition.Stagnancy)
                {
                    if (lastBestF != bestCreature.F)
                        currIterationNumberOrStagnancy = -1;
                }
                else
                    currIterationNumberOrStagnancy++;

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
                    C_kPoints(parent1, parent2, out child1, out child2, 1);/////!!!!!!!!!!!!!
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
                if (i%2 == 0)
                {
                    Array.Copy(parent1.Chromosome, start, childChromosome1, start, genNum);
                    Array.Copy(parent2.Chromosome, start, childChromosome2, start, genNum);
                }
                else
                {
                    Array.Copy(parent2.Chromosome, start, childChromosome1, start, genNum);
                    Array.Copy(parent1.Chromosome, start, childChromosome2, start, genNum);
                }
                start = indices[i];
                i++;
            }
            while (i < pointNumber);
            //copy the rest
            genNum = v - start;
            if (i % 2 == 0)
            {
                Array.Copy(parent1.Chromosome, start, childChromosome1, start, genNum);
                Array.Copy(parent2.Chromosome, start, childChromosome2, start, genNum);
            }
            else
            {
                Array.Copy(parent2.Chromosome, start, childChromosome1, start, genNum);
                Array.Copy(parent1.Chromosome, start, childChromosome2, start, genNum);
            }

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
                    M_ChangeToOpposite(child);
                    break;
                case MutationMethod.Exchange:
                    M_Exchange(child);
                    break;
                default: throw new NotImplementedException();
            }
        }
        private void M_ChangeToOpposite(Creature child)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= mutationPossibility)
            {
                int gen;
                gen = rnd.Next(0, v);
                child.Chromosome[gen] = !child.Chromosome[gen];
            }
        }
        private void M_Exchange(Creature child)
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
        private void LocalImprovement(Creature child)
        {
            switch (imprMethod)
            {
                case LocalImprMethod.AddVerticesToCliqueStraight:
                    LI_Straight(child); break;
                case LocalImprMethod.AddVerticesToCliqueRandom:
                    LI_Random(child); break;
                default: break;
            }
        }
        private void LI_Straight(Creature child)
        {
            child.ImproveClique();
        }
        private void LI_Random(Creature child)
        {
            Random rnd = new Random();
            int number = rnd.Next(0, v);            
            child.ImproveClique(number, v);
            child.ImproveClique(0, number + 1);
        }
        private void AddChildToPopulation(Creature child)   //add child and remove the worst
        {            
            if (bestCreature.F < child.F)
                bestCreature = child;
            if (child.F >= _currPopulation.ElementAt(0).Value.F)
            {
                _currPopulation.Add(child.F, child);
                _currPopulation.RemoveAt(0);
            }
        }
    }
}

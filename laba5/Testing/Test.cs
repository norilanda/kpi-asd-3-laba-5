using laba5.GA;
using laba5.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace laba5.Testing
{
    public static class Test
    {
        public static void CreateGraphs(int GNumber, int vertexNumber, int minDegree, int maxDegree)
        {
            for (int i=0;i <GNumber; i++)
            {
                string path = "graph" + i + ".txt";
                Graph graph = new Graph(vertexNumber, minDegree, maxDegree);
                graph.WriteToFile(path);
            }            
        }
        public static void TestOnDiffGraphs(int GNumber)
        {
            int crossMethod = 2; //0-TwoPoints, 1-FivePoints, 2-Dynamic
            int mutMethod = 1; //0-ChangeToOpposite, 1-Exchange
            int imprMethod = 2; //0-AddVerticesToCliqueStraight, 1-AddVerticesToCliqueRandom, 2-AddVerticesFromAdjList
            int terminationCondition = 1; //0-number of iterations, 1-stagnancy, 2-fullGraph
            int terminationNumber = 300;
            double mutationPossibility = 0.1;
            int k = 5;
            string[] conclusion = new string[GNumber];
            for (int i = 0; i < GNumber; i++)
            {
                Console.WriteLine("Number " + (int)(i + 1));
                string path = "graph" + i + ".txt";
                Graph graph = new Graph(path);
                if (graph.V < 30)
                {
                    graph.Display();
                    Console.WriteLine();
                }
                GeneticAlgorithm ga = new GeneticAlgorithm(graph, crossMethod, mutMethod, mutationPossibility, imprMethod, terminationCondition, terminationNumber);
                ga.Start(k);
                Creature bestCreature = ga.BestCreature;
                conclusion[i] += ga.Iterations;
                if (ga.HasAClique)
                {
                    Console.WriteLine("Graph CONTAINS clique with " + k + " vertices");
                    conclusion[i] += " +";
                }
                else
                { 
                    Console.WriteLine("The maximum clique has less than " + k + " vertices. Graph DOESN'T contain clique with " + k + " vertices");
                    conclusion[i] += " -";
                }
                Console.WriteLine("Maximum clique: ");
                bestCreature.DisplayMaxClique();

                Console.WriteLine("Number of iterations that has been made: " + ga.Iterations);
                Console.WriteLine();                
            }
            Console.WriteLine();
            for (int i = 0; i < GNumber; i++)
                Console.WriteLine(conclusion[i]);
        }
    }
}

﻿using laba5.GA;
using laba5.GraphModel;

string path = "graph.txt";
Graph graph1 = new Graph(300, 2, 30);
graph1.WriteToFile(path);

string pathA = "graphB.txt";
Graph graph = new Graph(pathA);
if (graph.V < 30)
    graph.Display();
Console.WriteLine();

int crossMethod = 2; //0-TwoPoints, 1-FivePoints, 2-Dynamic
int mutMethod = 1; //0-ChangeToOpposite, 1-Exchange
int imprMethod = 2; //0-AddVerticesToCliqueStraight, 1-AddVerticesToCliqueRandom, 2-AddVerticesFromAdjList
int terminationCondition = 1; //0-number of iterations, 1-stagnancy, 2-fullGraph
int terminationNumber = 1000;
double mutationPossibility = 0.1;
int k = 6;

GeneticAlgorithm ga = new GeneticAlgorithm(graph, crossMethod, mutMethod, mutationPossibility, imprMethod, terminationCondition, terminationNumber);
ga.Start(k);
Creature bestCreature = ga.BestCreature;
if (ga.HasAClique)
    Console.WriteLine("Graph CONTAINS clique with " + k + " vertices");
else
    Console.WriteLine("The maximum clique has less than " + k + " vertices. Graph DOESN'T contain clique with " + k + " vertices");
Console.WriteLine("Maximum clique: ");
bestCreature.DisplayMaxClique();

Console.WriteLine("Number of iterations that has been made: " + ga.Iterations);
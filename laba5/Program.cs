using laba5.GA;
using laba5.GraphModel;
using laba5.Testing;

//Test.CreateGraphs(10, 300, 2, 30);

//Test.TestOnDiffGraphs(10);

string path = "graph.txt";
int vertexNumber = 300;
int minDegree = 2;
int maxDegree = 30;
Graph graph1 = new Graph(vertexNumber, minDegree, maxDegree);
graph1.WriteToFile(path);

string pathA = "graphB.txt";
Graph graph = new Graph(path);

Console.WriteLine("Graph parameters: vertex number = " + vertexNumber + "; minDegree = " + minDegree + "; maxDegree = " + maxDegree + "\n");
if (graph.V < 30)
{
    Console.WriteLine("Initial graph:\n");
    graph.Display();
}
Console.WriteLine();

int crossMethod = 2; //0-TwoPoints, 1-FivePoints, 2-Dynamic
int mutMethod = 0; //0-ChangeToOpposite, 1-Exchange
int imprMethod = 2; //0-AddVerticesToCliqueStraight, 1-AddVerticesToCliqueRandom, 2-AddVerticesFromAdjList
int terminationCondition = 1; //0-number of iterations, 1-stagnancy, 2-fullGraph
int terminationNumber = 200;
double mutationPossibility = 0.1;
int k = 4;
Console.Write("Enter clique size to search for: ");
k = Convert.ToInt32(Console.ReadLine());

Console.Write("GA parametrs:\ncrossover method - ");
switch (crossMethod)
{
    case 0: Console.Write("TwoPoints; "); break;
    case 1: Console.Write("FivePoints; "); break;
    case 2: Console.Write("Dynamic; "); break;
    default: break;
}
Console.Write("mutation method - ");
switch (mutMethod)
{
    case 0: Console.Write("ChangeToOpposite; "); break;
    case 1: Console.Write("Exchange; "); break;
    default: break;
}
Console.Write("improvement method - ");
switch (imprMethod)
{
    case 1: Console.Write("AddVerticesToCliqueRandom; "); break;
    case 2: Console.Write("AddVerticesFromAdjList; "); break;
    default: break;
}
Console.Write("\ntermination condition - ");
switch (terminationCondition)
{
    case 0: Console.Write("number of iterations; "); break;
    case 1: Console.Write("stagnancy; "); break;
    default: break;
}
Console.Write("termination number - " + terminationNumber);
Console.WriteLine("\n");

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
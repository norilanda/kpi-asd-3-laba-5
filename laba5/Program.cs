using laba5.GA;
using laba5.GraphModel;

string path = "graph.txt";
Graph graph1 = new Graph(20, 2, 15);
//graph1.WriteToFile(path);

Graph graph = new Graph(path);
if (graph.V < 30)
    graph.Display();
Console.WriteLine();

int crossMethod = 0; //0-TwoPoints, 1-FivePoints, 2-Dynamic
int mutMethod = 1; //0-ChangeToOpposite, 1-Exchange
int imprMethod = 1; //0-AddVerticesToCliqueStraight, 1-AddVerticesToCliqueRandom
int terminationCondition = 1; //0-number of iterations, 1-stagnancy
int terminationNumber = 50;
double mutationPossibility = 0.1;
int k = 5;

GeneticAlgorithm ga = new GeneticAlgorithm(graph, crossMethod, mutMethod, mutationPossibility, imprMethod, terminationCondition, terminationNumber);
ga.Start(k);
Creature bestCreature = ga.BestCreature;
if (ga.HasAClique)
    Console.WriteLine("Graph CONTAINS clique with " + k + " vertices");
else
    Console.WriteLine("The maximum clique has less than " + k + " vertices. Graph DOESN'T contain clique with " + k + " vertices");
bestCreature.DisplayMaxClique();
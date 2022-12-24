using laba5.GA;
using laba5.GraphModel;

string path = "graph.txt";
Graph graph1 = new Graph(7, 2, 4);
graph1.WriteToFile(path);

//Graph graph2 = new Graph(5, new List<int>[] { new List<int>{1, 2 }, new List<int> {0, 2, 4 },
//    new List<int> { 0, 1, 3 }, new List<int> {2, 4 }, new List<int> {1, 3 } });
Graph graph3 = new Graph(path);
graph3.Display();
Console.WriteLine();
//graph2.Display();

int crossMethod = 0; //0-TwoPoints, 1-FivePoints, 2-Dynamic
int mutMethod = 1; //0-ChangeToOpposite, 1-Exchange
int imprMethod = 1; //0-AddVerticesToCliqueStraight, 1-AddVerticesToCliqueRandom
int terminationCondition = 0; //0-number of iterations, 1-stagnancy
int terminationNumber = 100;
double mutationPossibility = 0.1;

GeneticAlgorithm ga = new GeneticAlgorithm(graph3, crossMethod, mutMethod, mutationPossibility, imprMethod, terminationCondition, terminationNumber);
ga.Start();
Creature bestCreature = ga.BestCreature;
bestCreature.DisplayMaxClique();
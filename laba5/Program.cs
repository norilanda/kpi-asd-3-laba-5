using laba5.GA;
using laba5.GraphModel;

Graph graph1 = new Graph(5, 1, 4);
Graph graph2 = new Graph(5, new List<int>[] { new List<int>{1, 2 }, new List<int> {0, 2, 4 },
    new List<int> { 0, 1, 3 }, new List<int> {2, 4 }, new List<int> {1, 3 } });
//graph1.Display();
//Console.WriteLine();
//graph2.Display();

int crossMethod = 0; //0-TwoPoints, 1-FivePoints, 2-Dynamic
int mutMethod = 1; //0-ChangeToOpposite, 1-Exchange
int imprMethod = 1; //0-AddVerticesToCliqueStraight, 1-AddVerticesToCliqueRandom
double mutationPossibility = 0.1;

GeneticAlgorithm ga = new GeneticAlgorithm(graph2, crossMethod, mutMethod, mutationPossibility, imprMethod);
ga.Start();
Creature bestCreature = ga.BestCreature;
bestCreature.DisplayMaxClique();
using laba5.GA;
using laba5.GraphModel;

Graph graph1 = new Graph(5, 1, 4);
Graph graph2 = new Graph(5, new List<int>[] { new List<int>{1, 2 }, new List<int> {0, 2, 4 },
    new List<int> { 0, 1, 3 }, new List<int> {2, 4 }, new List<int> {1, 3 } });
//graph1.Display();
Console.WriteLine();
//graph2.Display();

int crossMethod = 2; //0-TwoPoints, 1-FivePoints, 2-Dynamic
int mutMethod = 0; //0-ChangeToOpposite, 1-Exchange

GeneticAlgorithm ga = new GeneticAlgorithm(graph2, crossMethod, mutMethod);
ga.Start();
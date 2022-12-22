using laba5.GraphModel;

Graph graph1 = new Graph(5, 1, 4);
Graph graph2 = new Graph(5, new List<int>[] { new List<int>{1, 2 }, new List<int> {0, 2, 4 },
    new List<int> { 0, 1, 3 }, new List<int> {2, 4 }, new List<int> {1, 3 } });
graph1.Display();
Console.WriteLine();
graph2.Display();
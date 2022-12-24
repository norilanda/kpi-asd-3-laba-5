using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.GA
{
    public class Creature
    {
        public static List<int>[] adjList;
        private bool[] _chromosome;
        private int _F;
        private Dictionary<int, List<int>> maxClique;
        public int F
        { 
            get { return _F; }
            set { _F = value; }
        }
        public bool[] Chromosome => _chromosome;
        public Creature(bool[] chromosome) 
        {
            _chromosome = new bool[chromosome.Length];
            Array.Copy(chromosome, _chromosome,chromosome.Length);
        }
        public void ExtractCliqueAndSetF()
        {
            ExtractMaxClique();
            _F = maxClique.Count;
        }
        private void ExtractMaxClique()
        {
            maxClique = new Dictionary<int, List<int>>();
            // add vertices from chromosome to dictionary
            for (int i=0;i< _chromosome.Length;i++)
            {
                if (_chromosome[i])
                    maxClique[i] = new List<int>();
            }
            //connect vertices
            foreach (int i in maxClique.Keys)
            {
                for (int j=0; j < adjList[i].Count; j++)
                {
                    int index = adjList[i][j];
                    if (maxClique.ContainsKey(index) && !maxClique[i].Contains(index) && i != index)
                    {
                        maxClique[index].Add(i);
                        maxClique[i].Add(index);
                    }     
                }
            }
            Random rnd= new Random();
            while (!IsClique())
            {
                List<int> candidatesForDeleting = new List<int>();
                int minDegree = int.MaxValue;
                //forming a candidatesForDeleting list
                foreach (int i in maxClique.Keys)
                {
                    if (maxClique[i].Count < minDegree)
                    {
                        candidatesForDeleting.Clear();
                        candidatesForDeleting.Add(i);
                        minDegree = maxClique[i].Count;
                    }
                    else if (maxClique[i].Count == minDegree)
                        candidatesForDeleting.Add(i);
                }
                int index = candidatesForDeleting[rnd.Next(candidatesForDeleting.Count)];//get vertex index to remove
                for (int i=0; i < maxClique[index].Count; i++)
                {
                    int connectedVertex = maxClique[index][i];
                    maxClique[connectedVertex].Remove(index);
                }
                maxClique.Remove(index);
            }
        }
        private bool IsClique()
        {
            foreach (int i in maxClique.Keys)
            {
                foreach (int j in maxClique.Keys)
                {
                    if (i == j) continue;
                    if (!maxClique[j].Contains(i))
                        return false;
                }
            }
            return true;
        }
        public void ImproveClique(int start = 0, int number = -1)
        {
            if (number == -1)
                number = _chromosome.Length;
            for(int i=start;i < number;i++)
            {
                if (!_chromosome[i])
                {
                    bool sholdAdd = true;
                    foreach (int key in maxClique.Keys)
                    {
                        if (!Creature.adjList[i].Contains(key))
                        {
                            sholdAdd= false;
                            break;
                        }
                    }
                    if (sholdAdd)
                    {
                        AddVertex(i);
                    }
                }
            }
        }
        private void AddVertex(int i)
        {
            foreach (int key in maxClique.Keys)
                maxClique[key].Add(i);
            List<int> adjListForI = new List<int>(maxClique.Keys);
            maxClique[i] = adjListForI;
            _chromosome[i] = true;
            _F++;
        }
        public void AddVerticesFromAdjList()
        {
            //create candidates to add to clique as vertices that are common for all vertices in the click but are not added yet
            if (maxClique.Count > 1)
            {
                //start with first vertex
                int firstVertexKey = maxClique.Keys.First();
                List<int> commonVerticesList = new List<int>();
                for (int i = 0; i < Creature.adjList[firstVertexKey].Count; i++)
                {
                    if (!_chromosome[Creature.adjList[firstVertexKey][i]])
                        commonVerticesList.Add(Creature.adjList[firstVertexKey][i]);
                }
                foreach (int key in maxClique.Keys)
                {
                    for (int j = 0; j < commonVerticesList.Count; j++)
                    {
                        if (!Creature.adjList[key].Contains(commonVerticesList[j]))
                            commonVerticesList.Remove(commonVerticesList[j]);
                    }
                    if (commonVerticesList.Count == 0) break;
                }
                if (commonVerticesList.Count > 0)
                {
                    AddVertex(commonVerticesList[0]);
                }
            }
            else
                ImproveClique();
        }
        public void DisplayMaxClique()
        {
            foreach (int key in maxClique.Keys)
            {
                Console.Write(Convert.ToString(key).PadLeft(3) + ":");
                for (int i = 0; i < maxClique[key].Count; i++)
                    Console.Write(" "+maxClique[key][i]);
                Console.WriteLine();
            }
        }
        public bool IsCompleteGraph()
        {
            for (int i=0; i<_chromosome.Length; i++)
            {
                if (!_chromosome[i])
                    return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Найдите эйлеров цикл в графе. 

Формат входных данных:
В первой строке указаны два числа разделенных пробелом: v (число вершин) и e (число ребер). 
В следующих e строках указаны пары вершин, соединенных ребром. 
Выполняются ограничения: 2≤v≤1000,0≤e≤1000 .

Формат выходных данных:
Одно слово: NONE, если в графе нет эйлерова цикла, или список вершин в порядке обхода эйлерова цикла, если он есть.

 */

namespace EulerTourCSharp
{
    class Program
    {
        public static List<Vertex> VerticesList;
        private static int _numberVerticies;

        static void Main(string[] args)
        {
            Input();
            if (CheckDegrees())
            {
                Stack<Vertex> eulerTour = EulerTourSearch();
                if (CheckDiscoveredEdges())
                    Output(eulerTour);
            }
        }

        private static bool CheckDiscoveredEdges()
        {
            foreach (var vertex in VerticesList)
            {
                foreach (var edge in vertex.AdjacencyList)
                {
                    if (edge.IsDiscovered == false)
                    {
                        Console.WriteLine("NONE");
                        return false;
                    }
                }
            }

            return true;
        }

        private static void Output(Stack<Vertex> eulerTour)
        {
            StringBuilder sb = new StringBuilder("", _numberVerticies * 5);
            int i = 0;
            foreach (var vertex in eulerTour)
            {
                if (i == eulerTour.Count - 1)
                    break;
                sb.Append(vertex.Index.ToString() + " ");
                i++;
            }
            Console.WriteLine(sb);
        }

        private static Stack<Vertex> EulerTourSearch()
        {
            Stack<Vertex> startStack = new Stack<Vertex>();
            Stack<Vertex> endStack = new Stack<Vertex>();

            startStack.Push(VerticesList[0]);

            while (startStack.Count != 0)
            {
                Vertex curVertex = startStack.Peek();
                bool hasNotDiscoveredEdges = false;
                foreach (var incidentEdge in curVertex.AdjacencyList)
                    if (incidentEdge.IsDiscovered == false)
                    {
                        incidentEdge.IsDiscovered = true;
                        incidentEdge.IncidentTo.AdjacencyList.Find(edge => edge.IncidentTo == curVertex && edge.IsDiscovered == false).IsDiscovered = true;
                        startStack.Push(incidentEdge.IncidentTo);
                        hasNotDiscoveredEdges = true;
                        break;
                    }

                if (hasNotDiscoveredEdges == false)
                    endStack.Push(startStack.Pop());
            }

            return endStack;
        }

        private static bool CheckDegrees()
        {
            foreach (var vertex in VerticesList)
            {
                if (vertex.AdjacencyList.Count % 2 != 0 || vertex.AdjacencyList.Count == 0)
                {
                    Console.WriteLine("NONE");
                    return false;
                }
            }

            return true;
        }

        private static void Input()
        {
            int v, e;
            var str = Console.ReadLine();
            var array = str.Split();
            v = int.Parse(array[0]);
            e = int.Parse(array[1]);
            _numberVerticies = v;

            VerticesList = new List<Vertex>(v);

            for (int i = 1; i <= v; i++)
            {
                VerticesList.Add(new Vertex(i));
            }

            for (int i = 0; i < e; i++)
            {
                str = Console.ReadLine();
                array = str.Split();

                int firstIndex = int.Parse(array[0]);
                int secondIndex = int.Parse(array[1]);

                Vertex firstVertex = VerticesList[firstIndex - 1];
                Vertex secondVertex = VerticesList[secondIndex - 1];

                IncidentEdge curEdge = new IncidentEdge(secondVertex);
                firstVertex.AdjacencyList.Add(curEdge);
                curEdge = new IncidentEdge(firstVertex);
                secondVertex.AdjacencyList.Add(curEdge);
            }
        }
    }
}

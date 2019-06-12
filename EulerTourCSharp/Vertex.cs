using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerTourCSharp
{
    public class Vertex //<T> where T : IComparable
    {
        public bool IsDiscovered;
        public int Index;
        public List<IncidentEdge> AdjacencyList;


        public Vertex(int index)
        {
            Index = index;
            AdjacencyList = new List<IncidentEdge>();
        }
    }
}

using App1.Iunia_Fabi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Iunia_Fabi
{
    internal class Fabi__Path_Finding : Graph
    {

        private Dictionary<int, int> bfs(int start)
        {
            Dictionary<int, int> parents = new Dictionary<int, int>();
            Queue<int> q = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();

            q.Enqueue(start);
            visited.Add(start);
            parents.Add(start, 0);

            while (q.Count() != 0)
            {
                int city = q.Dequeue();
                foreach (int neighbour in outbound[city])
                    if (!visited.Contains(neighbour))
                    {
                        q.Enqueue(neighbour);
                        visited.Add(neighbour);
                        parents.Add(neighbour, city);
                    }
            }

            return parents;
        }


        public List<int> path(int start, int end)
        {
            List<int> path = new List<int>();
            Dictionary<int, int> parents = bfs(start);

            int city = end;
            while (city != 0)
            {
                path.Add(cities[city].id);
                city = parents[city];
            }

            path.Reverse();
            if (path[0] != start)
                return new List<int>();
            return path;
        }
    }
}

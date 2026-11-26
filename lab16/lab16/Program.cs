using System;
using System.Collections.Generic;

namespace lab16
{
    internal class Program
    {
        class PriorityQueue
        {
            private List<Tuple<string, int>> items =
                new List<Tuple<string, int>>();

            public void Add(string vertex, int priority)
            {
                items.Add(new Tuple<string, int>(vertex, priority));
            }

            public Tuple<string, int> RemoveMin()
            {
                int minIndex = 0;

                for (int i = 1; i < items.Count; i++)
                {
                    if (items[i].Item2 < items[minIndex].Item2)
                    {
                        minIndex = i;
                    }
                }

                Tuple<string, int> min = items[minIndex];
                items.RemoveAt(minIndex);

                return min;
            }

            public bool IsEmpty
            {
                get { return items.Count == 0; }
            }
        }

        static Tuple<Dictionary<string, int>,
                    Dictionary<string, string>,
                    int, int> Dijkstra(
            Dictionary<string, List<Tuple<string, int>>> graph,
            string start)
        {
            Dictionary<string, int> dist =
                new Dictionary<string, int>();

            Dictionary<string, string> prev =
                new Dictionary<string, string>();

            HashSet<string> done = new HashSet<string>();

            int taken = 0;
            int processed = 0;

            foreach (string v in graph.Keys)
            {
                dist[v] = int.MaxValue;
            }

            dist[start] = 0;

            PriorityQueue queue = new PriorityQueue();

            queue.Add(start, 0);

            while (!queue.IsEmpty)
            {
                Tuple<string, int> item = queue.RemoveMin();

                string v = item.Item1;
                int d = item.Item2;

                taken++;

                if (done.Contains(v))
                {
                    continue;
                }

                done.Add(v);
                processed++;

                foreach (Tuple<string, int> edge in graph[v])
                {
                    string nb = edge.Item1;
                    int w = edge.Item2;

                    int candidate = dist[v] + w;

                    if (candidate < dist[nb])
                    {
                        dist[nb] = candidate;
                        prev[nb] = v;

                        queue.Add(nb, candidate);
                    }
                }
            }

            return Tuple.Create(dist, prev, taken, processed);
        }

        static List<string> BuildRoute(
            Dictionary<string, string> prev,
            string start,
            string target)
        {
            List<string> route = new List<string>();

            if (start == target)
            {
                route.Add(start);
                return route;
            }

            if (!prev.ContainsKey(target))
            {
                return route;
            }

            string current = target;

            route.Add(current);

            while (current != start)
            {
                if (!prev.ContainsKey(current))
                {
                    return new List<string>();
                }

                current = prev[current];

                route.Add(current);
            }

            route.Reverse();

            return route;
        }

        static void Main(string[] args)
        {
            Dictionary<string, List<Tuple<string, int>>> graph =
                new Dictionary<string, List<Tuple<string, int>>>();

            graph["Склад"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Школа", 7),
                Tuple.Create("Ринок", 2),
                Tuple.Create("Лікарня", 12)
            };

            graph["Школа"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Склад", 7),
                Tuple.Create("Ринок", 3),
                Tuple.Create("Бібліотека", 4)
            };

            graph["Ринок"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Склад", 2),
                Tuple.Create("Школа", 3),
                Tuple.Create("Лікарня", 9)
            };

            graph["Лікарня"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Склад", 12),
                Tuple.Create("Ринок", 9),
                Tuple.Create("Вокзал", 6),
                Tuple.Create("Парк", 5)
            };

            graph["Бібліотека"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Школа", 4),
                Tuple.Create("Вокзал", 1)
            };

            graph["Вокзал"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Бібліотека", 1),
                Tuple.Create("Лікарня", 6),
                Tuple.Create("Парк", 3)
            };

            graph["Парк"] = new List<Tuple<string, int>>()
            {
                Tuple.Create("Вокзал", 3),
                Tuple.Create("Лікарня", 5)
            };

            graph["Хутір"] = new List<Tuple<string, int>>();

            var result = Dijkstra(graph, "Склад");

            Dictionary<string, int> dist = result.Item1;
            Dictionary<string, string> prev = result.Item2;

            int taken = result.Item3;
            int processed = result.Item4;

            Console.WriteLine("АЛГОРИТМ ДЕЙКСТРИ");

            Console.WriteLine(
                "Вийнято з черги: " + taken +
                "; оброблено вершин: " + processed);

            string[] targets = { "Парк", "Лікарня", "Хутір" };

            foreach (string target in targets)
            {
                List<string> route =
                    BuildRoute(prev, "Склад", target);

                if (route.Count == 0)
                {
                    Console.WriteLine(target + ": маршруту немає");
                }
                else
                {
                    Console.WriteLine(
                        target + ": " +
                        string.Join(" -> ", route) +
                        " (" + dist[target] + " хв)");
                }
            }

            Console.ReadKey();
        }
    }
}
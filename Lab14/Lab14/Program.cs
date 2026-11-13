using System;
using System.Collections.Generic;
using System.Text;

namespace Lab14
{
    internal class Program
    {
        static HashSet<string> visitAll(Dictionary<string, List<string>> graph, string start)
        {
            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (string neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return visited;
        }

        static bool reachable(Dictionary<string, List<string>> graph, string from, string to)
        {
            if (!graph.ContainsKey(from) || !graph.ContainsKey(to))
                return false;

            HashSet<string> visited = visitAll(graph, from);

            return visited.Contains(to);
        }

        static List<string> findPath(Dictionary<string, List<string>> graph, string from, string to)
        {
            List<string> path = new List<string>();

            if (!graph.ContainsKey(from) || !graph.ContainsKey(to))
                return path;

            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            Dictionary<string, string> parent = new Dictionary<string, string>();

            visited.Add(from);
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                if (current == to)
                    break;

                foreach (string neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parent[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            if (!visited.Contains(to))
                return path;

            string vertex = to;

            while (vertex != from)
            {
                path.Add(vertex);
                vertex = parent[vertex];
            }

            path.Add(from);
            path.Reverse();

            return path;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            graph["Центр"] = new List<string> { "Парк", "Ринок" };
            graph["Парк"] = new List<string> { "Центр", "Вокзал" };
            graph["Ринок"] = new List<string> { "Центр", "Вокзал", "Стадіон" };
            graph["Вокзал"] = new List<string> { "Парк", "Ринок", "Озеро" };
            graph["Стадіон"] = new List<string> { "Ринок" };
            graph["Озеро"] = new List<string> { "Вокзал" };
            graph["Гай"] = new List<string> { "Міст" };
            graph["Міст"] = new List<string> { "Гай" };

            Console.WriteLine("Обхід графа від Центру:");
            HashSet<string> visited = visitAll(graph, "Центр");

            foreach (string vertex in visited)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("\nПеревірка доступності:");
            Console.WriteLine("Центр -> Озеро: " + reachable(graph, "Центр", "Озеро"));
            Console.WriteLine("Центр -> Гай: " + reachable(graph, "Центр", "Гай"));

            Console.WriteLine("\nПошук шляху:");
            List<string> path = findPath(graph, "Центр", "Озеро");

            if (path.Count > 0)
                Console.WriteLine(string.Join(" -> ", path));
            else
                Console.WriteLine("Шлях не знайдено");

            Console.WriteLine("\nПошук шляху до Гаю:");
            path = findPath(graph, "Центр", "Гай");

            if (path.Count > 0)
                Console.WriteLine(string.Join(" -> ", path));
            else
                Console.WriteLine("Шлях не знайдено");
        }
    }
}

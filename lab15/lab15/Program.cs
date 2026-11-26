using System;
using System.Collections.Generic;

namespace lab15
{
    internal class Program
    {
        static List<string> TopoSort(Dictionary<string, List<string>> before,Dictionary<string, List<string>> after)
        {
            Dictionary<string, int> incoming = new Dictionary<string, int>();

            foreach (string subject in before.Keys)
            {
                incoming[subject] = before[subject].Count;
            }

            Queue<string> queue = new Queue<string>();

            foreach (string subject in incoming.Keys)
            {
                if (incoming[subject] == 0)
                {
                    queue.Enqueue(subject);
                }
            }

            List<string> order = new List<string>();

            while (queue.Count > 0)
            {
                string v = queue.Dequeue();

                order.Add(v);

                foreach (string next in after[v])
                {
                    incoming[next]--;

                    if (incoming[next] == 0)
                    {
                        queue.Enqueue(next);
                    }
                }
            }

            return order;
        }

        static bool HasCycle(Dictionary<string, List<string>> before,Dictionary<string, List<string>> after)
        {
            List<string> order = TopoSort(before, after);

            return order.Count != before.Count;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Dictionary<string, List<string>> beforeA =
                new Dictionary<string, List<string>>()
            {
                { "Вступ", new List<string>() },
                { "Математика", new List<string>() },
                { "ООП", new List<string>() { "Вступ" } },
                { "Бази даних", new List<string>() { "Вступ" } },
                { "Алгоритми", new List<string>() { "Вступ", "Математика" } },
                { "Вебтехнології", new List<string>() { "ООП", "Бази даних" } },
                { "Курсовий", new List<string>() { "Алгоритми", "Вебтехнології" } }
            };

            Dictionary<string, List<string>> afterA =
                new Dictionary<string, List<string>>()
            {
                { "Вступ", new List<string>() { "ООП", "Бази даних", "Алгоритми" } },
                { "Математика", new List<string>() { "Алгоритми" } },
                { "ООП", new List<string>() { "Вебтехнології" } },
                { "Бази даних", new List<string>() { "Вебтехнології" } },
                { "Алгоритми", new List<string>() { "Курсовий" } },
                { "Вебтехнології", new List<string>() { "Курсовий" } },
                { "Курсовий", new List<string>() }
            };

            Dictionary<string, List<string>> beforeB =
                new Dictionary<string, List<string>>()
            {
                { "Вступ", new List<string>() },
                { "Математика", new List<string>() },
                { "ООП", new List<string>() { "Вступ" } },
                { "Бази даних", new List<string>() { "Вступ", "Вебтехнології" } },
                { "Алгоритми", new List<string>() { "Вступ", "Математика" } },
                { "Вебтехнології", new List<string>() { "ООП", "Бази даних" } },
                { "Курсовий", new List<string>() { "Алгоритми", "Вебтехнології" } }
            };

            Dictionary<string, List<string>> afterB =
                new Dictionary<string, List<string>>()
            {
                { "Вступ", new List<string>() { "ООП", "Бази даних", "Алгоритми" } },
                { "Математика", new List<string>() { "Алгоритми" } },
                { "ООП", new List<string>() { "Вебтехнології" } },
                { "Бази даних", new List<string>() { "Вебтехнології" } },
                { "Алгоритми", new List<string>() { "Курсовий" } },
                { "Вебтехнології", new List<string>() { "Курсовий", "Бази даних" } },
                { "Курсовий", new List<string>() }
            };

            Console.WriteLine("НАБІР А");

            List<string> orderA = TopoSort(beforeA, afterA);

            Console.WriteLine("Порядок складання предметів:");

            foreach (string subject in orderA)
            {
                Console.WriteLine(subject);
            }

            if (HasCycle(beforeA, afterA))
            {
                Console.WriteLine("У наборі А є цикл.");
            }
            else
            {
                Console.WriteLine("У наборі А циклу немає.");
            }

            Console.WriteLine("\nНАБІР Б");

            List<string> orderB = TopoSort(beforeB, afterB);

            Console.WriteLine("Порядок складання предметів:");

            foreach (string subject in orderB)
            {
                Console.WriteLine(subject);
            }

            if (HasCycle(beforeB, afterB))
            {
                Console.WriteLine("У наборі Б є цикл.");
            }
            else
            {
                Console.WriteLine("У наборі Б циклу немає.");
            }

            Console.ReadKey();
        }
    }
}
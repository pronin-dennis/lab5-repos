using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab._7
{
    class Request
    {
        public string name;
        public int minutes;

        public Request(string name, int minutes)
        {
            this.name = name;
            this.minutes = minutes;
        }
    }

    class Program
    {
        static string[] lines =
        {
            "","a + b","{[a] + (b)}","([)]","a)","(a[b]","f(x[1]) * {y + (z)}"
    };

        static Queue<Request> requests = new Queue<Request>(
            new Request[]
            {
            new Request("Довідка про навчання", 4),
            new Request("Академічна відпустка", 7),
            new Request("Копія наказу", 2),
            new Request("Виписка з оцінками", 6),
            new Request("Перескладання", 9),
            new Request("Студентський квиток", 3)
            }
        );

        static bool isBalanced(string line)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char c in line)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }

                    char opening = stack.Pop();

                    if ((c == ')' && opening != '(') || (c == ']' && opening != '[') || (c == '}' && opening != '{'))
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }

        static int processQueue(Queue<Request> requests)
        {
            int currentTime = 0;
            int order = 1;

            while (requests.Count > 0)
            {
                Request request = requests.Dequeue();

                int start = currentTime;
                currentTime += request.minutes;
                int end = currentTime;

                Console.WriteLine($"{order}. {request.name} з {start} до {end} хв");

                order++;
            }

            Console.WriteLine(
                $"Оброблено заявок: {order - 1}, загальний час: {currentTime} хв"
            );

            return currentTime;
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Перевірка дужок");

            foreach (string line in lines)
            {
                string result;

                if (isBalanced(line))
                {
                    result = "так";
                }
                else
                {
                    result = "ні";
                }

                Console.WriteLine($"{line} -> {result}");
            }

            Console.WriteLine();
            Console.WriteLine("Обробка заявок");

            processQueue(requests);
        }
    }
}
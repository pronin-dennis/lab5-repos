using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorna10
{
    class Request
    {
        public string name;
        public int priority;
        public int seq;

        public Request(string name, int priority, int seq)
        {
            this.name = name;
            this.priority = priority;
            this.seq = seq;
        }
    }

    internal class Program
    {
        static Request[] incoming =
        {
        new Request("Оновити підпис у листі", 3, 1),
        new Request("Сервер не відповідає", 1, 2),
        new Request("Не друкує принтер", 2, 3),
        new Request("Не працює термінал оплати", 1, 4),
        new Request("Змінити шпалери", 3, 5)
        
    };

        static Request late =
            new Request("Відвалився інтернет у відділі", 1, 6);

        static void processAll(Request[] incoming)
        {
            Console.WriteLine("Задача 1:");

            PriorityQueue<Request, int> queue = new PriorityQueue<Request, int>();

            foreach (Request request in incoming)
            {
                queue.Enqueue(request, request.priority);
            }

            int number = 1;

            while (queue.Count > 0)
            {
                Request request = queue.Dequeue();

                Console.WriteLine(
                    $"{number}. {request.name}, пріоритет: {request.priority}, seq: {request.seq}");

                number++;
            }
        }

        static void processStable(Request[] incoming)
        {
            Console.WriteLine("\nЗадача 2:");

            PriorityQueue<Request, (int, int)> queue =
                new PriorityQueue<Request, (int, int)>();

            foreach (Request request in incoming)
            {
                queue.Enqueue(request,(request.priority, request.seq));
            }

            int number = 1;

            while (queue.Count > 0)
            {
                Request request = queue.Dequeue();

                Console.WriteLine(
                    $"{number}. {request.name}, пріоритет: {request.priority}, seq: {request.seq}");

                number++;
            }
        }

        static void processWithLate(Request[] incoming, Request late)
        {
            Console.WriteLine("\nЗадача 3:");

            PriorityQueue<Request, (int, int)> queue =
                new PriorityQueue<Request, (int, int)>();

            foreach (Request request in incoming)
            {
                queue.Enqueue(request,(request.priority, request.seq));
            }

            int number = 1;

            for (int i = 0; i < 2; i++)
            {
                Request request = queue.Dequeue();

                Console.WriteLine(
                    $"{number}. {request.name}, пріоритет: {request.priority}, seq: {request.seq}");

                number++;
            }

            queue.Enqueue(late, (late.priority, late.seq));

            while (queue.Count > 0)
            {
                Request request = queue.Dequeue();

                Console.WriteLine(
                    $"{number}. {request.name}, пріоритет: {request.priority}, seq: {request.seq}");

                number++;
            }
        }

        static void Main(string[] args)
        {
            processAll(incoming);
            processStable(incoming);
            processWithLate(incoming, late);
        }
    }

}

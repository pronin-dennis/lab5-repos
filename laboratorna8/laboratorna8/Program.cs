
using System;
using System.Collections.Generic;
using System.Text;

class Client
{
    public int id;
    public string name;

    public Client(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

class Order
{
    public int number;
    public int clientId;
    public int sum;

    public Order(int number, int clientId, int sum)
    {
        this.number = number;
        this.clientId = clientId;
        this.sum = sum;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        List<Client> clients = new List<Client>
        {
            new Client(101, "Коваленко"),
            new Client(102, "Мельник"),
            new Client(103, "Шевчук"),
            new Client(104, "Бондар"),
            new Client(105, "Ткачук"),
            new Client(106, "Гриценко")
        };

        List<Order> orders = new List<Order>
        {
            new Order(1, 103, 450),
            new Order(2, 101, 120),
            new Order(3, 105, 980),
            new Order(4, 103, 300),
            new Order(5, 102, 75),
            new Order(6, 106, 1500),
            new Order(7, 101, 220),
            new Order(8, 103, 60)
        };

        List<int> blocked = new List<int> { 102, 104 };

        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("ФРАГМЕНТ 1");

        /*
        foreach (Order order in orders)
        {
            foreach (Client client in clients)
            {
                if (client.id == order.clientId)
                {
                    Console.WriteLine("Замовлення " + order.number + " — " + client.name);
                }
            }
        }
        */

        Dictionary<int, Client> byId = new Dictionary<int, Client>();

        foreach (Client client in clients)
        {
            byId[client.id] = client;
        }

        foreach (Order order in orders)
        {
            Console.WriteLine("Замовлення " + order.number + " — " + byId[order.clientId].name);
        }

        Console.WriteLine("\nФРАГМЕНТ 2");

        /*
        int total = 0;

        foreach (Order order in orders)
        {
            if (!blocked.Contains(order.clientId))
            {
                total += order.sum;
            }
        }

        Console.WriteLine("Сума прийнятих замовлень: " + total);
        */

        HashSet<int> blockedSet = new HashSet<int>(blocked);

        int total = 0;

        foreach (Order order in orders)
        {
            if (!blockedSet.Contains(order.clientId))
            {
                total += order.sum;
            }
        }

        Console.WriteLine("Сума прийнятих замовлень: " + total);

        Console.WriteLine("\nФРАГМЕНТ 3");

        /*
        List<int> counted = new List<int>();

        foreach (Order order in orders)
        {
            if (counted.Contains(order.clientId))
                continue;

            int count = 0;

            foreach (Order other in orders)
            {
                if (other.clientId == order.clientId)
                {
                    count++;
                }
            }

            counted.Add(order.clientId);
            Console.WriteLine(order.clientId + ": " + count);
        }
        */

        Dictionary<int, int> orderCounts = new Dictionary<int, int>();

        foreach (Order order in orders)
        {
            if (orderCounts.ContainsKey(order.clientId))
            {
                orderCounts[order.clientId]++;
            }
            else
            {
                orderCounts[order.clientId] = 1;
            }
        }

        foreach (Order order in orders)
        {
            if (orderCounts.ContainsKey(order.clientId))
            {
                Console.WriteLine(order.clientId + ": " + orderCounts[order.clientId]);
                orderCounts.Remove(order.clientId);
            }
        }


        Console.WriteLine("\nФРАГМЕНТ 4");

        Order best = orders[0];

        foreach (Order order in orders)
        {
            if (order.sum > best.sum)
            {
                best = order;
            }
        }
        Console.WriteLine("Найбільше замовлення: " + best.sum);


        // Фрагмент 5. Оптимізований варіант фрагмента 1
        Console.WriteLine("\nФРАГМЕНТ 5");

        /*
        foreach (Order order in orders)
        {
            Dictionary<int, Client> byId = new Dictionary<int, Client>();

            foreach (Client client in clients)
            {
                byId[client.id] = client;
            }

            Console.WriteLine("Замовлення " + order.number + " — " + byId[order.clientId].name);
        }
        */

        Dictionary<int, Client> clientsById = new Dictionary<int, Client>();

        foreach (Client client in clients)
        {
            clientsById[client.id] = client;
        }

        foreach (Order order in orders)
        {
            Console.WriteLine("Замовлення " + order.number + " — " + clientsById[order.clientId].name);
        }
    }
}

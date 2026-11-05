using System;
using System.Collections.Generic;

class Node
{
    public string title;
    public List<Node> children;

    public Node(string title, List<Node> children)
    {
        this.title = title;
        this.children = children;
    }
}

internal class Program
{
    static int callCount = 0;

    static Node menu = new Node("Головне меню", new List<Node>
    {
        new Node("Файл", new List<Node>
        {
            new Node("Новий", new List<Node>()),
            new Node("Зберегти", new List<Node>())
        }),

        new Node("Правка", new List<Node>
        {
            new Node("Копіювати", new List<Node>())
        }),

        new Node("Вигляд", new List<Node>
        {
            new Node("Масштаб", new List<Node>
            {
                new Node("Збільшити", new List<Node>()),
                new Node("Зменшити", new List<Node>())
            })
        }),

        new Node("Довідка", new List<Node>())
    });

    static void printMenu(Node node, int level)
    {
        Console.WriteLine(new string(' ', level * 2) + node.title);

        foreach (Node child in node.children)
        {
            printMenu(child, level + 1);
        }
    }

    static int countItems(Node node)
    {
        callCount++;

        int count = 1;

        foreach (Node child in node.children)
        {
            count += countItems(child);
        }

        return count;
    }

    static bool find(Node node, string name)
    {
        if (node.title == name)
        {
            return true;
        }

        foreach (Node child in node.children)
        {
            if (find(child, name))
            {
                return true;
            }
        }

        return false;
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("ДЕРЕВО МЕНЮ");
        printMenu(menu, 0);

        Console.WriteLine();

        Console.WriteLine("ПІДРАХУНОК ПУНКТІВ");
        int total = countItems(menu);

        Console.WriteLine("Кількість пунктів меню: " + total);
        Console.WriteLine("Кількість викликів countItems: " + callCount);

        Console.WriteLine();

        Console.WriteLine("ПОШУК ПУНКТІВ");

        Console.WriteLine("Пошук 'Новий': " + find(menu, "Новий"));
        Console.WriteLine("Пошук 'Довідка': " + find(menu, "Довідка"));
        Console.WriteLine("Пошук 'Видалити': " + find(menu, "Видалити"));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_rec
{
    class Category
    {
        public string name;
        public int products;
        public List<Category> children;

        public Category(string name, int products, List<Category> children)
        {
            this.name = name;
            this.products = products;
            this.children = children;
        }
    }
    class Program
    {
        static int callCount = 0;

        static Category catalog = new Category("Каталог", 0, new List<Category>
    {
        new Category("Одяг", 0, new List<Category>
        {
            new Category("Верхній одяг", 0, new List<Category>
            {
                new Category("Куртки", 12, new List<Category>()),
                new Category("Пальта", 7, new List<Category>())
            }),
            new Category("Светри", 15, new List<Category>())
        }),
        new Category("Взуття", 4, new List<Category>
        {
            new Category("Кросівки", 23, new List<Category>()),
            new Category("Чоботи", 9, new List<Category>())
        }),
        new Category("Аксесуари", 0, new List<Category>
        {
            new Category("Сумки", 18, new List<Category>()),
            new Category("Ремені", 5, new List<Category>())
        })
    });

        static void PrintTree(Category node, int level)
        {
            callCount++;

            Console.WriteLine(new string(' ', level * 2) + node.name);

            foreach (Category child in node.children)
            {
                PrintTree(child, level + 1);
            }
        }

        static int CountProducts(Category node)
        {
            callCount++;

            int total = node.products;

            foreach (Category child in node.children)
            {
                total += CountProducts(child);
            }

            return total;
        }

        static int MaxDepth(Category node)
        {
            callCount++;

            if (node.children.Count == 0)
            {
                return 1;
            }

            int maxChildDepth = 0;

            foreach (Category child in node.children)
            {
                int depth = MaxDepth(child);

                if (depth > maxChildDepth)
                {
                    maxChildDepth = depth;
                }
            }

            return maxChildDepth + 1;
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Ієрархія каталогу:");
            PrintTree(catalog, 0);

            Console.WriteLine();

            Console.WriteLine("Загальна кількість товарів: " + CountProducts(catalog));
            Console.WriteLine("Максимальна глибина: " + MaxDepth(catalog));
            Console.WriteLine("Кількість викликів функцій: " + callCount);
        }
    }
}
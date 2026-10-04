using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_sort
{
    class Student
    {
        public string surname;
        public string group;
        public int grade;
        public int year;

        public Student(string surname, string group, int grade, int year)
        {
            this.surname = surname;
            this.group = group;
            this.grade = grade;
            this.year = year;
        }
    }

    class Program
    {
        static void printAll(string title, IEnumerable<Student> items)
        {
            Console.WriteLine(title);

            foreach (Student s in items)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(s.surname + " " + s.group + " " + s.grade + " " + s.year);
            }

            Console.WriteLine();
        }

        static void Main()
        {
            List<Student> students = new List<Student>
        {
            new Student("Ткаченко", "ІПЗ-3/1", 85, 2024),
            new Student("Бондар", "ІПЗ-3/2", 92, 2023),
            new Student("Іваненко", "ІПЗ-3/1", 85, 2024),
            new Student("Коваль", "ІПЗ-3/2", 78, 2024),
            new Student("Сидоренко", "ІПЗ-3/1", 85, 2023),
            new Student("Мельник", "ІПЗ-3/2", 92, 2024),
            new Student("Гриценко", "ІПЗ-3/1", 78, 2023),
            new Student("Дяченко", "ІПЗ-3/2", 85, 2023)
        };
            List<Student> a = new List<Student>(students);

            for (int i = 0; i < a.Count - 1; i++)
            {
                for (int j = i + 1; j < a.Count; j++)
                {
                    if (a[i].surname.CompareTo(a[j].surname) > 0)
                    {
                        Student t = a[i];
                        a[i] = a[j];
                        a[j] = t;
                    }
                }
            }

            printAll("За прізвищем:", a);

            List<Student> b = new List<Student>(students);

            for (int i = 0; i < b.Count - 1; i++)
            {
                for (int j = i + 1; j < b.Count; j++)
                {
                    if (b[i].grade < b[j].grade)
                    {
                        Student t = b[i];
                        b[i] = b[j];
                        b[j] = t;
                    }
                }
            }

            printAll("За балом:", b);

            List<Student> c = new List<Student>(students);

            for (int i = 0; i < c.Count - 1; i++)
            {
                for (int j = i + 1; j < c.Count; j++)
                {
                    if (c[i].group.CompareTo(c[j].group) > 0 ||
                        c[i].group == c[j].group && c[i].grade < c[j].grade)
                    {
                        Student t = c[i];
                        c[i] = c[j];
                        c[j] = t;
                    }
                }
            }

            printAll("За групою і балом:", c);
        }
    }
}
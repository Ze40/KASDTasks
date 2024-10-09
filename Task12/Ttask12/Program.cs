using System;
using System.Reflection;
using MyLib;

namespace Task12
{
    static class Project
    {
        private static string SetPath(string name)
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return appDir + @"\" + name;
        }
        static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            MyPriorityQueueForArray<int> queue = new MyPriorityQueueForArray<int>();
            int[] request = null;
            try
            {
                string path = SetPath("out.txt");
                StreamWriter writer = new StreamWriter(path);
                for (int q = 0; q < N; q++)
                {
                    Random random = new Random();
                    int countOfRequest = random.Next(10);
                    for (int i = 0; i < countOfRequest; i++)
                    {
                        int priority = random.Next(1,6);
                        queue.Add(new int[] { priority, q+1+i, i+1 });
                        writer.WriteLine("ADD: " + (q+1+i) + " " + priority + " " + (i+1));
                    }
                }

                while (!queue.Empty())
                {
                    request = queue.Poll();
                    writer.WriteLine("REMOVE: " + request[1] + " " + request[0] + " " + request[2]);
                }
                writer.Close();
            }catch (Exception ex) { Console.WriteLine(ex.Message); }

            Console.WriteLine("Last: " + request[1] + " " + request[0] + " " + request[2]);
        }
    }
}
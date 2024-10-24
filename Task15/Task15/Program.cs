using System;
using System.Reflection;
using MyLib;

namespace Task15
{
    static class Project
    {
        public static string SetPath(string name)
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(appDir, name);
        }

        static int CountOfDigital(string str) {
            int count = 0;
            char[] digitals = new char[] {'0','1','2','3','4','5','6','7','8','9'};
            foreach (char symbol in str) if (digitals.Contains(symbol)) count++;
            return count;
        }
        static int CountOfSpace(string str)
        {
            int count = 0;
            foreach (char symbol in str) if (symbol == ' ') count++;
            return count;
        }

        static void Main(string[] args)
        {
            MyDequeue<string> dequeue = new MyDequeue<string>();
            try
            {
                StreamReader sr = new StreamReader(SetPath("input.txt"));
                string? line = sr.ReadLine();
                if (line != null) { dequeue.Add(line); }
                while (line != null)
                {
                    line = sr.ReadLine();
                    if ( line != null )
                    {
                        if (CountOfDigital(line) > CountOfDigital(dequeue.GetFirst())) dequeue.AddLast(line);
                        else dequeue.AddFirst(line);
                    }
                }
                sr.Close();

                StreamWriter sw = new StreamWriter(SetPath("sorted.txt"));
                for (int i = 0; i < dequeue.Size(); i++)
                {
                    sw.WriteLine(dequeue.Get(i));
                }
                sw.Close();
            }catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            int N =Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < dequeue.Size() ; i++)
            {
                if (CountOfSpace(dequeue.Get(i)) > N) dequeue.Remove(index: i);
            }
            for (int i = 0;i < dequeue.Size() ; i++) Console.WriteLine(dequeue.Get(i));
        }
    }
}
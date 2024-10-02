using System;
using MyLib;

namespace Task11
{
    class Project
    {

        static void Main(string[] args)
        {
            int[] arr = new int[] { 1, 4, 7, 0, 2, 5, 8, 3, 6, 9 };
            MyMaxHeep<int> heep = new MyMaxHeep<int>(arr);
            MyPriorityQueue<int> q = new MyPriorityQueue<int>(arr);
            Console.WriteLine(q.Contains(5, 1, 4, 9, 11));
            q.Remove(5, 3);
            Console.WriteLine("end");
        }
    }
}
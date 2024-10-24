using System;
using MyLib;

namespace Task16
{
    static class Project
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] {1,2,3,4,5,6,7,5};
            MyLinkedList<int> list = new MyLinkedList<int>(arr);
            Console.WriteLine(list.RemoveLastOccurrence(5));
        }
    }
}
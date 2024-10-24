using System;
using MyLib;

namespace Task18
{
    static class Project
    {

        static void Main(string[] args)
        {
            MyHashMap<int, int> map = new MyHashMap<int, int>();
            map.Push(1, 1);
            map.Push(2, 2);
            map.Push(3, 3);
            map.Push(4, 4);
            map.Push(5, 5);
            map.Push(3, 6);
            int[] keys = map.KeySet();
            (int, int)[] arr = map.EntrySet();
            Console.WriteLine(map.ContainKey(6));
        }
    }
}
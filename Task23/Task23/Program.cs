using MyLib;

namespace Task23
{
    static class Program
    {

        static void Main(string[] args)
        {
            MyHashSet<int> set = new MyHashSet<int>(new int[] { 1,3,5,7,9 });
            MyHashSet<int> set2 = new MyHashSet<int>(new int[] { 1,2, 4, 6, 8, 0 });

            MyHashSet<int> subset = set.SubSet(2, 6);
            MyHashSet<int> inter = set.Intersection(set2);
            string str = set.ToString();
            Console.WriteLine(set.EqualsSet(set2));
        }
    }
}
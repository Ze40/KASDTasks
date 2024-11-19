using MyLib;

namespace Task24
{

    class Program
    {

        static void Main(string[] args)
        {
            MyTreeSet<int> tree = new MyTreeSet<int>(new int[] {1,2,3,4,5,6,7, 34,8});
            int[] arr = tree.ToArray();
            foreach (int i in tree) Console.Write(i + " ");

        }
    }
}
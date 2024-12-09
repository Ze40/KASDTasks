using MyLib;

namespace Task28
{
    class Project
    {

        static void Main(string[] args)
        {
            MyTreeSet<int> ints = new MyTreeSet<int>(new int[] {13, 8, 17, 1, 11, 15, 25, 6, 22, 27});

            IMyIterator<int> it = ints.GetMyItr();

            while (it.HasNext())
            {
                Console.WriteLine(it.Current());
                it.Next();
            }

            Console.WriteLine("=====");
            foreach (int i in ints) Console.WriteLine(i);
        }
    }
}
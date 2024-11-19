using MyLib;

namespace Task21
{
    static class Project
    {

        static void Main(string[] args)
        {
            MyTreeMap<int,int> myTreeMap = new MyTreeMap<int,int>();
            myTreeMap.Put(22, 22);
            myTreeMap.Put(12, 12);
            myTreeMap.Put(30, 30);
            myTreeMap.Put(8, 8);
            myTreeMap.Put(17, 17);
            myTreeMap.Put(25, 9);
            myTreeMap.Put(32, 5);
            myTreeMap.Put(4, 11);
            myTreeMap.Put(9, 4);
            myTreeMap.Put(13, 4);
            myTreeMap.Put(18, 4);
            myTreeMap.Put(27, 4);
            myTreeMap.Remove(30);

            int[]set=myTreeMap.SubMap(5, 9);
            Console.WriteLine(myTreeMap.PollFirstEntry());
        }
    }
}
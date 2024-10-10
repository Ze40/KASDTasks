using MyLib;

namespace Task14
{
    static class Project
    {

        static void Main(string[] args)
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 65, 7};
            MyDequeue<int> dequeue = new MyDequeue<int>();
            Console.WriteLine(dequeue.PeekFirst());
            dequeue.Add(123, 2);
            dequeue.Print();
            dequeue.Remove(1, 65, 123);
            dequeue.Print();
            dequeue.Retain(2, 2);
            dequeue.Print();
            Console.WriteLine(dequeue.Contains(65, 4));
            Console.WriteLine(dequeue.Size());
        }
    }
}
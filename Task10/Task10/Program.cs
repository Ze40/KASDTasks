using System;
using MyLib;


namespace Task10
{

    public class Heap<T> where T : IComparable<T>
    {
        private MyVector<T> data;
        private void HeapifyDown(int index)
        {
            int leftChild = index * 2;
            int rightChild = index * 2 + 1;
            int smallestIndex = index;
            if (leftChild < data.Size() && data.Get(leftChild).CompareTo(data.Get(smallestIndex)) < 0) smallestIndex = leftChild;
            if (rightChild < data.Size() && data.Get(rightChild).CompareTo(data.Get(smallestIndex)) < 0) smallestIndex = rightChild;
            if (smallestIndex != index)
            {
                T temp = (T)data.Get(smallestIndex);
                data.Set(smallestIndex, data.Get(index));
                data.Set(index, temp);
                HeapifyDown(smallestIndex);
            }
        }
        private void HeapifyUp(int index)
        {
            int parentIndex = (int)(index/2);
            while (index > 1 && data.Get(parentIndex).CompareTo(data.Get(index)) > 0)
            {
                T temp = data.Get(parentIndex);
                data.Set(parentIndex, data.Get(index));
                data.Set(index, temp);
                index = parentIndex;
                parentIndex = (int)(index / 2);
            }
        }


        public Heap(T[] array)
        {
            this.data = new MyVector<T>(1,1);
            foreach (T item in array) this.Insert(item);
        }
        public void Insert(T key)
        {
            data.Add(key);
            HeapifyUp(data.Size() - 1);
        }
        public bool IsEmpty()
        {
            return data.IsEmpty();
        }
        public T ExtremumElement()
        {
            if (IsEmpty()) throw new InvalidOperationException("Куча пуста");
            T top = data.Get(1);
            data.Set(1, data.LastElement());
            HeapifyDown(1);

            return top;
        }
        public T ExtremumPop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Куча пуста");
            T top = data.Get(1);
            data.Set(1, data.LastElement());
            data.Remove(data.LastElement());
            HeapifyDown(1);

            return top;
        }
        public void Replace(int key, T value)
        {
            data.Set(key, value);
            HeapifyUp(key);
        } 
        public void Merge(Heap<T> heap)
        {
            for (int i = 0; i < heap.data.Size(); i++) this.Insert(heap.data.Get(i));
        }
    }

    class Project
    {

        static void Main(string[] args)
        {
            int[] arr = { 3, 2, 5, 3, 4, 5, 6, };
            Heap<int> heap = new Heap<int>(arr);
            heap.Replace(0, 1);
            Console.WriteLine(heap.ExtremumPop());
            Console.WriteLine(heap.ExtremumElement());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyPriorityQueue<T> : IMyQueue<T> where T : IComparable<T>
    {
        T[] queue;
        int size;
        double comparator;

        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index;
            int rightChild = 2 * index + 1;
            int biggest = index;
            if (leftChild <= size && queue[leftChild].CompareTo(queue[biggest]) > 0) biggest = leftChild;
            if (rightChild <= size && queue[rightChild].CompareTo(queue[biggest]) > 0) biggest = rightChild;
            if (biggest != index)
            {
                T temp = queue[biggest];
                queue[biggest] = queue[index];
                queue[index] = temp;
                HeapifyDown(biggest);
            }
        }
        private void HeapifiUp(int index)
        {
            int parent = index / 2;
            while (index > 1 && queue[parent].CompareTo(queue[index]) < 0)
            {
                T temp = queue[parent];
                queue[parent] = queue[index];
                queue[index] = temp;
                index = parent;
                parent = index / 2;
            }
        }

        public MyPriorityQueue()
        {
            queue = new T[11];
            size = 0;
            comparator = 10;
        }
        public MyPriorityQueue(T[] data)
        {
            this.queue = new T[data.Length + 1];
            this.Add(data);
            size = data.Length;
        }
        public MyPriorityQueue(IMyCollection<T> collection)
        {
            queue = collection.ToArray();
            size = collection.Size();
        }
        public MyPriorityQueue(int initialCapasity, int comparator = 1)
        {
            queue = new T[initialCapasity];
            this.comparator = comparator;
            size = 0;
        }
        public MyPriorityQueue(MyPriorityQueue<T> priorityQueue)
        {
            queue = priorityQueue.queue;
            size = priorityQueue.size;
            comparator = priorityQueue.comparator;
        }

        public void Add(params T[] data)
        {
            if (queue.Length + data.Length < 64) comparator = 2;
            else comparator = 1.5;

            if (queue.Length - 1 - data.Length <= size)
            {
                T[] newQueue = new T[(int)((size + data.Length) * comparator)];
                for (int i = 1; i <= size; i++) newQueue[i] = queue[i];
                queue = newQueue;
                for (int i = 0; i < data.Length; i++)
                {
                    queue[++size] = data[i];
                    HeapifiUp(size);
                }
                return;
            }

            for (int i = 0; i < data.Length; i++)
            {
                queue[++size] = data[i];
                HeapifiUp(size);
            }
        }
        public void Add(IMyCollection<T> collection)
        {
            Add(collection.ToArray());
        }
        public void Clear()
        {
            size = 0;
            queue = new T[11];
            comparator = 1;
        }
        public bool Contains(params T[] items)
        {
            foreach (T item in items)
            {
                if (item.CompareTo(queue[1]) > 0) return false;
                bool flag = false;
                for (int i = 1; i <= size; i++) if (item.Equals(queue[i])) flag = true;
                if (!flag) return false;
            }
            return true;
        }
        public bool Contains(IMyCollection<T> collection) { return Contains(collection.ToArray()); }
        public bool IsEmpty() { return size == 0; }
        public void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                int index = 1;
                while (index <= size)
                {
                    if (item.Equals(queue[index]))
                    {
                        queue[index] = queue[size--];
                        HeapifiUp(index);
                        HeapifyDown(index);
                    }
                    index++;
                }
            }
        }
        public void Remove(IMyCollection<T> collection)
        {
            Remove(collection.ToArray());
        }
        public void Retain(params T[] items)
        {
            foreach (T item in items)
            {
                for (int i = 1; i <= size; ++i)
                {
                    if (!item.Equals(queue[i])) Remove(queue[i]);
                }
            }
        }
        public void Retain(IMyCollection<T> collection)
        {
            Retain(collection.ToArray());
        }
        public int Size() { return size; }
        public T[] ToArray()
        {
            T[] array = new T[size];
            for (int i = 0; i < size; ++i) array[i] = queue[i + 1];
            return array;
        }
        public void ToArray(T[] array)
        {
            array = ToArray();
        }
        public T Peek() { return IsEmpty() ? default(T) : queue[1]; }
        public T Poll()
        {
            T element = queue[1];
            Remove(element);
            return element;
        }
        public T Element()
        {
            return queue[1];
        }
        public bool Offer(T value)
        {
            if (value == null) return false;
            Add(value);
            return true;
        }
    }
    public class MyPriorityQueueForArray<T> where T : IComparable<T>
    {
        T[][] queue;
        int size;
        int key;
        double comparator;

        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index;
            int rightChild = 2 * index + 1;
            int biggest = index;
            if (leftChild <= size && queue[leftChild][key].CompareTo(queue[biggest][key]) >= 0) biggest = leftChild;
            if (rightChild <= size && queue[rightChild][key].CompareTo(queue[biggest][key]) >= 0) biggest = rightChild;
            if (biggest != index)
            {
                T[] temp = queue[biggest];
                queue[biggest] = queue[index];
                queue[index] = temp;
                HeapifyDown(biggest);
            }
        }
        private void HeapifiUp(int index)
        {
            int parent = index / 2;
            while (index > 1 && queue[parent][key].CompareTo(queue[index][key]) < 0)
            {
                T[] temp = queue[parent];
                queue[parent] = queue[index];
                queue[index] = temp;
                index = parent;
                parent = index / 2;
            }
        }

        public MyPriorityQueueForArray()
        {
            queue = new T[11][];
            size = 0;
            comparator = 10;
            key = 0;
        }
        public MyPriorityQueueForArray(T[][] data)
        {
            this.queue = new T[data.Length + 1][];
            key = 0;
            this.Add(data);
            size = data.Length;
        }
        public MyPriorityQueueForArray(int initialCapasity, int comparator = 1)
        {
            queue = new T[initialCapasity][];
            key = 0;
            this.comparator = comparator;
            size = 0;
        }
        public MyPriorityQueueForArray(MyPriorityQueueForArray<T> priorityQueue)
        {
            queue = priorityQueue.queue;
            size = priorityQueue.size;
            comparator = priorityQueue.comparator;
            key = priorityQueue.key;
        }

        public void Add(params T[][] data)
        {
            if (queue.Length + data.Length < 64) comparator = 2;
            else comparator = 1.5;

            if (queue.Length - 1 - data.Length <= size)
            {
                T[][] newQueue = new T[(int)((size + data.Length) * comparator)][];
                for (int i = 1; i <= size; i++) newQueue[i] = queue[i];
                queue = newQueue;
                for (int i = 0; i < data.Length; i++)
                {
                    queue[++size] = data[i];
                    HeapifiUp(size);
                }
                return;
            }

            for (int i = 0; i < data.Length; i++)
            {
                queue[++size] = data[i];
                HeapifiUp(size);
            }
        }
        public int Size() { return size; }
        public bool Empty() { return size == 0; }
        public void Remove(int indexKey, params T[] items)
        {
            foreach (T item in items)
            {
                int index = 1;
                while (index <= size)
                {
                    if (item.Equals(queue[index][indexKey]))
                    {
                        queue[index] = queue[size--];
                        HeapifiUp(index);
                        HeapifyDown(index);
                        break;
                    }
                    index++;
                }
            }
        }
        public T[] Poll()
        {
            T[] element = queue[1];
            Remove(0, element[0]);
            return element;
        }
    }
}

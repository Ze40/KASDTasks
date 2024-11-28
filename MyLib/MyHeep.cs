using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyMinHeep<T> where T : IComparable<T>
    {
        T[] data;
        int size;

        public MyMinHeep()
        {
            data = new T[10];
            size = 0;
        }
        public MyMinHeep(T[] data)
        {
            this.data = new T[data.Length + 1];
            for (int i = 0; i < data.Length; i++) this.Insert(data[i]);
            size = data.Length;
        }

        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index;
            int rightChild = 2 * index + 1;
            int smallest = index;
            if (leftChild <= size && data[leftChild].CompareTo(data[smallest]) < 0) smallest = leftChild;
            if (rightChild <= size && data[rightChild].CompareTo(data[smallest]) < 0) smallest = rightChild;
            if (smallest != index)
            {
                T temp = data[smallest];
                data[smallest] = data[index];
                data[index] = temp;
                HeapifyDown(smallest);
            }
        }
        private void HeapifiUp(int index)
        {
            int parent = index / 2;
            while (index > 1 && data[parent].CompareTo(data[index]) > 0)
            {
                T temp = data[parent];
                data[parent] = data[index];
                data[index] = temp;
                index = parent;
                parent = index / 2;
            }
        }

        public void Insert(T key)
        {
            if (data.Length - 1 <= size)
            {
                T[] values = new T[size + 10];
                for (int i = 1; i <= size; i++) values[i] = data[i];
                values[++size] = key;
                data = values;
                HeapifiUp(size);
                return;
            }
            data[++size] = key;
            HeapifiUp(size);
        }
        public bool Empty()
        {
            return size == 0;
        }
        public T Min()
        {
            return data[1];
        }
        public T PopMin()
        {
            if (Empty()) throw new InvalidOperationException("Heeap is Empty");
            T min = data[1];
            for (int i = 1; i < size; i++) data[i] = data[i + 1];
            size--;
            HeapifyDown(1);
            return min;
        }
        public void ReplaceKey(int index, T key)
        {
            data[++index] = key;
            HeapifiUp(index);
            HeapifyDown(index);
        }
        public void Merge(MyMinHeep<T> heep)
        {
            for (int i = 1; i <= heep.size; i++) this.Insert(heep.data[i]);
        }
    }
    public class MyMaxHeep<T> where T : IComparable<T>
    {
        T[] data;
        int size;

        public MyMaxHeep()
        {
            data = new T[10];
            size = 0;
        }
        public MyMaxHeep(T[] data)
        {
            this.data = new T[data.Length + 1];
            for (int i = 0; i < data.Length; i++) this.Insert(data[i]);
            size = data.Length;
        }

        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index;
            int rightChild = 2 * index + 1;
            int biggest = index;
            if (leftChild <= size && data[leftChild].CompareTo(data[biggest]) > 0) biggest = leftChild;
            if (rightChild <= size && data[rightChild].CompareTo(data[biggest]) > 0) biggest = rightChild;
            if (biggest != index)
            {
                T temp = data[biggest];
                data[biggest] = data[index];
                data[index] = temp;
                HeapifyDown(biggest);
            }
        }
        private void HeapifiUp(int index)
        {
            int parent = index / 2;
            while (index > 1 && data[parent].CompareTo(data[index]) < 0)
            {
                T temp = data[parent];
                data[parent] = data[index];
                data[index] = temp;
                index = parent;
                parent = index / 2;
            }
        }

        public void Insert(T key)
        {
            if (data.Length - 1 <= size)
            {
                T[] values = new T[size + 10];
                for (int i = 1; i <= size; i++) values[i] = data[i];
                values[++size] = key;
                data = values;
                HeapifiUp(size);
                return;
            }
            data[++size] = key;
            HeapifiUp(size);
        }
        public bool Empty()
        {
            return size == 0;
        }
        public T Max()
        {
            return data[1];
        }
        public T PopMax()
        {
            if (Empty()) throw new InvalidOperationException("Heeap is Empty");
            T min = data[1];
            for (int i = 1; i < size; i++) data[i] = data[i + 1];
            size--;
            HeapifyDown(1);
            return min;
        }
        public void ReplaceKey(int index, T key)
        {
            data[++index] = key;
            HeapifiUp(index);
            HeapifyDown(index);
        }
        public void Merge(MyMaxHeep<T> heep)
        {
            for (int i = 1; i <= heep.size; i++) this.Insert(heep.data[i]);
        }
    }
}

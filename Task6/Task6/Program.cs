using System;

namespace Task5
{

    public class MyVector<T>
    {
        T[] elementData;
        int elementCount;
        int capacityIncrement;

        public MyVector()
        {
            elementData = null;
            elementCount = 10;
            capacityIncrement = 0;
        }
        public MyVector(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
            {
                elementData[i] = array[i];
            }
            elementCount = array.Length;
            capacityIncrement = 0;
        }
        public MyVector(int initialCapacity, int initialCapacityIncrement)
        {
            elementData = new T[initialCapacity];
            elementCount = initialCapacity;
            capacityIncrement = initialCapacityIncrement;
        }
        public void Add(T item)
        {
            if (elementCount == elementData.Length)
            {
                T[] newArray = null;
                if (capacityIncrement != 0) newArray = new T[elementCount+capacityIncrement];
                else newArray = new T[elementCount*2];
                for (int i = 0; i < elementCount; i++) newArray[i] = elementData[i];
                elementData = newArray;
            }
            elementData[elementCount] = item;
            elementCount++;
        }
        public void Add(params T[] array)
        {
            foreach (T item in array) Add(item:item);
        }
        public void Add(int index, params T[] array)
        {
            if (index > elementCount)
            {
                Add(array);
                return;
            }
            T[] newData = null;
            if (array.Length + elementCount > elementData.Length)
            {
                newData = new T[elementCount + array.Length];
                int i = 0, j = 0;
                while (i < newData.Length)
                {
                    if (i == index)
                    {
                        while (i < newData.Length && j < array.Length)
                        {
                            newData[i++] = array[j++];
                        }
                    }
                    if (i < newData.Length)
                    {
                        newData[i] = elementData[i - j];
                        i++;
                    }
                }
                elementData = newData;
                elementCount = newData.Length;
                return;
            }
            int k = 0;
            for (int i = index; i < elementData.Length; i++)
            {
                if (k < array.Length)
                {
                    elementData[i] = array[k];
                    k++;
                    continue;
                }
                elementData[i] = elementData[i + k];
            }
            elementCount += array.Length;
        }
        public void Clear()
        {
            elementData = null;
            elementCount = 0;
        }
        public bool Contains(params object[] array)
        {
            foreach (object item in array)
            {
                for (int i = 0; i < elementCount; i++)
                {
                    object element = elementData[i];
                    if (element.Equals(item)) return true;
                }
            }
            return false;
        }
        public bool IsEmpty()
        {
            return elementCount == 0;
        }
        public void Remove(params object[] obj)
        {
            foreach (object item in obj)
            {
                int i = 0;
                while (i < elementCount)
                {
                    if (item.Equals((object)elementData[i]))
                    {
                        for (int j = i; j < elementCount - 1; j++) elementData[j] = elementData[j + 1];
                        elementCount--;
                    }
                    i++; ;
                }
            }
        }
        public T Remove(int index)
        {
            if ((index < 0) || (index >= elementCount)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < elementCount - 1; i++) elementData[i] = elementData[i + 1];
            elementCount--;
            return element;
        }
        public void Retain(params object[] obj)
        {
            T[] newValue = new T[elementCount];
            int newSize = 0;
            for (int i = 0; i < elementCount; i++)
                foreach (object item in obj)
                    if (item.Equals(elementData[i]))
                    {
                        newValue[newSize] = elementData[i];
                        newSize++;
                    }
            elementCount = newSize;
            elementData = newValue;
        }
        public int Size() { return elementCount; }
        public T[] toArray()
        {
            T[] answerArray = new T[elementCount];
            for (int i = 0; i < elementCount; i++) answerArray[i] = elementData[i];
            return answerArray;
        }
        public void toArray(T[] array)
        {
            for (int i = 0; i < array.Length && i < elementCount; i++) array[i] = (T)elementData[i];
        }
        public T Get(int index)
        {
            if (index < 0 || index >= elementCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return elementData[index];
        }
        public int IndexOf(object element)
        {
            for (int i = 0; i < elementCount; i++) if (element.Equals(elementData[i])) return i;
            return -1;
        }
        public int LastIndexOf(object element)
        {
            int index = -1;
            for (int i = 0; i < elementCount; i++) if (element.Equals(elementData[i])) index = i;
            return index;
        }
        public void Set(int index, T element)
        {
            if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException("index");
            if (element == null) throw new ArgumentNullException(element.ToString());
            elementData[index] = element;
        }
        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= elementCount) throw new ArgumentOutOfRangeException("fromindex");
            if (toIndex < 0 || toIndex >= elementCount) throw new ArgumentOutOfRangeException("toindex");
            MyVector<T> result = new MyVector<T>(toIndex - fromIndex, 10);
            for (int i = 0; i < result.elementCount; i++)
            {
                result.Set(i, elementData[fromIndex + i]);
            }
            return result;
        }
        public ref T FirstElement()
        {
            return ref elementData[0];
        }
        public ref T LastElement()
        {
            return ref elementData[elementCount - 1];
        }
        public void RemoveElementAt(int pos) { T delElement = this.Remove(pos);}
        public void RemoveRange(int begin, int end)
        {
            if ((begin < 0) || (begin >= elementCount)) throw new ArgumentOutOfRangeException("begin out of range");
            if ((end < 0) || (end >= elementCount)) throw new ArgumentOutOfRangeException("end out of range");
            for (int i = begin; i < end; i++) { T delElement = this.Remove(i);}
        }

        public void Print()
        {
            for (int i = 0; i < elementCount; i++) Console.Write(elementData[i] + " ");
            Console.WriteLine();
        }
    }

    public static class Project
    {
        static void Main(string[] args)
        {
            MyVector<int> vector = new MyVector<int>(new int[] {1,2,3,4,5});
            vector.Add(1);
            vector.Print();
            vector.RemoveRange(1,2);
            vector.Print();
        }
    }
}
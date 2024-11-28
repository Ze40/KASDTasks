using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyVector<T>:IMyList<T>
    {
        T[] elementData;
        int elementCount;
        int capacityIncrement;

        public MyVector()
        {
            elementData = null;
            elementCount = 0;
            capacityIncrement = 10;
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
        public MyVector(IMyCollection<T> array)
        {
            elementData = array.ToArray();
            elementCount = array.Size();
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
            if (elementData == null)
            {
                elementData = new T[] { item };
                elementCount = 1;
                return;
            }
            if (elementCount == elementData.Length)
            {
                T[] newArray = null;
                if (capacityIncrement != 0) newArray = new T[elementCount + capacityIncrement];
                else newArray = new T[elementCount * 2];
                for (int i = 0; i < elementData.Length; i++) newArray[i] = elementData[i];
                elementData = newArray;
            }
            elementData[elementCount] = item;
            elementCount++;
        }
        public void Add(params T[] array)
        {
            foreach (T item in array) Add(item: item);
        }
        public void Add(IMyCollection<T> items)
        {
            Add(items.ToArray());
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
        public void Add(int index, IMyCollection<T> collection)
        {
            Add(index, collection.ToArray());
        }
        public void Clear()
        {
            elementData = null;
            elementCount = 0;
        }
        public bool Contains(params T[] array)
        {
            if (array==null) throw new ArgumentNullException("array is null");
            foreach (T item in array)
            {
                for (int i = 0; i < elementCount; i++)
                {
                    T element = elementData[i];
                    if (element.Equals(item)) return true;
                }
            }
            return false;
        }
        public bool Contains(IMyCollection<T> collection)
        {
            return Contains(collection.ToArray());
        }
        public bool IsEmpty()
        {
            return elementCount == 0;
        }
        public void Remove(params T[] obj)
        {
            foreach (T item in obj)
            {
                int i = 0;
                while (i < elementCount)
                {
                    if (item.Equals(elementData[i]))
                    {
                        for (int j = i; j < elementCount - 1; j++) elementData[j] = elementData[j + 1];
                        elementCount--;
                    }
                    i++; ;
                }
            }
        }
        public void Remove(IMyCollection<T> collection)
        {
            Remove(collection.ToArray());
        }
        public T Remove(int index)
        {
            if ((index < 0) || (index >= elementCount)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < elementCount - 1; i++) elementData[i] = elementData[i + 1];
            elementCount--;
            return element;
        }
        public void Retain(params T[] obj)
        {
            T[] newValue = new T[elementCount];
            int newSize = 0;
            for (int i = 0; i < elementCount; i++)
                foreach (T item in obj)
                    if (item.Equals(elementData[i]))
                    {
                        newValue[newSize] = elementData[i];
                        newSize++;
                    }
            elementCount = newSize;
            elementData = newValue;
        }
        public void Retain(IMyCollection<T> collection)
        {
            Retain(collection.ToArray());
        }
        public int Size() { return elementCount; }
        public T[] ToArray()
        {
            T[] answerArray = new T[elementCount];
            for (int i = 0; i < elementCount; i++) answerArray[i] = elementData[i];
            return answerArray;
        }
        public void ToArray(T[] array)
        {
            for (int i = 0; i < array.Length && i < elementCount; i++) array[i] = (T)elementData[i];
        }
        public T this[int index]
        {
            get
            {
                if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException("index out of range");
                return elementData[index];
            }
            set
            {
                if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException("index out of range");
                elementData[index] = value;
            }
        }
        public int IndexOf(T element)
        {
            for (int i = 0; i < elementCount; i++) if (element.Equals(elementData[i])) return i;
            return -1;
        }
        public int LastIndexOf(T element)
        {
            int index = -1;
            for (int i = 0; i < elementCount; i++) if (element.Equals(elementData[i])) index = i;
            return index;
        }
        public IMyList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= elementCount) throw new ArgumentOutOfRangeException("fromindex");
            if (toIndex < 0 || toIndex >= elementCount) throw new ArgumentOutOfRangeException("toindex");
            MyVector<T> result = new MyVector<T>(toIndex - fromIndex, 10);
            for (int i = 0; i < result.elementCount; i++)
            {
                result[i] = elementData[fromIndex + i];
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
        public void RemoveElementAt(int pos) { T delElement = this.Remove(pos); }
        public void RemoveRange(int begin, int end)
        {
            if ((begin < 0) || (begin >= elementCount)) throw new ArgumentOutOfRangeException("begin out of range");
            if ((end < 0) || (end >= elementCount)) throw new ArgumentOutOfRangeException("end out of range");
            for (int i = begin; i < end; i++) { T delElement = this.Remove(i); }
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < elementCount; i++)
            {
                yield return elementData[i];
            }
        }
        public IEnumerator<T> ListIterator(int index = 0)
        {
            for (; index < elementCount; index++) yield return elementData[index];
        }

        public void Print()
        {
            for (int i = 0; i < elementCount; i++) Console.Write(elementData[i] + " ");
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyHashSet<T> : IMySet<T> where T : IComparable<T>
    {
        private class SetElement
        {
            public T? value;
        }

        SetElement?[] table;
        int size;
        double loadFactor;

        public MyHashSet()
        {
            size = 0;
            table = new SetElement[16];
            loadFactor = 1.75;
        }
        public MyHashSet(IMyCollection<T> collection)
        {
            loadFactor = 1.75;
            size = 0;
            table = new SetElement[(int)(collection.Size()*loadFactor)];
            Add(collection.ToArray());
        }
        public MyHashSet(int initialCapacity, double loadFactor = 0.75)
        {
            table = new SetElement[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor+1;
        }


        private void ReSize()
        {
            SetElement[] newTable = new SetElement[(int)(table.Length*loadFactor)];
            foreach(SetElement element in table) {
                int backet = Math.Abs(element.value.GetHashCode()) % table.Length;
                for (int i = backet; i < table.Length; i++) if (newTable[i]==null) newTable[i] = element;
            }
        }
        private void Add(T item)
        {
            int backet = Math.Abs(item.GetHashCode()) % table.Length;
            for (int i = backet; i < table.Length; i++)
            {
                if (table[i]==null)
                {
                    size++;
                    table[i] = new SetElement() {value=item };
                    return;
                }
                if (table[i].value.Equals(item)) return;
            }
            ReSize();
            Add(item);
        }
        public void Add(params T[] items)
        {
            foreach(T item in items) Add(item: item);
        }
        public void Add(IMyCollection<T> collection)
        {
            Add(collection.ToArray());
        }
        public void Clear()
        {
            size = 0;
            table = new SetElement[16];
        }
        private bool Contains(T item)
        {
            int backet = Math.Abs(item.GetHashCode()) % table.Length;
            for (int i = backet;i < table.Length; i++)
            {
                if (table[i] == null) return false;
                if (table[i].value.Equals(item)) return true;
            }
            return false;
        }
        public bool Contains(params T[] items)
        {
            foreach (T item in items)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }
        public bool Contains(IMyCollection<T> collection)
        {
            return Contains(collection.ToArray());
        }
        public bool IsEmpty() => size == 0;
        private void Remove(T item)
        {
            int backet = Math.Abs(item.GetHashCode()) % table.Length;
            for (int i = backet; i<table.Length; i++)
            {
                if (table[i]==null) return;
                if (table[i].value.Equals(item))
                {   
                    table[i] = null;
                    return;
                }
            }
        }
        public void Remove(params T[] items) { 
            foreach (T item in items) Remove(item);
        }
        public void Remove(IMyCollection<T> collection)
        {
            Remove(collection.ToArray());
        }
        public void Retain(params T[] items)
        {
            SetElement[] answer = new SetElement[items.Length];
            foreach(T item in items)
            {
                int backet = Math.Abs(item.GetHashCode()) % table.Length;
                for (int i =backet; i<table.Length && table[i] != null;i++)
                {
                    if (table[i].value.Equals(item)) answer[backet] = table[i];
                }
            }
            table = answer;
        }
        public void Retain(IMyCollection<T> collection)
        {
            Retain(collection.ToArray());
        }
        public int Size() => size;
        public T[] ToArray()
        {
            T[] result = new T[size];
            int index = 0;
            for (int i =0; i<table.Length; i++)
            {
                if (table[i] != null)
                {
                    result[index++] = table[i].value;
                }
            }
            return result;
        }
        public void ToArray(T[] array)
        {
            array = new T[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    array[index++] = table[i].value;
                }
            }
        }
        public T First()
        {
            if (IsEmpty()) throw new Exception("Set is empty");
            T min = default(T);
            bool isMin = false;
            for (int i =0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    if (!isMin)
                    {
                        min = table[i].value;
                        isMin = true;
                        continue;
                    }
                    if (min.CompareTo(table[i].value) > 0) min = table[i].value;
                }
            }
            return min;
        }
        public T Last()
        {
            if (IsEmpty()) throw new Exception("Set is empty");
            T max = default(T);
            bool isMax = false;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    if (!isMax)
                    {
                        max = table[i].value;
                        isMax = true;
                        continue;
                    }
                    if (max.CompareTo(table[i].value) < 0) max = table[i].value;
                }
            }
            return max;
        }
        public IMySet<T> SubSet(T from, T to)
        {
            MyHashSet<T> result = new MyHashSet<T>(table.Length);
            for (int i =0; i<table.Length; i++)
            {
                if (table[i] != null && table[i].value.CompareTo(from)>=0 && table[i].value.CompareTo(to)<=0) result.Add(table[i].value);
            }
            return result;
        }
        public IMySet<T> HeadSet(T to)
        {
            MyHashSet<T> result = new MyHashSet<T>(table.Length);
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && table[i].value.CompareTo(to) <= 0) result.Add(table[i].value);
            }
            return result;
        }
        public IMySet<T> TailSet(T from)
        {
            MyHashSet<T> result = new MyHashSet<T>(table.Length);
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && table[i].value.CompareTo(from) >= 0) result.Add(table[i].value);
            }
            return result;
        }

    }
}

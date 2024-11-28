using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyLinkedList<T> : IMyList<T> where T : IComparable<T>
    {
        private class LinkedListElement<T> where T : IComparable<T>
        {
            public T key;
            public LinkedListElement<T>? next;
            public LinkedListElement<T>? previous;

            public LinkedListElement(T key)
            {
                this.key = key;
                this.next = null;
                this.previous = null;
            }
         }

        LinkedListElement<T>? first;
        LinkedListElement<T>? last;
        int size;

        public MyLinkedList() {
            first = null;
            last = null;
            size = 0;
        }
        public MyLinkedList(params T[] values)  
        {
            first = new LinkedListElement<T>(values[0]);
            last = first;
            LinkedListElement<T>? previous = first;
            int i = 1;
            for (i = 1; i < values.Length; i++)
            {
                LinkedListElement<T> newElement = new LinkedListElement<T>(values[i]);
                newElement.previous = previous;
                previous.next = newElement;
                previous = newElement;
                last = newElement;
            }
            size = i;
        }
        public MyLinkedList(IMyCollection<T> collection)
        {
            T[] values = collection.ToArray();
            first = new LinkedListElement<T>(values[0]);
            last = first;
            LinkedListElement<T>? previous = first;
            int i = 1;
            for (i = 1; i < values.Length; i++)
            {
                LinkedListElement<T> newElement = new LinkedListElement<T>(values[i]);
                newElement.previous = previous;
                previous.next = newElement;
                previous = newElement;
                last = newElement;
            }
            size = i;
        }

        public void Add(params T[] values)
        {
            LinkedListElement<T> curr = last;
            foreach(T t in values)
            {
                curr.next = new LinkedListElement<T>(t);
                curr.next.previous = curr;
                curr = curr.next;
                size++;
            }
        }
        public void Add(IMyCollection<T> collection)
        {
            Add(collection.ToArray());
        }
        public void AddEnd(params T[] value) {
            if (size == 0)
            {
                first = new LinkedListElement<T>(value[0]);
                last = first;
                LinkedListElement<T>? previous = first;
                int i = 1;
                for (i = 1; i < value.Length; i++)
                {
                    LinkedListElement<T> newElement = new LinkedListElement<T>(value[i]);
                    newElement.previous = previous;
                    previous.next = newElement;
                    previous = newElement;
                    last = newElement;
                }
                size = i;
                return;
            }
            foreach(T t in value) { 
                LinkedListElement<T> newElement = new LinkedListElement<T>(t);
                last.next = newElement;
                newElement.previous = last;
                last = newElement;
                size++;
            }
        }
        public void AddBegin(params T[] value)
        {
            if (size == 0)
            {
                first = new LinkedListElement<T>(value[0]);
                last = first;
                LinkedListElement<T>? next = last;
                int i = 1;
                for (i = 1; i < value.Length; i++)
                {
                    LinkedListElement<T> newElement = new LinkedListElement<T>(value[i]);
                    newElement.next = next;
                    next.previous = newElement;
                    next = newElement;
                    first = newElement;
                }
                size = i;
                return;
            }
            foreach (T t in value)
            {
                LinkedListElement<T> newElement = new LinkedListElement<T>(t);
                first.previous = newElement;
                newElement.next = first;
                first = newElement;
                size++;
            }
        }
        public void Add(int index, params T[] values)
        {
            if (size <= index) throw new IndexOutOfRangeException("index out of range");
            int i = 0;
            LinkedListElement<T> current = first;
            while(current.next != null)
            {
                if (++i == index)
                {
                    foreach (T key in values)
                    {
                        LinkedListElement<T> newElement = new LinkedListElement<T>(key);
                        newElement.next = current.next;
                        newElement.previous = current;
                        current.next.previous = newElement;
                        current.next = newElement;
                        current = newElement;
                        size++;
                    }
                    return;
                }
                current = current.next;
            }

        }
        public void Add(int index, IMyCollection<T> collection)
        {
            Add(index, collection.ToArray());
        }
        public void Clear()
        {
            size = 0;
            first = null;
            last = null;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public bool Contains(params T[] keys)
        {
            if (IsEmpty()) return false;
            foreach (T key in keys)
            {
                LinkedListElement<T>? current = first;
                bool isContain = false;
                while (current != last && !isContain) { 
                    if (current.key.Equals(key)) isContain = true;
                    current = current.next;
                }
                if (last.key.Equals(key)) isContain = true;
                if (!isContain) return false;
            }
            return true;
        }
        public bool Contains(IMyCollection<T> collection)
        {
            return Contains(collection.ToArray());
        }
        public void Remove(params T[] keys) {
            foreach (T key in keys)
            {
                if (IsEmpty()) return;
                if (first.key.Equals(key))
                {
                    if (size == 1)
                    {
                        first = last = null;
                        size = 0;
                        return;
                    }
                    else
                    {
                        first.next.previous = null;
                        first = first.next;
                    }
                    size--;
                }
                LinkedListElement<T>? current = first;
                while (current != last && current != null)
                {
                    if (current.key.Equals(key))
                    {
                        current.previous.next = current.next;
                        current.next.previous = current.previous;
                        size--;
                    }
                    current = current.next;
                }

                if (last.key.Equals(key))
                {
                    if (size == 1)
                    {
                        last = first = null;
                        size = 0;
                        return;
                    }else
                    {
                        last.previous.next = null;
                        last = last.previous;
                    }
                    size--;
                }
            }
        }
        public void Remove(IMyCollection<T> collection)
        {
            Remove(collection.ToArray());
        }
        public T Remove(int index)
        {
            if (0 > index || index >= size) throw new IndexOutOfRangeException("index out of range");
            T answer = first.key;
            if (index == 0)
            {
                answer = first.key;
                if (size == 1)
                {
                    first = last = null;
                    size = 0;
                    return answer;
                }
                first.next.previous = null;
                first = first.next;
                size--;
                return answer;
            }
            if (index == size -1)
            {
                answer = last.key;
                if (size == 1)
                {
                    last = first = null;
                    size = 0;
                    return answer;
                }
                last.previous.next = null;
                last = last.previous;
                size--;
                return answer;
            }
            int i = 0;
            LinkedListElement<T> current = first;
            while (current.next != null)
            {
                if (i == index)
                {
                    answer = current.key;
                    current.previous.next = current.next;
                    current.next.previous = current.previous;
                    size--;
                    break;
                }
                i++;
                current = current.next;
            }
            return answer;
        }
        public T RemoveLast()
        {
            if (IsEmpty()) throw new InvalidOperationException("list is empty");
            T key = last.key;
            if (size == 1)
            {
                last = first = null;
                size = 0;
                return key;
            }
            last.previous.next = null;
            last = last.previous;
            size--;
            return key;
        }
        public T RemoveFirst()
        {
            if (IsEmpty()) throw new InvalidOperationException("list is empty");
            T key = first.key;
            if (size == 1)
            {;
                first = last = null;
                size = 0;
                return key;
            }
            first.next.previous = null;
            first = first.next;
            return key;
        }
        public T PollFirst()
        {
            if (IsEmpty()) return default(T);
            return RemoveFirst();
        }
        public T PollLast()
        {
            if (IsEmpty()) return default(T);
            return RemoveLast();
        }
        public bool RemoveLastOccurrence(T obj)
        {
            if (IsEmpty()) return false;
            if (last.key.Equals(obj))
            {
                if (size == 1)
                {
                    first = last = null;
                    size = 0;
                    return true;
                }
                last.previous.next = null;
                last = last.previous;
                return true;
            }
            
            LinkedListElement<T> current = last.previous;
            while (current.previous != null)
            {
                if (current.key.Equals(obj))
                {
                    current.previous.next = current.next;
                    current.next.previous = current.previous;
                    return true;
                }
                current = current.previous;
            }
            if (first.key.Equals(obj))
            {
                if (size == 1)
                {
                    first = last = null;
                    size = 0;
                    return true;
                }
                first.next.previous = null;
                first = first.next;
                return true;
            }
            return false;
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            if (IsEmpty()) return false;
            if (first.key.Equals(obj))
            {
                if (size == 1)
                {
                    first = last = null;
                    size = 0;
                    return true;
                }
                first.next.previous = null;
                first = first.next;
                return true;
            }

            LinkedListElement<T> current = first.next;
            while (current.next != null)
            {
                if (current.key.Equals(obj))
                {
                    current.previous.next = current.next;
                    current.next.previous = current.previous;
                    return true;
                }
                current = current.next;
            }

            if (last.key.Equals(obj))
            {
                if (size == 1)
                {
                    first = last = null;
                    size = 0;
                    return true;
                }
                last.previous.next = null;
                last = last.previous;
                return true;
            }
            return false;
        }
        public void Retain(params T[] keys)
        {
            MyLinkedList<T> retainList = new MyLinkedList<T>();
            foreach (T key in keys)
            {
                LinkedListElement<T>? current = first;
                while (current != null)
                {
                    if (current.key.Equals(key)) retainList.AddEnd(key);
                    current = current.next;
                }
            }
            first = retainList.first;
            last = retainList.last;
            size = retainList.size;
        }
        public void Retain(IMyCollection<T> collection)
        {
            Retain(collection.ToArray());
        }
        public int Size() { return size; }
        public T[] ToArray()
        {
            if (size == 0) return new T[0];
            T[] array = new T[size];
            LinkedListElement<T> current = first;
            int index = 0;
            while(current != null)
            {
                array[index] = current.key;
                index++;
            }
            return array;
        }
        public void ToArray(T[] array)
        {
            if (size == 0)
            {
                array = new T[0];
                return;
            }
            T[] newArray = new T[size];
            LinkedListElement<T> current = first;
            int index = 0;
            while (current != null)
            {
                newArray[index] = current.key;
                index++;
            }
            array = newArray;
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size) throw new IndexOutOfRangeException("index out of range");
                LinkedListElement<T> curr = first;
                int i = 0;
                while (curr != null)
                {
                    if (i == index) return curr.key;
                    curr = curr.next;
                    i++;
                }
                return curr.key;
            }
            set
            {
                if (index < 0 || index >= size) throw new IndexOutOfRangeException("index out of range");
                LinkedListElement<T> curr = first;
                int i = 0;
                while (curr != null)
                {
                    if (i == index)
                    {
                        curr.key = value;
                        return;
                    }
                    curr = curr.next;
                    i++;
                }
                curr.key = value;
            }
        }
        public T GetFirst() {
            if (IsEmpty()) throw new Exception("List is empty");
            return first.key;
        }
        public T GetLast() { 
            if (IsEmpty()) throw new Exception("List is empty");
            return last.key;
        }
        public T PeekFirst()
        {
            if (IsEmpty()) return default(T);
            return GetFirst();
        }
        public T PeekLast()
        {
            if (IsEmpty()) return default(T);
            return GetLast();
        }
        public int IndexOf(T item)
        {
            if (IsEmpty()) return -1;
            LinkedListElement<T> current = first;
            int index = 0;
            while (current != null)
            {
                if (current.key.Equals(item)) return index;
                index++;
                current = current.next;
            }
            return -1;
        }
        public int LastIndexOf(T item)
        {
            if (IsEmpty()) return -1;
            LinkedListElement<T> current = first;
            int index = 0;
            int lastIndex = -1;
            while (current != null)
            {
                if (current.key.Equals(item)) lastIndex = index;
                index++;
                current = current.next;
            }
            return lastIndex;
        }
        public IMyList<T> SubList(int indexFrom, int indexTo)
        {
            if (indexFrom < 0 || indexTo < 0 || indexFrom >= size || indexTo > size) throw new ArgumentOutOfRangeException("index out of range in SubList()");
            LinkedListElement<T> current = first;
            int index = 0;
            MyLinkedList<T> newList = new MyLinkedList<T>();
            while (current != null && index < indexTo)
            {
                if (index >= indexFrom)
                {
                    newList.AddEnd(current.key);
                }
                current = current.next;
                index++;
            }
            return newList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListElement<T> curr = first;
            while (curr != null)
            {
                yield return curr.key;
                curr = curr.next;
            }
        }
        public IEnumerator<T> ListIterator(int index = 0)
        {
            LinkedListElement<T> curr = first;
            int i = 0;
            while (curr != null)
            {
                if (i >= index ) yield return curr.key;
                curr = curr.next;
                i++;
            }
        }
    }
}

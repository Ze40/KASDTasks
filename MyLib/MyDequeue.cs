using System.Drawing;

namespace MyLib
{
    public class MyDequeue<T> : IMyDequeue<T>
    {
        T[] elements;
        int head;
        int tail;

        public MyDequeue()
        {
            elements = new T[16];
            tail = -1;
            head = -1;
        }
        public MyDequeue(T[] elements)
        {
            this.elements = elements;
            tail = 0;
            head = elements.Length - 1;
        }
        public MyDequeue(IMyCollection<T> collection)
        {
            elements = collection.ToArray();
            tail = 0;
            head = elements.Length - 1;
        }
        public MyDequeue(int countOfElements)
        {
            elements = new T[countOfElements];
            tail = -1;
            head = -1;
        }

        public bool IsEmpty() { return head == -1 || head < tail; }
        public void Add(T element)
        {
            if (head == elements.Length-1)
            {
                T[] newElements = new T[(int)((head - tail + 2) * 1.3)];
                int index = 0;
                for (int i = tail; i <= head; i++) newElements[index++] = elements[i];
                newElements[index] = element;
                tail = 0;
                head = index;
                elements = newElements;
                return;
            }
            if (IsEmpty())
            {
                tail = head = 0;
                elements[0] = element;
                return;
            }
            elements[++head] = element;
        }
        public void Add(params T[] elements)
        {
            foreach(T element in elements) Add(element);
        }
        public void Add(IMyCollection<T> collection)
        {
            Add(collection.ToArray());
        }
        public void Add(int index, params T[] element)
        {
            int k = 0, t = 0;
            T[] ans = new T[element.Length + Size()];
            for (int i = tail; i <= head; i++) {
                while (i == index - tail && t < element.Length)
                {
                    ans[k++] = element[t];
                }
                ans[k++] = elements[i];
            }
        }
        public void Add(int index, IMyCollection<T> collection)
        {
            Add(index, collection.ToArray());
        }
        public void Clear()
        {
            elements = new T[16];
            tail = -1;
            head = -1;
        }
        public bool Contains(params T[] elements)
        {
            foreach(T element in elements)
            {
                bool isContain = false;
                for (int i = tail; i <= head;i++)
                {
                    if (this.elements[i].Equals(element))
                    {
                        isContain = true; 
                        break;
                    }
                }
                if (!isContain) return false;
            }
            return true;
        }
        public bool Contains(IMyCollection<T> collection)
        {
            return Contains(collection.ToArray());
        }
        public void Remove(T element)
        {
            for (int i = tail; i<= head;i++)
            {
                if (element.Equals(elements[i]))
                {
                    for (int j = i; j < head;j++)
                    {
                        elements[j] = elements[j + 1];
                    }
                    head--;
                }
            }
        }
        public T Remove(int index)
        {
            if (tail > index || index > head) throw new IndexOutOfRangeException("Index out of range");
            T ans = this[index];
            for (int i = tail+index; i < head;i++) elements[i] = elements[i + 1];
            head--;
            return ans;
        }
        public void Remove(params T[] elements)
        {
            foreach (T element in elements) Remove(element);
        }
        public void Remove(IMyCollection<T> collection) { Remove(collection.ToArray()); }
        public void Retain(params T[] elements)
        {
            T[] newElements = new T[head-tail+2];
            int index = 0;

            foreach(T element in elements)
            {
                if (newElements.Contains(element)) continue;
                for (int i = tail; i <= head; i++)
                {
                    if (element.Equals(this.elements[i])) newElements[index++] = element;
                }
            }
            head = index-1;
            tail = 0;
            this.elements = newElements;
        }
        public void Retain(IMyCollection<T> collection)
        {
            Retain(collection.ToArray());
        }
        public int Size() {  return head-tail+1; }
        public T[] ToArray()
        {
            T[] array = new T[Size()];
            int index = 0;
            for (int i = tail; i <= head;i++) array[index++] = this.elements[i];
            return array;
        }
        public void ToArray(T[] array)
        {
            array = ToArray();
        }
        public ref T Element() {
            if (IsEmpty()) throw new InvalidOperationException("Deq is empty");
            return ref this.elements[head]; 
        }
        public void AddFirst(T element)
        {
            Add(element);
        }
        public void AddLast(T element)
        {
            if (IsEmpty()) { throw new InvalidOperationException("Deq is empty"); };
            if (tail == 0)
            {
                T[] newElements = new T[Size()+1];
                newElements[0] = element;
                int index = 1;
                for (int i = tail; i <= head; i++) newElements[index++] = elements[i];
                tail = 0;
                head = index-1;
                elements = newElements;
                return;
            }
            elements[--tail] = element;
        }
        public T GetFirst() {
            return PeekFirst();
        }
        public int IndexOf(T element)
        {
            for (int i = 0; i< Size(); i++)
            {
                if (this[i].Equals(element)) return i;
            }
            return -1;
        }
        public int LastIndexOf(T element)
        {
            for (int i = Size()-1; i >= 0; i--)
            {
                if (this[i].Equals(element)) return i;
            }
            return -1;
        }
        public T GetLast() { return PeekLast(); }
        public T this[int index]
        {
            get
            {
                if (IsEmpty()) throw new InvalidOperationException("Deq is empty");
                if (index < tail || index > head) throw new IndexOutOfRangeException("Index out of range");
                return elements[tail + index];
            }
            set
            {
                if (IsEmpty()) throw new InvalidOperationException("Deq is empty");
                if (index < tail || index > head) throw new IndexOutOfRangeException("Index out of range");
                elements[tail + index] = value;
            }
        }
        public bool OfferLast(object obj)
        {
            if (obj == null) throw new ArgumentNullException("Null object");
            if (obj is T) { AddLast((T)obj); return true; }
            return false;
        }
        public bool OfferFirst(object obj)
        {
            if (obj == null) throw new ArgumentNullException("Null object");
            if (obj is T) { AddFirst((T)obj); return true; }
            return false;
        }
        public T PeekFirst()
        {
            if (IsEmpty()) return default(T);
            return elements[head];
        }
        public T PeekLast()
        {
            if (IsEmpty()) return default(T);
            return elements[tail];
        }
        public T PollFirst()
        {
            T? res = PeekFirst();
            head--;
            return res;
        }
        public T PollLast()
        {
            T? res = PeekLast();
            tail++;
            return res;
        }
        public T RemoveLast()
        {
            if (IsEmpty()) throw new EndOfStreamException("deq is empty");
            head--;
            return elements[head+1];
        }
        public T RemoveFirst()
        {
            if (IsEmpty()) throw new EndOfStreamException("deq is empty");
            tail++;
            return elements[tail-1];
        }
        public bool RemoveLastOccurrence(object obj)
        {
            if (obj == null) return false;
            if (obj is not T) return false;
            for (int i = head; i >= tail; i--)
            {
                if (elements[i].Equals(obj))
                {
                    for (int j = i; j < head; j++)
                    {
                        elements[j] = elements[j + 1];
                    }
                    return true;
                }
            }
            return false;
        }
        public bool RemoveFirstOccurrence(object obj)
        {
            if (obj == null) return false;
            if (obj is not T) return false;
            for (int i = tail; i <= head; i++)
            {
                if (elements[i].Equals(obj))
                {
                    for (int j = i; j < head; j++)
                    {
                        elements[j] = elements[j + 1];
                    }
                    return true;
                }
            }
            return false;
        }
        public T Pop()
        {
            if (IsEmpty()) throw new EndOfStreamException("deq is empty");
            head--;
            return elements[head+1];
        }
        public void Push(T item)
        {
            Add(item);
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = tail; i <- head; i++)
            {
                yield return elements[i];
            }
        }
        public IEnumerator<T> ListIterator(int index = 0)
        {
            if (index + tail > head || index < 0) throw new IndexOutOfRangeException("index out of range"); 
            for (int i = tail+index; i < -head; i++)
            {
                yield return elements[i];
            }
        }
        public IMyList<T> SubList(int indexFrom, int indexTo)
        {
            if (indexFrom < 0 || indexFrom + tail > head) throw new ArgumentOutOfRangeException("fromindex is out of range");
            if (indexTo < 0 || indexTo + tail > head) throw new ArgumentOutOfRangeException("indexTo is out of range");
            MyDequeue<T> result = new MyDequeue<T>(indexTo - indexFrom);
            for (int i = 0; i < indexTo; i++)
            {
                result[i] = elements[indexFrom + i];
            }
            return result;
        }
        public void Print()
        {
            for (int i = tail; i <= head; i++)
            {
                Console.Write(elements[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
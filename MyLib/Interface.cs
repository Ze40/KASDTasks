using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public interface IMyCollection<T>
    {
        void Add(params T[] items);
        void Add(IMyCollection<T> items);
        void Clear();
        bool Contains(params T[] items);
        bool Contains(IMyCollection<T> items);
        bool IsEmpty();
        void Remove(params T[] items);
        void Remove(IMyCollection<T> items);
        void Retain(params T[] items);
        void Retain(IMyCollection<T> items);
        int Size();
        T[] ToArray();
        void ToArray(T[] array);
    }

    public interface IMyList<T> : IMyCollection<T>
    {
        void Add(int index, params T[] items);
        void Add(int index, IMyCollection<T> items);
        T this[int index]
        {
            get;
            set;
        }
        int IndexOf(T item);
        int LastIndexOf(T item);
        IEnumerator<T> ListIterator(int index = 0);
        T Remove(int index);

        //HERE!!!
        IMyList<T> SubList(int indexFrom, int indexTo);
    }
    public interface IMyQueue<T> : IMyCollection<T>
    {
        T Peek();
        T Poll();
        bool Offer(T element);
        T Element();
    }
    public interface IMyDequeue<T>:IMyList<T>
    {
        void AddFirst(T element);
        void AddLast(T element);
        T GetFirst();
        T GetLast();
        bool OfferFirst(object element);
        bool OfferLast(object element);
        T Pop();
        void Push(T element);
        T PeekFirst();
        T PeekLast();
        T PollFirst();
        T PollLast();
        T RemoveFirst();
        T RemoveLast();
        bool RemoveLastOccurrence(object obj);
        bool RemoveFirstOccurrence(object obj);
    }
    public interface IMySet<T> : IMyCollection<T>
    {
        T First();
        T Last();
        IMySet<T> SubSet(T fromElement, T toElement);
        IMySet<T> HeadSet(T toElement);
        IMySet<T> TailSet(T fromElement);
    }
    public interface IMyNavigableSet<T> : IMySet<T>
    {
        T Lower(T element);
        T Floor(T element);
        T Higher(T element);
        T Ceiling(T element);
        T PollFirst();
        T PollLast();
        IMySet<T> SubSet(T from, T to, bool fromIn = false, bool toIn = false);
    }
    public interface IMyMap<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        void Clear();
        bool ContainsKey(K key);
        bool ContainsValue(V value);
        (K, V)[] EntrySet();
        V Get(K key);
        bool IsEmpty();
        K[] KeySet();
        void Push(K key, V value);
        void PushAll(IMyMap<K, V> map);
        void Remove(K key);
        int Size();
        IMyCollection<V> Values();
    }
    public interface IMySortedMap<K, V> : IMyMap<K, V>
    {
        K FirstKey();
        K LastKey();
        IMySortedMap<K,V> HeadMap(K end);
        IMySortedMap<K, V> TailMap(K start);
        IMySortedMap<K, V> SubMap(K start, K end);

    }
    public interface IMyNavigableMap<K, V> : IMySortedMap<K,V> {
        public (K, V)[] LowerEntry(K key);
        public (K, V)[] FloorEntry(K key);
        public (K, V)[] HigherEntry(K key);
        public (K, V)[] CeilingEntry(K key);
        public K LowerKey(K key);
        public K FloorKey(K key);
        public K HigherKey(K key);
        public K CeilingKey(K key);
        public (K, V) PollFirstEntry();
        public (K, V) PollLastEntry();
        public (K, V) FirstEntry();
        public (K, V) LastEntry();


    }
}

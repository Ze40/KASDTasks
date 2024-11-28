using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLib
{
    public class MyTreeSet<T> : IMyNavigableSet<T> where T : IComparable
    {
        RBTree<T, object> tree;
        int size;
        Func<T?, T?, int>? comparator;

        private int Compare(T? first, T? second)
        {
            if (first == null) throw new ArgumentNullException("first element is null");
            if (second == null) throw new ArgumentNullException("second element is null");
            return first.CompareTo(second);
        }

        public MyTreeSet()
        {
            tree = new RBTree<T, object>();
            size = 0;
            comparator = Compare;
        }
        public MyTreeSet(RBTree<T, object> map)
        {
            tree = map;
            size = map.Size();
            comparator = Compare;
        }
        public MyTreeSet(Func<T?, T?, int> comparator)
        {
            this.comparator = comparator;
            tree = new RBTree<T, object>();
            size = 0;
        }
        public MyTreeSet(IMyCollection<T> collection)
        {
            tree = new RBTree<T, object>();
            foreach (T item in collection.ToArray()) tree.Insert(item, 0);
            size = tree.Size();
            comparator = Compare;
        }
        public MyTreeSet(params T[] array)
        {
            tree = new RBTree<T, object>();
            foreach (T item in array) tree.Insert(item, 0);
            size = tree.Size();
            comparator = Compare;
        }

        public void Add(params T[] items) {
            foreach (T item in items) tree.Insert(item, 0);
            size = tree.Size();
        }
        public void Add(IMyCollection<T> collection)
        {
            Add(collection.ToArray());
        }
        public void Clear()
        {
            tree = new RBTree<T, object>();
            size = 0;
        }
        public bool Contains(params T[] items) {
            foreach(T item in items) if (!tree.IsContain(item)) return false;
            return true;
        }
        public bool Contains(IMyCollection<T> collection)
        {
            return Contains(collection.ToArray());
        }
        public bool IsEmpty() => size == 0;
        public void Remove(params T[] items) { 
            foreach(T item in items) tree.Remove(item);
            size = tree.Size();
        }
        public void Remove(IMyCollection<T> collection)
        {
            Remove(collection.ToArray());
        }
        public void Retain(params T[] items)
        {
            RBTree<T, object> newMap = new RBTree<T, object>();
            foreach (T item in items) newMap.Insert(item, 0);
            tree = new RBTree<T, object>();
            size = tree.Size();
        }
        public void Retain(IMyCollection<T> collection)
        {
            Retain(collection.ToArray());
        }
        public int Size()=>tree.Size();
        public T[] ToArray()
        {
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            T[] answer = new T[size];
            int index = 0;
            stack.Push(node);
            
            while (!stack.Empty())
            {
                while (node.Left() != nill)
                {
                    node = node.Left();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                answer[index++] = node.key;

                if (node.Right() != nill)
                {
                    stack.Push(node.Right());
                    node = node.right;
                }
            }
            return answer;
        }
        public void ToArray(T[] array)
        {
            array = ToArray();
        }
        public T First()
        {
            RBTree<T, object>.TreeElement node = tree.Root();
            if (node == null || node == tree.Nill()) throw new Exception("Empty");
            while(node.right != tree.Nill()) node = node.Right();
            return node.key;
        }
        public T Last()
        {
            RBTree<T, object>.TreeElement node = tree.Root();
            if (node == null || node == tree.Nill()) throw new Exception("Empty");
            while (node.left != tree.Nill()) node = node.Left();
            return node.key;
        }
        public IMySet<T> SubSet(T from, T to, bool fromIn = false, bool toIn = false)
        {
            MyTreeSet<T> answer = new MyTreeSet<T>();
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            stack.Push(node);

            if (fromIn && toIn)
            {
                while (!stack.Empty())
                {
                    while (node.Left() != nill)
                    {
                        node = node.Left();
                        stack.Push(node);
                    }
                    node = stack.Peek();
                    stack.Pop();
                    if (comparator(node.key, from) >=  0 && comparator(node.key, to)<=0) answer.Add(node.key); 

                    if (node.Right() != nill)
                    {
                        stack.Push(node.Right());
                        node = node.right;
                    }
                }
                return answer;
            }
            if (fromIn && !toIn)
            {
                while (!stack.Empty())
                {
                    while (node.Left() != nill)
                    {
                        node = node.Left();
                        stack.Push(node);
                    }
                    node = stack.Peek();
                    stack.Pop();
                    if (comparator(node.key, from) >= 0 && comparator(node.key, to) < 0) answer.Add(node.key);

                    if (node.Right() != nill)
                    {
                        stack.Push(node.Right());
                        node = node.right;
                    }
                }
                return answer;
            }
            if (!fromIn && toIn)
            {
                while (!stack.Empty())
                {
                    while (node.Left() != nill)
                    {
                        node = node.Left();
                        stack.Push(node);
                    }
                    node = stack.Peek();
                    stack.Pop();
                    if (comparator(node.key, from) > 0 && comparator(node.key, to) <= 0) answer.Add(node.key);

                    if (node.Right() != nill)
                    {
                        stack.Push(node.Right());
                        node = node.right;
                    }
                }
                return answer;
            }
            while (!stack.Empty())
            {
                while (node.Left() != nill)
                {
                    node = node.Left();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, from) > 0 && comparator(node.key, to) < 0) answer.Add(node.key);

                if (node.Right() != nill)
                {
                    stack.Push(node.Right());
                    node = node.right;
                }
            }
            return answer;

        }
        public IMySet<T> SubSet(T from, T to)
        {
            MyTreeSet<T> answer = new MyTreeSet<T>();
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.Left() != nill)
                {
                    node = node.Left();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, from) > 0 && comparator(node.key, to) < 0) answer.Add(node.key);

                if (node.Right() != nill)
                {
                    stack.Push(node.Right());
                    node = node.right;
                }
            }
            return answer;

        }
        public IMySet<T> HeadSet(T to)
        {
            MyTreeSet<T> answer = new MyTreeSet<T>();
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.Left() != nill)
                {
                    node = node.Left();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, to) < 0) answer.Add(node.key);

                if (node.Right() != nill)
                {
                    stack.Push(node.Right());
                    node = node.right;
                }
            }
            return answer;
        }
        public IMySet<T> TailSet(T from)
        {
            MyTreeSet<T> answer = new MyTreeSet<T>();
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.Left() != nill)
                {
                    node = node.Left();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, from) >= 0) answer.Add(node.key);

                if (node.Right() != nill)
                {
                    stack.Push(node.Right());
                    node = node.right;
                }
            }
            return answer;
        }
        public T Ceiling(T element) => comparator(element, First()) >= 0 ? First() : default(T);
        public T Floor(T element) => comparator(element, Last()) <= 0 ? Last() : default(T);
        public T Higher(T element) => comparator(element, First()) > 0 ? First() : default(T);
        public T Lower(T element) => comparator(element, Last()) < 0 ? Last() : default(T);
        public T PollFirst()
        {
            if (IsEmpty()) return default(T);
            T ans = First();
            Remove(ans);
            return ans;
        }
        public T PollLast()
        {
            if (IsEmpty()) return default(T);
            T ans = Last();
            Remove(ans);
            return ans;
        }
        public IEnumerator<T> GetEnumerator()
        {
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            T[] answer = new T[size];
            int index = 0;
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.Right() != nill)
                {
                    node = node.Right();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                yield return node.key;

                if (node.Left() != nill)
                {
                    stack.Push(node.Left());
                    node = node.Left();
                }
            }
        }
        public MyTreeSet<T> DescendingSet()
        {
            RBTree<T, object>.TreeElement? node = tree.Root();
            if (node == null) throw new Exception("Set is null");

            RBTree<T, object>.TreeElement? nill = tree.Nill();

            MyStack<RBTree<T, object>.TreeElement> stack = new MyStack<RBTree<T, object>.TreeElement>();
            T[] answer = new T[size];
            int index = 0;
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.Right() != nill)
                {
                    node = node.Right();
                    stack.Push(node);
                }
                node = stack.Peek();
                stack.Pop();
                answer[index++] = node.key;

                if (node.Left() != nill)
                {
                    stack.Push(node.Left());
                    node = node.right;
                }
            }
            return new MyTreeSet<T>(answer);
        }

    }
}

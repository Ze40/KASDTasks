using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyTreeMap<TypeKey, TypeValue> : IMyNavigableMap<TypeKey, TypeValue> where TypeKey : IComparable<TypeKey>
    {
        private class TreeMapElement
        {
            public TypeKey? key;
            public TypeValue? value;
            public TreeMapElement? left;
            public TreeMapElement? right;

            public TreeMapElement(TypeKey key, TypeValue value)
            {
                left = null;
                right = null;
                this.key = key;
                this.value = value;
            }
        }

        private TreeMapElement? _root;
        private int size;
        private Func<TypeKey?, TypeKey?, int>? comparator;

        private int Compare(TypeKey? first, TypeKey? second)
        {
            if (first == null) throw new ArgumentNullException("first element is null");
            if (second == null) throw new ArgumentNullException("second element is null");
            return first.CompareTo(second);
        }

        public MyTreeMap()
        {
            size = 0;
            comparator = Compare;
            _root = null;
        }
        public MyTreeMap(Func<TypeKey?, TypeKey?, int> comparer) { 
            size = 0;
            comparator = comparer;
            _root = null;
        }

        public void Clear()
        {
            _root = null;
            size = 0;
        }
        public bool ContainsKey(TypeKey key) { 
            if (key == null) throw new ArgumentNullException("params key is null");
            TreeMapElement? node = _root;
            while(node != null)
            {
                if(comparator(key, node.key)==0) return true;
                if (comparator(key, node.key) < 0) node = node.right;
                else node = node.left;
            }
            return false;
        }
        public bool ContainsValue(TypeValue value) {
            if (value == null) throw new ArgumentNullException("value");
            TreeMapElement? node = _root;
            if (node == null) return false;

            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node == null)
                {
                    node = stack.Peek();
                    stack.Pop();
                }
                if (node.value.Equals(value)) return true;
                else
                {
                    stack.Push(node.right);
                    stack.Push(node.left);
                    node= node.left;
                }
            }
            return false;
        }
        public (TypeKey, TypeValue)[]? EntrySet()
        {
            TreeMapElement? node = _root;
            if (node == null) return null;

            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            (TypeKey, TypeValue)[] set = new (TypeKey, TypeValue)[size];
            int index = 0;
            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                set[index++] = (node.key, node.value);

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return set;
        }
        public TypeValue Get(TypeKey key)
        {
            if (key == null) throw new ArgumentNullException("params key is null");
            TreeMapElement? node = _root;
            while (node != null)
            {
                if (comparator(key, node.key) == 0) return node.value;
                if (comparator(key, node.key) < 0) node = node.right;
                else node = node.left;
            }
            return default(TypeValue);
        }
        public bool IsEmpty()=>size == 0;
        public TypeKey[] KeySet()
        {
            TreeMapElement? node = _root;
            if (node == null) return null;

            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            TypeKey[] set = new TypeKey[size];
            int index = 0;

            while (!stack.Empty())
            {
                while(node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                set[index++] = node.key;

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }
                
            }
            return set;
        }
        public void Push(TypeKey key, TypeValue value)
        {
            if (key == null) throw new ArgumentNullException("params key is null");
            if (_root == null)
            {
                _root = new TreeMapElement(key, value);
                size = 1;
                return;
            }
            TreeMapElement? node = _root;
            while(node != null)
            {

                if (comparator(key, node.key)<0)
                {
                    if (node.right != null) node = node.right;
                    else
                    {
                        node.right = new TreeMapElement(key, value);
                        size++;
                        return;
                    }
                }
                else if(comparator(key, node.key) == 0)
                {
                    node.value= value;
                    return;
                }
                else
                {
                    if (node.left != null) node = node.left;
                    else
                    {
                        node.left = new TreeMapElement(key, value);
                        size++;
                        return;
                    }
                }
            }
            

        }
        public void PushAll(IMyMap<TypeKey, TypeValue> map)
        {
            foreach(KeyValuePair<TypeKey, TypeValue> keyValuePair in map)
            {
                Push(keyValuePair.Key, keyValuePair.Value);
            }
        }
        public void Remove(TypeKey key)
        {
            TreeMapElement? node = _root, previous = _root;
            if (node == null) return;
    
            while (node != null)
            {
                previous = node;
                if (comparator(key, node.key)<0) node = node.right;
                else if (comparator(key, node.key)>0) node = node.left;
                
                if (node!=null && comparator(key, node.key) == 0){
                    if (node.right == null && node.left == null)
                    {
                        node = null;
                        size--;
                        return;
                    }
                    if (node.right != null && node.left == null)
                    {
                        if (comparator(previous.left.key, node.key) == 0) previous.left = node.right;
                        else previous.right = node.right;
                        size--;
                        return;
                    }
                    if (node.left != null && node.right == null)
                    {
                        if (comparator(previous.left.key, node.key) == 0) previous.left = node.left;
                        else previous.right = node.left;
                        size--;
                        return;
                    }
                    TreeMapElement min = node.left, prevMin = min;
                    while (min.right != null)
                    {
                        prevMin = min;
                        min = min.right;
                    }
                    node.key = min.key;
                    node.value = min.value;
                    if (min.left != null) prevMin.right = min.left;
                    size--;
                    return;
                }
            }
            
        }

        public int Size() => size;
        public TypeKey FirstKey()
        {
            if (_root == null) return _root.key;
            return default(TypeKey);
        }
        public TypeKey LastKey()
        {
            if (_root == null) throw new ArgumentNullException("tree is empty");

            TreeMapElement? node = _root;
            Queue<TreeMapElement> queue = new Queue<TreeMapElement>();
            queue.Enqueue(node);

            while (queue.Count()!=0)
            {   
                node = queue.Dequeue();
                if (node.left!=null)queue.Enqueue(node.left);
                if (node.right!=null)queue.Enqueue(node.right);
            }
            return node.key;
        }
        public IMySortedMap<TypeKey, TypeValue> HeadMap(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            MyTreeMap<TypeKey, TypeValue> result = new MyTreeMap<TypeKey, TypeValue>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, key) < 0) result.Push(node.key, node.value);

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result;

        }
        public IMySortedMap<TypeKey, TypeValue> TailMap(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            MyTreeMap<TypeKey, TypeValue> result = new MyTreeMap<TypeKey, TypeValue>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, key) > 0) result.Push(node.key, node.value);

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result;

        }
        public IMySortedMap<TypeKey, TypeValue> SubMap(TypeKey start, TypeKey end)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            MyTreeMap<TypeKey, TypeValue> result = new MyTreeMap<TypeKey, TypeValue>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, end) < 0 && comparator(node.key, start) > 0) result.Push(node.key, node.value);

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result;

        }
        public IMyCollection<TypeValue> Values()
        {
            MyArrayList<TypeValue> result = new MyArrayList<TypeValue>();
            foreach (KeyValuePair<TypeKey, TypeValue> pair in this)
            {
                result.Add(pair.Value);
            }
            return result;
        }
        public (TypeKey, TypeValue)[] LowerEntry(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            List<(TypeKey, TypeValue)> result = new List<(TypeKey, TypeValue)>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, key) < 0) result.Add((node.key, node.value));

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result.ToArray();

        }
        public (TypeKey, TypeValue)[] FloorEntry(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            List<(TypeKey, TypeValue)> result = new List<(TypeKey, TypeValue)>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, key) <= 0) result.Add((node.key, node.value));

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result.ToArray();

        }
        public (TypeKey, TypeValue)[] HigherEntry(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            List<(TypeKey, TypeValue)> result = new List<(TypeKey, TypeValue)>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, key) > 0) result.Add((node.key, node.value));

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result.ToArray();

        }
        public (TypeKey, TypeValue)[] CeilingEntry(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            List<(TypeKey, TypeValue)> result = new List<(TypeKey, TypeValue)>();
            int index = 0;
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                if (comparator(node.key, key) >= 0) result.Add((node.key, node.value));

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }
            return result.ToArray();

        }
        public TypeKey LowerKey(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            TreeMapElement? node = _root;

            while (node != null)
            {
                if (comparator(key, node.key)>0)
                {
                    return node.key;
                }
                node = node.right;
            }
            return key;

        }
        public TypeKey FloorKey(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            TreeMapElement? node = _root;

            while (node != null)
            {
                if (comparator(key, node.key) >= 0)
                {
                    return node.key;
                }
                node = node.right;
            }
            return key;

        }
        public TypeKey HigherKey(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            TreeMapElement? node = _root;

            while (node != null)
            {
                if (comparator(key, node.key) < 0)
                {
                    return node.key;
                }
                node = node.left;
            }
            return key;

        }
        public TypeKey CeilingKey(TypeKey key)
        {
            if (_root == null) throw new ArgumentNullException("tree is emprty");

            TreeMapElement? node = _root;

            while (node != null)
            {
                if (comparator(key, node.key) < 0)
                {
                    return node.key;
                }
                node = node.left;
            }
            return key;

        }
        public (TypeKey, TypeValue) PollFirstEntry()
        {
            (TypeKey, TypeValue) result = (_root.key, _root.value);
            Remove(_root.key);
            return result;
        }
        public (TypeKey, TypeValue) PollLastEntry()
        {
            TypeKey key = this.LastKey();
            (TypeKey, TypeValue) result = (key, this.Get(key));
            Remove(key);
            return result;
        }
        public (TypeKey, TypeValue) FirstEntry()
        {
            (TypeKey, TypeValue) result = (_root.key, _root.value);
            return result;
        }
        public (TypeKey, TypeValue) LastEntry()
        {
            TypeKey key = this.LastKey();
            (TypeKey, TypeValue) result = (key, this.Get(key));
            return result;
        }
        IEnumerator<KeyValuePair<TypeKey, TypeValue>> IEnumerable<KeyValuePair<TypeKey, TypeValue>>.GetEnumerator()
        {
            TreeMapElement? node = _root;
            MyStack<TreeMapElement> stack = new MyStack<TreeMapElement>();
            stack.Push(node);

            while (!stack.Empty())
            {
                while (node.right != null)
                {
                    stack.Push(node.right);
                    node = node.right;
                }

                node = stack.Peek();
                stack.Pop();
                yield return new KeyValuePair<TypeKey, TypeValue>(node.key, node.value);

                if (node.left != null)
                {
                    stack.Push(node.left);
                    node = node.left;
                }

            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

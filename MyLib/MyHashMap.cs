using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyHashMap<TypeKey, TypeValue> : IMyMap<TypeKey, TypeValue>
    {
        private class TableElement<TK, TV> where TK : TypeKey where TV : TypeValue
        {
            public TK? key;
            public TV? value;
        }

        TableElement<TypeKey, TypeValue>[]? table;
        int size;
        double loadFactor;

        public MyHashMap(int initialCapacity = 16, double loadFactor = 0.75)
        {
            table = new TableElement<TypeKey, TypeValue>[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor;
        }

        private void ReSize()
        {
            MyHashMap<TypeKey, TypeValue> newMap = new MyHashMap<TypeKey, TypeValue>((int)(size * (1 + loadFactor)));
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null)
                {
                    newMap.Push(table[i].key, table[i].value);
                }
            }
            table = newMap.table;
        }

        public void Clear()
        {
            table = new TableElement<TypeKey, TypeValue>[16];
            size = 0;
            loadFactor = 0.75;
        }
        public void Push(TypeKey key, TypeValue value) {
            int backet = Math.Abs(key.GetHashCode()) % table.Length;
            bool isPush = false;
            
            for (int i = backet; i < table.Length && !isPush; i++) { 
                if (table[i] != null && table[i].key.Equals(key))
                {
                    table[i].value = value;
                    isPush = true;
                }
                if (table[i] == null)
                {
                    table[i] = new TableElement<TypeKey, TypeValue>();
                    table[i].value = value;
                    table[i].key = key;
                    isPush = true;
                    size++;
                }
            }
            if (!isPush)
            {
                ReSize();
                Push(key, value);
            }

        }
        public void PushAll(IMyMap<TypeKey, TypeValue> map)
        {
            foreach(KeyValuePair<TypeKey, TypeValue> pair in map)
            {
                Push(pair.Key, pair.Value);
            }
        }
        public void Remove(TypeKey key)
        {
            int bucket = Math.Abs(key.GetHashCode()) % table.Length;
            if (table[bucket] == null) { return; }
            for (int i = bucket; i < table.Length; i++)
            {
                if (table[i] != null && table[i].key.Equals(key))
                {
                    table[i] = null;
                    size--;
                    return;

                }
            }
        }
        public bool ContainsKey(TypeKey key)
        {
            int bucket = Math.Abs(key.GetHashCode()) % table.Length;
            if (table[bucket] == null) return false;
            for (int i = bucket; i < table.Length; i++) if (table[i] != null && table[i].key.Equals(key)) return true;
            return false;
        }
        public bool ContainsValue(TypeValue value)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && table[i].value.Equals(value)) return true;
            }
            return false;
        }
        public bool IsEmpty() => size == 0;
        public TypeValue Get(TypeKey key)
        {
            int bucket = Math.Abs(key.GetHashCode()) % table.Length;
            for (int i = bucket; i < table.Length; i++)
            {
                if (table[i] != null && table[i].key.Equals(key)) return table[i].value;
            }
            return default(TypeValue);
        }
        public (TypeKey, TypeValue)[] EntrySet()
        {
            (TypeKey, TypeValue)[] set = new (TypeKey, TypeValue)[size];
            int index = 0;
            for(int i = 0; i<table.Length; i++)
            {
                if (table[i] is not null)
                    set[index++] = (table[i].key, table[i].value);
            }
           return set;
        }
        public TypeKey[] KeySet()
        {
            TypeKey[] key = new TypeKey[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    key[index++] = table[i].key;
                }
            }
            return key;
        }
        public int Size() => size;
        public IMyCollection<TypeValue> Values()
        {
            MyArrayList<TypeValue> values = new MyArrayList<TypeValue>();
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null) continue;
                values.Add(table[i].value);
            }
            return values;
        }
        public IEnumerator<KeyValuePair<TypeKey, TypeValue>> GetEnumerator()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] is null) continue; 
                yield return new KeyValuePair<TypeKey, TypeValue>(table[i].key, table[i].value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

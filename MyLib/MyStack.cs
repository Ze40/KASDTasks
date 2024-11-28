using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyStack<T> : MyVector<T>
    {
        MyVector<T> elementData;
        int top;

        public MyStack()
        {
            top = -1;
            elementData = new MyVector<T>();
        }

        public void Push(T element)
        {
            top++;
            elementData.Add(element);
        }
        public void Pop()
        {
            elementData.Remove(top);
            top--;
        }
        public T Peek()
        {
            if (top == -1) throw new Exception("Stack is empty");
            return elementData.LastElement();
        }
        public bool Empty()
        {
            return top == -1;
        }
        public int Search(T element)
        {
            return top - elementData.LastIndexOf(element) + 1;
        }

        public void Print()
        {
            for (int i = 0; i <= top; i++) Console.WriteLine(elementData.Get(i));
        }

    }
}

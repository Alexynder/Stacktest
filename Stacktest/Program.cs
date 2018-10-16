using System;
using System.Collections;
using System.Collections.Generic;

namespace Stacktest
{
    class Program
    {
        public class MyStack:IEnumerable<int>
        {
            private MyStackCell top;
            int stackSize = 0;
            public int Count { get  { return stackSize; } }

            //adding data to stack
            public void Push(int data)
            {
                //creating new variable for stack cell
                MyStackCell cell = new MyStackCell(data);
                //if stack is empty top becomes holder of data, also it's next field points on nothing
                if (top==null)
                {
                    top = cell;
                }
                //if stack has some data we just write top cell in next field of new cell, and save new cell in top field
                else
                {
                    cell.next = top;
                    top = cell;
                }
                //just counting data in stack
                stackSize++;
            }
            //returning data from top element and deleting it
            public int Pop()
            {
                if (top != null)
                {
                    int result = top.data;
                    top = top.next;
                    stackSize--;
                    return result;
                }
                else
                    throw new InvalidOperationException();
            }
            //just returning data from top element
            public int Peek()
            {
                if (top != null)
                    return top.data;
                else
                    throw new InvalidOperationException();
            }
            //making possible to use foreach cycle for our class
            private IEnumerable<int> GetData(MyStackCell toFindData)
            {
                if (toFindData.next != null)
                {
                    yield return toFindData.data;
                    foreach (var i in GetData(toFindData.next))
                    {
                        yield return i;
                    }
                }
                else
                   yield return toFindData.data;
            }

            public IEnumerator<int> GetEnumerator()
            {
                return GetData(top).GetEnumerator();             
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        class MyStackCell
        {
            public int data;
            public MyStackCell next;
            public MyStackCell(int Data)
            {
                data = Data;
                next = null;
            }

        }
        static void Main(string[] args)
        {
            MyStack stack = new MyStack();
            Console.WriteLine(stack.Count); //0
            for (int i = 0; i < 10; i++)
                stack.Push(i);
            Console.WriteLine(stack.Count); //10
            int c = stack.Count;
            Console.WriteLine(stack.Peek()); //9
            Console.WriteLine(stack.Peek()); //9
            for (int i=0;i<c;i++)
            {
                Console.Write(stack.Pop()); //9876543210
            }
            Console.WriteLine(stack.Count); //0
            try
            {
                stack.Pop();//exceprion (stack is empry)
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("Stack is empty.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }

   
    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null; //첫번째
        public MyLinkedListNode<T> Tail = null; //마지막
        public int Count = 0;

        //O(1) 상수시간
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newMyLinkedListNode = new MyLinkedListNode<T>();
            newMyLinkedListNode.Data = data;

            if (Head == null)
                Head = newMyLinkedListNode;

            if(Tail !=null)
            {
                Tail.Next = newMyLinkedListNode;
                newMyLinkedListNode.Prev = Tail;
            }

            Tail = newMyLinkedListNode;
            Count++;
            return newMyLinkedListNode;
        }

        //O(1) 상수시간
        public void Remove(MyLinkedListNode<T> MyLinkedListNode)
        {
            if (Head == MyLinkedListNode)
                Head = Head.Next;

            if (Tail == MyLinkedListNode)
                Tail = Tail.Prev;

            if (MyLinkedListNode.Prev != null)
                MyLinkedListNode.Prev.Next = MyLinkedListNode.Next;

            if (MyLinkedListNode.Next != null)
                MyLinkedListNode.Next.Prev = MyLinkedListNode.Prev;

            Count--;
        }
    }

    class Board_linkedList
    {

        public MyLinkedList<int> _data = new MyLinkedList<int>(); //연결 리스트


        public void Initialize()
        {
            _data.AddLast(101);
            _data.AddLast(102);
            MyLinkedListNode<int> node =  _data.AddLast(103);
            _data.AddLast(104);
            _data.AddLast(105);


            _data.Remove(node);
        }

    }
}

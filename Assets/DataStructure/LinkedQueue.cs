using System;

namespace DataStructure2
{
    // 链表实现队列
    public class LinkedQueue<T>
    {
        public LinkedNode<T> front;
        public LinkedNode<T> rear;
        private int count;
        public int Count => count;

        public LinkedQueue()
        {
            count = 0;
        }

        // 加入队列元素 
        public void Enqueue(T value)
        {
            LinkedNode<T> node = new LinkedNode<T>(value);
            if (front == null)
            {
                front = node;
                rear = front;
            }
            else
            {
                rear.Next = node;
                rear = node;
            }
            count++;
        }

        // 取出队列元素并删除 
        public T Dequeue()
        {
            if (front == null)
                throw new Exception("Queue is empty");      // 空队列 
            T value = front.Value;
            front = front.Next;
            count--;
            return value;
        }

        // 取出队列元素 
        public T Pull()
        {
            if (front == null)
                throw new Exception("Queue is empty");      // 空队列 
            return front.Value;
        }
    }

    // 链表节点 
    public class LinkedNode<T>
    {
        public T Value { get; set; }
        public LinkedNode<T> Next { get; set; }

        public LinkedNode() { }
        public LinkedNode(T value) => Value = value;
    }

}
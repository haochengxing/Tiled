// 数组实现队列 
using System;

public class MyQueue<T>
{
    private T[] items;
    public int front;
    public int rear;
    private int count;
    public int Count => count;
    public int Capacity => items == null ? 0 : items.Length;

    public MyQueue(int capacity = 10)
    {
        items = new T[capacity];
        front = 0;
        rear = 0;
        count = 0;
    }

    // 加入队列元素 
    public void Enqueue(T item)
    {
        if (count >= Capacity)
            Expansion();
        items[rear] = item;
        rear = (rear + 1) % Capacity;
        count++;
    }

    // 取出队列元素 
    public T Dequeue()
    {
        if (count == 0)
            throw new Exception("The queue is empty");     // 空队列 
        T value = items[front];
        count--;
        front = (front + 1) % Capacity;
        return value;
    }

    // 查看队列元素 
    public T Pull()
    {
        if (count == 0)
            throw new Exception("Queue is empty");      // 空队列  
        return items[front];
    }

    // 扩容 
    private void Expansion()
    {
        int newCapacity = Capacity * 2;
        T[] newItems = new T[newCapacity];
        for (int i = front; i < count; i++)
            newItems[i - front] = items[i % Capacity];
        items = newItems;
        front = 0;
        rear = count;
    }
}

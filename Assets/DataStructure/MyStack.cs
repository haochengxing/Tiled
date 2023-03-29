// 数组实现 
using System;

public class MyStack<T>
{
    private T[] items;
    private int top;
    private int count;
    public int Count => count;
    public int Capacity => items == null ? 0 : items.Length;

    public MyStack(int capacity = 10)
    {
        items = new T[capacity + 1];
        top = 0;
        count = 0;
    }

    // 推入栈元素 
    public void Push(T item)
    {
        if (count == Capacity)
            Expansion();
        items[top] = item;
        count++;
        top++;
    }

    // 删除并抛出 
    public T Pop()
    {
        if (count == 0)
            throw new Exception("Stack is empty");
        top--;
        T value = items[top];
        count--;
        return value;
    }

    // 取出栈元素 
    public T Pull()
    {
        if (count == 0)
            throw new Exception("Stack is empty");
        return items[top - 1];
    }

    // 扩容 
    private void Expansion()
    {
        int newCapacity = Capacity * 2;
        T[] newItems = new T[newCapacity];

        for (int i = 0; i < top; i++)
            newItems[i] = items[i];
        items = newItems;
    }
}


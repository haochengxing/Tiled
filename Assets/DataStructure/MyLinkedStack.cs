// 链表实现
using System;

public class MyLinkedStack<T>
{
    public LinkedNode<T> bottom;    // 栈底
    public LinkedNode<T> top;       // 栈顶 
    private int count = 0;
    public int Count => count;

    public MyLinkedStack()
    {
        bottom = new LinkedNode<T>();
        top = bottom;
    }

    // 推入栈元素 
    public void Push(T value)
    {
        LinkedNode<T> node = new LinkedNode<T>(value);
        node.Next = top;
        top = node;
        count++;
    }

    // 删除并抛出 
    public T Pop()
    {
        if (top == bottom)
            throw new Exception("Stack is empty");      // 空栈
        T value = top.Value;
        top = top.Next;
        count--;
        return value;
    }

    // 输出栈元素 
    public T Pull()
    {
        if (top == bottom)
            throw new Exception("Stack is empty");      // 空栈 
        return top.Value;
    }
}

public class LinkedNode<T>
{
    public T Value { get; set; }
    public LinkedNode<T> Next { get; set; }
    public LinkedNode() { }
    public LinkedNode(T value) => Value = value;
}


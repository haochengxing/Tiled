using System;

public interface ILinkList
{
    bool IsEmpty();
    void Unshift(Object obj);
    Object Shift();
    void Push(Object obj);
    Object Pop();
    bool Contain(Object obj);
    void Delete(Object obj);
    void PrintAll();
    Object GetHead();
    Object GetTail();
    void Clear();
}

class LinkListNode
{
    public Object value;
    public LinkListNode next;

    public LinkListNode(Object value, LinkListNode next)
    {
        this.value = value;
        this.next = next;
    }

    public LinkListNode(Object value)
    {
        this.value = value;
        this.next = null;
    }
}

// 实现单向链表为例子
public class OneWayList : ILinkList
{
    private LinkListNode head, tail;

    public OneWayList()
    {
        this.head = this.tail = null;
    }

    // 判断是否为空  
    public bool IsEmpty()
    {
        return head == null;
    }

    public void Unshift(Object obj)
    {
        head = new LinkListNode(obj, head);
        if (tail == null)
            tail = head;
    }

    public Object Shift()
    {
        if (head == null)
            throw new NullReferenceException();
        Object value = head.value;
        if (head == tail)
            head = tail = null;
        else
            head = head.next;
        return value;
    }

    public void Push(Object obj)
    {
        if (!IsEmpty())
        {
            tail.next = new LinkListNode(obj);
            tail = tail.next;
        }
        else
            head = tail = new LinkListNode(obj);
    }

    public Object Pop()
    {
        if (head == null)
            throw new NullReferenceException();
        Object obj = tail.value;
        if (head == tail)
            head = tail = null;
        else
        {
            // 查找前驱节点
            for (LinkListNode temp = head; temp.next != null && !temp.next.Equals(tail); temp = temp.next)
                tail = temp;
            tail.next = null;
        }
        return obj;
    }

    public void PrintAll()
    {
        string result = "";
        for (LinkListNode temp = head; temp != null; temp = temp.next)
            result += "   " + temp.value.ToString();
        Console.WriteLine(result);
    }

    public bool Contain(Object obj)
    {
        if (head == null)
            return false;
        else
        {
            for (LinkListNode temp = head; temp != null; temp = temp.next)
            {
                if (temp.value.Equals(obj))
                    return true;
            }
        }
        return false;
    }

    public void Delete(Object obj)
    {
        if (!IsEmpty())
        {
            if (head == tail && head.value.Equals(obj))
                head = tail = null;
            else if (head.value.Equals(obj))
                head = head.next;
            else
            {
                // temp_prev为删除值的前驱节点
                for (LinkListNode temp_prev = head, temp = head.next; temp != null; temp_prev = temp_prev.next, temp = temp.next)
                {
                    if (temp.value.Equals(obj))
                    {
                        temp_prev.next = temp.next;   // 设置前驱节点的next为下个节点 
                        if (temp == tail)
                            tail = temp_prev;
                        temp = null;
                        break;
                    }
                }
            }
        }
    }

    public Object GetHead()
    {
        return this.head.value;
    }

    public Object GetTail()
    {
        return this.tail.value;
    }

    public void Clear()
    {
        do
        {
            Delete(head.value);
        } while (!IsEmpty());
    }
}


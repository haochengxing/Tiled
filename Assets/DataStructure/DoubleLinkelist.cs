using System;

namespace DataStructure
{
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

    class LinkedNode
    {
        public Object value;
        public LinkedNode prev;
        public LinkedNode next;

        public LinkedNode(Object value, LinkedNode prev, LinkedNode next)
        {
            this.value = value;
            this.next = next;
            this.prev = prev;
        }

        public LinkedNode(Object value)
        {
            this.value = value;
        }
    }


    // 双向链表
    public class DoubleLinkelist : ILinkList
    {
        private LinkedNode head, tail;

        public DoubleLinkelist()
        {
            head = tail = null;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void Unshift(Object obj)
        {
            if (IsEmpty())
                head = tail = new LinkedNode(obj);
            else
            {
                head = new LinkedNode(obj, null, head);
                head.next.prev = head;
            }
        }

        public Object Shift()
        {
            if (IsEmpty())
                throw new NullReferenceException();
            Object obj = head.value;
            if (head == tail)
                head = tail = null;
            else
            {
                head = head.next;
                head.prev = null;
            }
            return obj;
        }

        public void Push(Object obj)
        {
            if (IsEmpty())
                head = tail = new LinkedNode(obj);
            else
            {
                tail = new LinkedNode(obj, tail, null);
                tail.prev.next = tail;
            }
        }

        public Object Pop()
        {
            if (IsEmpty())
                throw new NullReferenceException();
            Object value = tail.value;
            if (head == tail)
                head = tail = null;
            else
            {
                tail = tail.prev;
                tail.next = null;
            }
            return value;
        }

        public bool Contain(Object obj)
        {
            if (IsEmpty())
                return false;
            else
            {
                for (LinkedNode temp = head; temp != null; temp = temp.next)
                    if (temp.value.Equals(obj))
                        return true;
            }
            return false;
        }

        public void Delete(Object obj)
        {
            if (IsEmpty())
                throw new NullReferenceException();
            if (head == tail)
                head = tail = null;
            else
            {
                for (LinkedNode temp = head; temp != null; temp = temp.next)
                {
                    if (temp.value.Equals(obj))
                    {
                        if (temp.value.Equals(obj))
                        {
                            if (temp == tail)
                            {
                                tail = tail.prev;
                                tail.next = null;
                                break;
                            }
                            else if (temp == head)
                            {
                                head.next.prev = null;
                                head = head.next;
                            }

                            else
                                temp.prev.next = temp.next;
                        }
                    }
                }
            }
        }

        public void PrintAll()
        {
            string result = "";
            for (LinkedNode temp = head; temp != null; temp = temp.next)
                result += "   " + temp.value.ToString();
            Console.WriteLine(result);
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


}


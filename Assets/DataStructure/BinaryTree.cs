// 以实现简单二叉树为例子 
using System;
using System.Collections.Generic;

class BinaryTree<TKey, TValue> where TKey : IComparable
{
    public TKey Key { get; set; }           // 键
    public TValue Value { get; set; }       // 值 

    BinaryTree<TKey, TValue> Left { get; set; }     // 左分支 
    BinaryTree<TKey, TValue> Right { get; set; }    // 右分支 

    // 构造函数初始化 
    public BinaryTree(TKey key, TValue value)
    {
        this.Key = key;
        this.Value = value;
    }

    //  查找功能 
    public TValue Search(TKey key)
    {
        int ret = key.CompareTo(this.Key);

        if (ret == 0)
        {
            return Value;
        }
        else
        {
            var subTree = ret < 0 ? Left : Right;
            if (subTree == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                return subTree.Search(key);
            }
        }
    }

    // 插入功能
    public void Insert(TKey key, TValue value)
    {
        int ret = key.CompareTo(this.Key);

        if (ret == 0)
        {
            this.Value = value;
        }
        else
        {
            var subTree = ret < 0 ? Left : Right;
            if (subTree == null)
            {
                subTree = new BinaryTree<TKey, TValue>(key, value);
                if (ret < 0)
                    Left = subTree;
                else
                    Right = subTree;
            }
            else
            {
                subTree.Insert(key, value);
            }
        }
    }

    // 二叉排序树性的遍历 
    public void Visit(Action<TKey, TValue> visitor)
    {
        if (Left != null)
        {
            Left.Visit(visitor);
        }

        visitor(Key, Value);

        if (Right != null)
        {
            Right.Visit(visitor);
        }
    }
}

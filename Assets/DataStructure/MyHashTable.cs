using System;
using UnityEngine;

public class MyHashTable
{
    // 初始化哈希表
    int[] hashTable;

    // 创建哈希表
    public void CreateHashTable(int[] intArray)
    {
        hashTable = new int[intArray.Length];
        for (int i = 0; i < intArray.Length; i++)
        {
            Insert(intArray[i]);
        }
    }

    public void ShowData()
    {
        Debug.Log("展示哈希表中的数据：" + String.Join(",", hashTable));
    }

    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="value">待插入值</param>
    public void Insert(int value)
    {
        // 哈希函数，除留余数法
        int hashAddress = Hash(value);
        // 如果不为0，则说明发生冲突
        while (hashTable[hashAddress] != 0)
        {
            // 利用开放定址的线性探测法解决冲突
            hashAddress = (++hashAddress) % hashTable.Length;
        }
        // 将待插入值存入字典中
        hashTable[hashAddress] = value;
    }

    /// <summary>
    /// 哈希表查找
    /// </summary>
    /// <param name="value">待查找的值</param>
    /// <returns>对应的下标</returns>
    public int Search(int value)
    {
        int hashAddress = Hash(value);
        // 冲突发生
        while (hashTable[hashAddress] != value)
        {
            // 利用开放定址的线性探测法解决冲突
            hashAddress = (++hashAddress) % hashTable.Length;
            // 查找到了开放单元或者循环回到原点，表示查找失败
            if (hashTable[hashAddress] == 0 || hashAddress == Hash(value)) { return -1; }
        }
        // 查找成功，返回值的下标
        return hashAddress;
    }

    /// <summary>
    /// 哈希函数（除留余数法）
    /// </summary>
    /// <param name="value"></param>
    /// <returns>返回数据的位置</returns>
    private int Hash(int value)
    {
        return value % hashTable.Length;
    }
}

// 简单实现数组，数组涉及的应用方法很多
// 这里只实现增删功能, 其它方法逻辑差不多 
public class MyArray<T>
{
    private T[] myArray;
    private int arraysize;
    
    public int Count
    {
        get
        {
            return arraysize;
        }
    }

    // 属性或索引器必须至少有一个访问器
    public T this[int index]
    {
        get
        {
            return myArray[index];
        }
        set
        {
            myArray[index] = value;
        }
    }

    public MyArray()
    {
        arraysize = 0;      // 初始化数组大小
        myArray = new T[arraysize];
    }

    
    public T GetValue(int index)
    {
        return myArray[index];
    }

    // 向数组添加新元素 
    public void Add(T data)
    {
        if (data == null)    // 元素为空的清空 
        {
            return;
        }

        T[] array = new T[arraysize + 1];       //申请一块新的内存
        for (int i = 0; i < myArray.Length; i++)
        {
            array[i] = myArray[i];      // 先把之前的元素添加进来 
        }
        array[arraysize] = data;
        myArray = null;
        myArray = array;
        arraysize++;
        array = null;           // 释放原来的array内存 

    }

    // 按照索引删除数据 
    public void RemoveAt(int index)
    {
        if (index < 0 && index > arraysize)
        {
            return;
        }

        for (int i = index; i < arraysize - 1; i++)
        {
            myArray[i] = myArray[i + 1];
        }
        
        myArray[arraysize - 1] = default(T);
        arraysize--;
    }

    // 指定元素删除 
    public void Remove(T n)
    {
        for (int i = 0; i < myArray.Length; i++)
        {
            if (myArray[i].Equals(n))
            {
                RemoveAt(i);
            }
        }
    }
} 

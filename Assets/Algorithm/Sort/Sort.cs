using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 9, 5, 8, 7, 2, 6, 4, 1, 3 };
        //insertSort(array);
        //selectSort(array);
        //shellSort(array);
        //quickSort(array,0,array.Length-1);
        //heapSort(array);
        mergeSort(array, 0, array.Length - 1);
        Debug.Log(string.Join(",", array));
    }

    // Update is called once per frame
    void Update()
    {

    }


    //直接插入排序
    public void insertSort(int[] array)
    {
        //i下标从数组的第二个值开始
        for (int i = 1; i < array.Length; i++)
        {
            int tmp = array[i];//tmp存放i下标的值
            int j = i - 1;//j下标为i的前一个位置
            for (; j >= 0; j--)
            {
                if (array[j] > tmp)
                {
                    //j下标的值大，将j下标的值放到j的下一个位置
                    array[j + 1] = array[j];
                }
                else
                {
                    //j下标的值较小，j小标的值要直接放到j的下一个位置
                    break;
                }
            }
            //此时j下标的值是负数了，将tmp的值放到j变量的后一个位置
            array[j + 1] = tmp;
        }
    }


    public void selectSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int minIndex = i;//假设最小值是当前的i下标的值
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[minIndex])
                {
                    //更新
                    minIndex = j;//将此时的j下标的值赋给minIndex
                }
            }
            if (i != minIndex)
            {
                //调用交换的方法交换i下标的值和minIndex的值
                swap(array, minIndex, i);
            }
        }
    }

    //交换的方法
    public void swap(int[] array, int i, int j)
    {
        int tmp = array[i];
        array[i] = array[j];
        array[j] = tmp;
    }


    public void shellSort(int[] array2)
    {
        int gap = array2.Length;//为数组的长度 - 为10
        while (gap > 1)
        {
            gap /= 2;//先是分成了5组，然后是2组，再是1组
            shell(array2, gap);//调用直接插入排序方法
        }
    }

    //实现直接插入排序方法
    public void shell(int[] array2, int gap)
    {
        //i下标从第一组的第二个数据开始
        for (int i = gap; i < array2.Length; i++)
        {
            int tmp = array2[i];//tmp存放i下标的值
            int j = i - gap;//j下标为i-gap个位置
                            //j每次-gap个位置
            for (; j >= 0; j -= gap)
            {
                if (array2[j] > tmp)
                {
                    //j下标的值大，将j下标的值放到j变量加上一个gap的位置上
                    array2[j + gap] = array2[j];
                }
                else
                {
                    //j下标的值较小，j下标的值要直接放到j变量加上一个gap的位置上
                    break;
                }
            }
            //此时j下标的值是负数了，将tmp的值放到j变量加上一个gap的位置上
            array2[j + gap] = tmp;
        }
    }

    public void quickSort(int[] array, int begin, int end)
    {
        if (begin > end)
            return;
        int tmp = array[begin];
        int i = begin;
        int j = end;
        while (i != j)
        {
            while (array[j] >= tmp && j > i)
                j--;
            while (array[i] <= tmp && j > i)
                i++;
            if (j > i)
            {
                int t = array[i];
                array[i] = array[j];
                array[j] = t;
            }
        }
        array[begin] = array[i];
        array[i] = tmp;
        quickSort(array, begin, i - 1);
        quickSort(array, i + 1, end);
    }


    public void heapSort(int[] array)
    {
        creatBigHeap(array);
        int end = array.Length - 1;
        while (end > 0)
        {
            swap(array, 0, end);
            shiftDown(array, 0, end);
            end--;
        }
    }

    //建堆
    public void creatBigHeap(int[] array)
    {
        for (int parent = (array.Length - 1 - 1) / 2; parent >= 0; parent--)
        {
            //调用向下调整
            shiftDown(array, parent, array.Length);
        }
    }

    //向下调整的方法
    public void shiftDown(int[] array, int parent, int len)
    {
        int child = (2 * parent) + 1;
        while (child < len)
        {
            if (child + 1 < len && array[child] < array[child + 1])
            {
                child++;
            }
            if (array[child] > array[parent])
            {
                swap(array, child, parent);
                parent = child;
                child = 2 * parent + 1;
            }
            else
            {
                break;
            }
        }
    }


    public void mergeSort(int[] arr, int left, int right)
    {
        if (left >= right)
        {
            return;
        }
        int mid = (left + right) / 2;
        mergeSort(arr, left, mid);
        mergeSort(arr, mid + 1, right);
        merge(arr, left, mid, right);

    }
    //需要注意的是整个合并过程中并没有将两个被合并的数组单独拎出来，二者始终是存在于一个数组地址上的
    public void merge(int[] arr, int left, int mid, int right)
    {
        int s1 = left;//根据拿到的左边界，我们定其为第一个数组的指针
        int s2 = mid + 1;//根据中间位置，让中间位置右移一个单位，那就是第二个数组的指针
        int[] temp = new int[right - left + 1];//根据左右边界相减我们得到这片空间的长度，以此声明额外空间
        int i = 0;//定义额外空间的指针
        while (s1 <= mid && s2 <= right)
        {
            if (arr[s1] <= arr[s2])
            {
                //如果第一个数组的指针数值小于第二个数组的，那么其放置在临时空间上
                temp[i++] = arr[s1++];
            }
            else
            {
                //否则是第二个数组的数值放置于其上
                temp[i++] = arr[s2++];
            }
        }
        while (s1 <= mid)
        {
            //如果这是s1仍然没有到达其终点，那么说明它还有剩
            temp[i++] = arr[s1++];//因为我们知道每个参与合并的数组都是有序数组，因此直接往后拼接即可
        }
        while (s2 <= right)
        {
            //同上
            temp[i++] = arr[s2++];
        }
        for (int j = 0; j < temp.Length; j++)
        {
            //数组复制
            arr[j + left] = temp[j];
        }
    }

}

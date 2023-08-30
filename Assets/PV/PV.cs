using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PV : MonoBehaviour
{
    private readonly object syncObject = new object();

    int mutex = 1;

    Thread thread0;
    Thread thread1;
    Thread thread2;

    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        thread0 = new Thread(Pa);
        thread1 = new Thread(Pb);
        thread2 = new Thread(Pc);

        thread0.Start();
        thread1.Start();
        thread2.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Pa()
    {
        P();
        count = 1;
        Thread.Sleep(new System.Random().Next(1, 10));
        Debug.Log("Pa count = " + count);
        V();
    }
    void Pb()
    {
        P();
        count = 2;
        Thread.Sleep(new System.Random().Next(1, 10));
        Debug.Log("Pb count = " + count);
        V();
    }
    void Pc()
    {
        P();
        count = 3;
        Thread.Sleep(new System.Random().Next(1, 10));
        Debug.Log("Pc count = " + count);
        V();
    }

    void P()
    {
        lock (syncObject)
        {
            while (mutex == 0) // 如果count已经为0，那么调用线程需要等待
            {
                Monitor.Wait(syncObject);
            }

            mutex--; // 申请资源

            Monitor.Pulse(syncObject); // 唤醒等待的线程
        }
    }

    void V()
    {
        lock (syncObject)
        {
            mutex++; // 释放资源

            Monitor.Pulse(syncObject); // 唤醒等待的线程
        }
    }

    void OnDestroy()
    {
        thread0?.Abort();
        thread1?.Abort();
        thread2?.Abort();
    }
}

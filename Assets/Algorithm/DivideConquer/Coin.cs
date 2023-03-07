using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] test = {1,1,1,0,1, 1, 1, 1, 1,1 };

        Debug.Log(getCounterfeitCoin(test,0, test.Length-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int getCounterfeitCoin(int [] coins,int first,int last)
    {
        int firstSum = 0, lastSum = 0;
        int i;
        if (first == last - 1)
        {
            if (coins[first] < coins[last])
            {
                return first;
            }
            return last;
        }
        if ((last - first + 1) % 2 == 0)
        {
            for (i = first; i < first+(last-first)/2+1; i++)
            {
                firstSum += coins[i];
            }
            for (i = first+(last-first)/2+1; i < last+1; i++)
            {
                lastSum += coins[i];
            }
            if (firstSum<lastSum)
            {
                return getCounterfeitCoin(coins, first, first + (last - first) / 2);
            }
            else
            {
                return getCounterfeitCoin(coins, first + (last - first) / 2+1,last);
            }
        }
        else
        {
            for (i = first; i < first + (last - first) / 2 ; i++)
            {
                firstSum += coins[i];
            }
            for (i = first + (last - first) / 2 + 1; i < last + 1; i++)
            {
                lastSum += coins[i];
            }
            if (firstSum < lastSum)
            {
                return getCounterfeitCoin(coins, first, first + (last - first) / 2-1);
            }
            else if(firstSum > lastSum)
            {
                return getCounterfeitCoin(coins, first + (last - first) / 2 - 1, last);
            }
            else
            {
                return first + (last - first) / 2;
            }
        }
    }
}

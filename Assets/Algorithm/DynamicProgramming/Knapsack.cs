using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] test_v = {0, 1, 6, 18, 22, 28 };
        int[] test_w = {0, 1, 2, 5, 6, 7 };

        Debug.Log(Memoized_Knapsack(test_v, test_w,8));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    const int N = 6;
    const int maxT = 10;

    int[,] c = new int[N,maxT];

    int Memoized_Knapsack(int [] v,int [] w,int T)
    {
        int i, j;
        for (i = 0; i < N; i++)
        {
            for (j = 0; j <= T; j++)
            {
                c[i,j] = -1;
            }
        }
        return Calculate_Max_Value(v,w, w.Length-1, T);
    }

    int Calculate_Max_Value(int [] v,int []w,int i,int j)
    {
        int temp;
        if (c[i, j] != -1)
        {
            return c[i,j];
        }
        if (i == 0 || j == 0)
        {
            c[i, j] = 0;
        }
        else
        {
            c[i, j] = Calculate_Max_Value(v,w,i-1,j);
            if (j>=w[i]) {
                temp = Calculate_Max_Value(v, w, i - 1, j - w[i]) +v[i];
                if (c[i, j] < temp)
                {
                    c[i, j] = temp;
                }
            }
        }
        return c[i,j];
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] test = { 0, 1, 3, 5, 4, 8 };

        Debug.Log(Top_Down_Cut_Rod(test, test.Length-1));
        Debug.Log(Bottom_Up_Cut_Rod(test, test.Length-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int Top_Down_Cut_Rod(int [] p,int n)
    {
        int r = 0;
        int i;
        if (n == 0)
        {
            return 0;
        }
        for (i = 1; i <=n; i++)
        {
            int tmp = p[i] + Top_Down_Cut_Rod(p, n - i);
            r = (r >= tmp) ? r : tmp;
        }
        return r;
    }

    int Bottom_Up_Cut_Rod(int[] p, int n)
    {
        int[] r = new int[10];
        int temp;
        int i, j;
        for (j = 1; j <=n; j++)
        {
            temp = 0;
            for (i = 0; i <= j; i++)
            {
                temp = (temp >= p[i] + r[j - 1]) ? temp : (p[i] + r[j - 1]);
            }
            r[j] = temp;
        }
        return r[n];
    }
}

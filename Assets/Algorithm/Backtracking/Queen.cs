using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Nqueen(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    const int n = 4;
    int[] queen = new int[n+1];

    void Show()
    {
        for (int i = 1; i <=n; i++)
        {
            Debug.Log(queen[i]);
        }
    }

    int Place(int j)
    {
        int i;
        for (i = 0; i < j; i++)
        {
            if (queen[i]==queen[j] || Mathf.Abs(queen[i] - queen[j]) == (j - i))
            {
                return 0;
            }
        }
        return 1;
    }

    void Nqueen(int j)
    {
        int i;
        for (i = 1; i <=n; i++)
        {
            queen[j] = i;
            if (Place(j)==1&&j<=n)
            {
                if (j==n)
                {
                    Show();
                }
                else
                {
                    Nqueen(j+1);
                }
            }
        }
    }
}

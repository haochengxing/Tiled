using System;
using UnityEngine;

public class Vertex
{
    public string Data;
    public bool IsVisited;

    public Vertex(string Vertexdata)
    {
        Data = Vertexdata;
    }
}

// 图的简单实现 
public class Graph
{
    //图中所能包含的点上限
    private const int Number = 10;

    //顶点数组
    private Vertex[] vertiexes;

    //邻接矩阵
    public int[,] adjmatrix;

    //统计当前图中有几个点
    int numVerts = 0;

    //初始化图
    public Graph()
    {
        //初始化邻接矩阵和顶点数组
        adjmatrix = new Int32[Number, Number];
        vertiexes = new Vertex[Number];
        //将代表邻接矩阵的表全初始化为0
        for (int i = 0; i < Number; i++)
        {
            for (int j = 0; j < Number; j++)
            {
                adjmatrix[i, j] = 0;
            }
        }
    }

    //向图中添加节点
    public void AddVertex(String v)
    {
        vertiexes[numVerts] = new Vertex(v);
        numVerts++;
    }

    //向图中添加有向边
    public void AddEdge(int vertex1, int vertex2)
    {
        adjmatrix[vertex1, vertex2] = 1;
        //adjmatrix[vertex2, vertex1] = 1;
    }

    //显示点
    public void DisplayVert(int vertexPosition)
    {
        Debug.Log(vertiexes[vertexPosition] + " ");
    }
}

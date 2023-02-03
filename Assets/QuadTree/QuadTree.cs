using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public Vector2 pos;
    public int data;

    public Node(Vector2 _pos, int _data)
    {
        pos = _pos;
        data = _data;
    }
    Node()
    {
        data = 0;
    }
}

public class QuadTree
{
    //左下
    Vector2 botLeft;
    //右上
    Vector2 topRight;


    Node n;

    //子结点
    QuadTree botLeftTree;
    QuadTree topLeftTree;
    QuadTree topRightTree;
    QuadTree botRightTree;

    float width;


    QuadTree()
    {
        botLeft = new Vector2(0, 0);
        topRight = new Vector2(0, 0);
        n = null;
        botLeftTree = null;
        topLeftTree = null;
        topRightTree = null;
        botRightTree = null;
        width = 1;
    }
    public QuadTree(Vector2 botL, Vector2 topR,float w)
    {
        n = null;
        botLeftTree = null;
        topLeftTree = null;
        topRightTree = null;
        botRightTree = null;
        botLeft = botL;
        topRight = topR;
        width = w;
    }

    public void insert(Node node)
    {
        if (node == null)
            return;

        // Current quad cannot contain it
        if (!inBoundary(node.pos))
            return;

        // We are at a quad of unit area
        // We cannot subdivide this quad further
        if (Mathf.Abs(topRight.x - botLeft.x) <= width &&
            Mathf.Abs(topRight.y - botLeft.y) <= width)
        {
            if (n == null)
                n = node;
            return;
        }

        Vector2 center = (botLeft + topRight) / 2;
        float centerX = center.x;
        float centerY = center.y;

        if (centerX >= node.pos.x)
        {
            if (centerY >= node.pos.y)
            {
                //左下
                if (botLeftTree == null)
                    botLeftTree = new QuadTree(botLeft,center, width);
                botLeftTree.insert(node);
            }
            else
            {
                //左上
                if (topLeftTree == null)
                    topLeftTree = new QuadTree(new Vector2(botLeft.x, centerY),new Vector2(centerX, topRight.y), width);
                topLeftTree.insert(node);
            }
        }
        else
        {
            if (centerY >= node.pos.y)
            {
                //右下
                if (botRightTree == null)
                    botRightTree = new QuadTree( new Vector2(centerX, botLeft.y), new Vector2(topRight.x, centerY), width);
                botRightTree.insert(node);
            }
            else
            {
                //右上
                if (topRightTree == null)
                    topRightTree = new QuadTree(center,  topRight, width);
                topRightTree.insert(node);
            }
        }
    }

    public Node search(Vector2 p)
    {
        // Current quad cannot contain it
        if (!inBoundary(p))
            return null;

        // We are at a quad of unit length
        // We cannot subdivide this quad further
        if (n != null)
            return n;

        Vector2 center = (botLeft + topRight) / 2;
        float centerX = center.x;
        float centerY = center.y;

        if (centerX >= p.x)
        {
            if (centerY >= p.y)
            {
                //左下
                if (botLeftTree == null)
                    return null;
                return botLeftTree.search(p);
            }
            else
            {
                //左上
                if (topLeftTree == null)
                    return null;
                return topLeftTree.search(p);
            }
        }
        else
        {
            if (centerY >= p.y)
            {
                //右下
                if (botRightTree == null)
                    return null;
                return botRightTree.search(p);
            }
            else
            {
                if (topRightTree == null)
                    return null;
                return topRightTree.search(p);
            }
        }
    }

    bool inBoundary(Vector2 p)
    {
        return (p.x >= botLeft.x &&
            p.x <= topRight.x &&
            p.y >= botLeft.y &&
            p.y <= topRight.y);
    }


    public void find(List<Node> result,float minX,float minY,float maxX,float maxY)
    {
        if (n != null)
        {
            if(n.pos.x >= minX && n.pos.y >= minY && n.pos.x <= maxX && n.pos.y <= maxY)
            {
                result.Add(n);
            }
        }
        if (botLeftTree != null)
        {
            botLeftTree.find(result, minX, minY, maxX, maxY);
        }
        if (topLeftTree != null)
        {
            topLeftTree.find(result, minX, minY, maxX, maxY);
        }
        if (topRightTree != null)
        {
            topRightTree.find(result, minX, minY, maxX, maxY);
        }
        if (botRightTree != null)
        {
            botRightTree.find(result, minX, minY, maxX, maxY);
        }
    }
}

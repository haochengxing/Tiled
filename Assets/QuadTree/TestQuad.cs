using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestQuad : MonoBehaviour
{
    public GameObject Image;
    QuadTree quadTree;
    List<RectTransform> rectTransforms;
    List<LineRenderer> lineRenderers;
    public int cNums = 200;
    private void Start()
    {

        rectTransforms = new List<RectTransform>();
        lineRenderers = new List<LineRenderer>();
        Rect sc = Screen.safeArea;

        RectTransform rect = Image.GetComponent<RectTransform>();
        quadTree = new QuadTree(new Vector2(-sc.width/2, -sc.height / 2),new Vector2(sc.width/2, sc.height/2),rect.sizeDelta.x);

        for (int i = 0; i < cNums; i++)
        {
            GameObject go = Instantiate(Image);
            RectTransform rectT = go.GetComponent<RectTransform>();
            //随机生成位置
            float x = Random.Range(0, sc.width);
            float y = Random.Range(0, sc.height);
            rectT.position = new Vector3(x, y, 0);
            go.transform.SetParent(transform);
            rectTransforms.Add(rectT);
        }

        //插入节点
        for (int i = 0; i < rectTransforms.Count; i++)
        {
            quadTree.insert( new Node(rectTransforms[i].anchoredPosition,i));
        }

        Node green = quadTree.search(rectTransforms[cNums/2].anchoredPosition);

        rectTransforms[green.data].GetComponent<Image>().color = Color.green;

        List<Node> result = new List<Node>();

        quadTree.find(result, 100,100,400,400);

        foreach (var item in result)
        {
            rectTransforms[item.data].GetComponent<Image>().color = Color.red;
        }
    }
    private void Update()
    {

    }
}


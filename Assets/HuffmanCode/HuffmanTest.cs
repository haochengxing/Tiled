using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuffmanTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string source = "哈夫曼曼曼曼曼";

        Huffman hf = new Huffman();
        Dictionary<char, string> key = null;
        var hfCode = hf.StringToHuffmanCode(out key, source);

        Debug.Log("--------------------编码与解析");
        Debug.Log("编码: " + hfCode);

        var text = hf.ToText(hfCode, key);
        Debug.Log("解析: " + text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

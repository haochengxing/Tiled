using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Huffman
{
    public class Node : IComparable
    {
        /// <summary>
        /// 左子树
        /// </summary>
        public Node LChild { get; set; }
        /// <summary>
        /// 右子树
        /// </summary>
        public Node RChild { get; set; }
        /// <summary>
        /// 权值
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 原字符
        /// </summary>
        public char Key { get; set; }

        public Node()
        {

        }

        public Node(int weight, char value)
        {
            this.Weight = weight;
            this.Key = value;
        }
        /// <summary>
        /// 实现比较接口，用于排序
        /// </summary>
        public int CompareTo(object obj)
        {
            int result = 0;
            Node tmp = obj as Node;

            if (tmp.Weight > this.Weight)
            {
                result = 1;
            }
            else if (tmp.Weight < this.Weight)
            {
                result = -1;
            }

            return result;
        }
    }

    /// <summary>
    /// 获得权值数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public Node[] GetWeightArray(string str)
    {
        List<Node> result = new List<Node>();

        char[] charArr = str.ToCharArray();

        while (charArr.Length > 0)
        {
            char[] cntChars = null;
            var tmp = charArr.Where(m => m == charArr[0]);
            cntChars = new char[tmp.Count()];
            tmp.ToArray().CopyTo(cntChars, 0);
            charArr = charArr.Where(m => m != tmp.First()).ToArray();

            result.Add(new Node(cntChars.Length, cntChars[0]));
        }

        return result.ToArray();
    }

    /// <summary>
    /// 构建哈弗曼树
    /// </summary>
    /// <param name="sources">权值数组</param>
    /// <returns>返回哈弗曼树根</returns>
    public Node CreateHuffmanTree(Node[] sources)
    {
        Node result = null;
        Node[] nodes = sources;
        bool isNext = true;

        while (isNext)
        {
            if (nodes.Length == 2)
            {
                result = new Node();
                result.LChild = nodes[0];
                result.RChild = nodes[1];
                isNext = false;
            }

            Array.Sort(nodes);
            Node n1 = nodes[nodes.Length - 1];
            Node n2 = nodes[nodes.Length - 2];
            Node tmp = new Node();
            tmp.Weight = n1.Weight + n2.Weight;
            tmp.LChild = n1;
            tmp.RChild = n2;

            Node[] tmpNds = new Node[nodes.Length - 1];
            Array.Copy(nodes, 0, tmpNds, 0, nodes.Length - 1);
            tmpNds[tmpNds.Length - 1] = tmp;

            nodes = tmpNds;
        }

        return result;
    }

    /// <summary>
    /// 字符串转换为哈弗曼代码.当然可以改成二进制数据
    /// </summary>
    /// <param name="key">编码字典</param>
    /// <param name="str">传入字符串</param>
    /// <returns>编码后的字符串</returns>
    public string StringToHuffmanCode(out Dictionary<char, string> key, string str)
    {
        string result = "";

        var tmps = GetWeightArray(str);

        var tree = CreateHuffmanTree(tmps);
        var dict = CreateHuffmanDict(tree);

        foreach (var item in dict)
        {
            Debug.Log(item);
        }

        result = ToHuffmanCode(str, dict);

        key = dict;
        return result;
    }

    /// <summary>
    /// 创建哈弗曼代码字典
    /// </summary>
    /// <param name="hTree">哈弗曼树</param>
    /// <returns></returns>
    public Dictionary<char, string> CreateHuffmanDict(Node hTree)
    {
        return CreateHuffmanDict("", hTree);
    }

    private Dictionary<char, string> CreateHuffmanDict(string code, Node hTree)
    {
        Dictionary<char, string> result = new Dictionary<char, string>();

        if (hTree.LChild == null && hTree.RChild == null)
        {
            result.Add(hTree.Key, code);
        }
        else
        {
            var dictL = CreateHuffmanDict(code + "0", hTree.LChild);
            var dictR = CreateHuffmanDict(code + "1", hTree.RChild);

            foreach (var item in dictL)
            {
                result.Add(item.Key, item.Value);
            }

            foreach (var item in dictR)
            {
                result.Add(item.Key, item.Value);
            }
        }

        return result;
    }

    /// <summary>
    /// 源字符串转换到哈弗曼代码
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="hfdict">哈弗曼代码字典</param>
    /// <returns>编译后代码</returns>
    public string ToHuffmanCode(string source, Dictionary<char, string> hfdict)
    {
        string result = "";

        for (int i = 0; i < source.Length; i++)
        {
            result += hfdict.First(m => m.Key == source[i]).Value;
        }

        return result;
    }

    /// <summary>
    /// 解析回字符串
    /// </summary>
    /// <param name="code">哈弗曼代码</param>
    /// <param name="hfdict">哈弗曼代码字典</param>
    /// <returns>解析后字符串</returns>
    public string ToText(string code, Dictionary<char, string> hfdict)
    {
        string result = "";

        for (int i = 0; i < code.Length;)
        {
            foreach (var item in hfdict)
            {
                if (code[i] == item.Value[0]
                    && item.Value.Length + i <= code.Length)
                {
                    char[] tmpArr = new char[item.Value.Length];

                    Array.Copy(code.ToCharArray(), i, tmpArr, 0, tmpArr.Length);

                    if (new String(tmpArr) == item.Value)
                    {
                        result += item.Key;
                        i += item.Value.Length;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
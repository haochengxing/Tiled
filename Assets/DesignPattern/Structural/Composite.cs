using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 组合模式
/// </summary>
public class Composite
{
    /// <summary>
    /// 文本打印
    /// </summary>
    /// <remarks>透明式抽象构建</remarks>
    public interface TextPrint
    {
        //在安全式组合模式中，仅保留公有的方法(如Print)

        /// <summary>
        /// 添加子构件
        /// </summary>
        void Add(TextPrint textPrint);

        /// <summary>
        /// 打印
        /// </summary>
        void Print();
    }

    /// <summary>
    /// 食物文本打印
    /// </summary>
    /// <remarks>树叶构件</remarks>
    public class FoodTextPrint : TextPrint
    {
        public FoodTextPrint(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 食物名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 无Add操作，省略
        /// </summary>
        public void Add(TextPrint textPrint)
        {
            //无Add操作，省略
        }

        /// <summary>
        /// 食物文本打印
        /// </summary>
        public void Print()
        {
            Debug.Log($"Food:{this.Name}");
        }
    }

    /// <summary>
    /// 文本打印组合
    /// </summary>
    /// <remarks>树枝构件</remarks>
    public class TextPrintGroup : TextPrint
    {
        private List<TextPrint> TextPrints = new List<TextPrint>();

        /// <summary>
        /// 添加子构件
        /// </summary>
        /// <param name="textPrint">子构件</param>
        public void Add(TextPrint textPrint)
        {
            TextPrints.Add(textPrint);
        }

        /// <summary>
        /// 子构件文本打印
        /// </summary>
        public void Print()
        {
            foreach (TextPrint textPrint in TextPrints)
            {
                textPrint.Print();
            }
        }
    }

    /*
        调用方式：
        TextPrint group = new TextPrintGroup();
        TextPrint group1 = new TextPrintGroup();

        FoodTextPrint foodPrint = new FoodTextPrint("apple");
        FoodTextPrint orangePrint = new FoodTextPrint("orange");
        FoodTextPrint bananaPrint = new FoodTextPrint("banana");
        
        //进行组合
        group.Add(foodPrint);
        group.Add(orangePrint);
        group1.Add(bananaPrint);
        group1.Add(group);
        
        //不同组合的打印
        group1.Print();
        group.Print();
     */
}

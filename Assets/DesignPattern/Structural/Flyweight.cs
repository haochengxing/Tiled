using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 享元模式
/// </summary>
public class Flyweight
{
    /// <summary>
    /// 文本打印对象
    /// </summary>
    /// <remarks>
    /// 非享元角色
    /// </remarks>
    public class TextPrint
    {
        public TextPrint()
        {
            this.Text = String.Empty;
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        private string Text { get; set; }

        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="text">添加的文本</param>
        public void AddText(string text)
        {
            this.Text += text;
        }

        /// <summary>
        /// 打印文本内容
        /// </summary>
        public void Print()
        {
            Debug.Log(Text);
        }
    }

    /// <summary>
    /// 文本写入享元抽象
    /// </summary>
    /// <remarks>抽象享元角色</remarks>
    public interface TextWriteFlyweight
    {
        void Write(TextPrint textPrint, string name);
    }

    /// <summary>
    /// 食物文本写入角色
    /// </summary>
    /// <remarks>具体享元角色</remarks>
    public class FoodTextWrite : TextWriteFlyweight
    {
        public void Write(TextPrint textPrint, string name)
        {
            textPrint.AddText($"Food:{name}");
        }
    }

    /// <summary>
    /// 饮品文本写入角色
    /// </summary>
    /// <remarks>具体享元角色</remarks>
    public class DrinkTextWrite : TextWriteFlyweight
    {
        public void Write(TextPrint textPrint, string name)
        {
            textPrint.AddText($"Drink:{name}");
        }
    }

    /// <summary>
    /// 文本写入享元工厂
    /// </summary>
    public class TextWriteFlyweightFactory
    {
        /// <summary>
        /// 文本写入享元字典
        /// </summary>
        private Dictionary<string, TextWriteFlyweight> TextWrites = new Dictionary<string, TextWriteFlyweight>();

        /// <summary>
        /// 获取文本写入享元对象
        /// </summary>
        /// <param name="type">类型标识</param>
        /// <returns></returns>
        public TextWriteFlyweight GetTextWriteFlyweight(string type)
        {
            if (TextWrites.ContainsKey(type))
            {
                //享元角色已存在，使用现有享元角色
                return TextWrites[type];
            }
            else
            {
                //享元角色不存在，使用工厂模式创建角色
                TextWriteFlyweight flyweight;
                switch (type)
                {
                    case "food":
                        flyweight = new FoodTextWrite();
                        break;
                    case "drink":
                        flyweight = new DrinkTextWrite();
                        break;
                    default:
                        throw new ArgumentException("type is invalid");
                }
                TextWrites.Add(type, flyweight);
                return flyweight;
            }
        }

        /*
            调用方式：
            TextPrint textPrint = new TextPrint();
            TextWriteFlyweightFactory factory = new TextWriteFlyweightFactory();
            var foodFlyweight = factory.GetTextWriteFlyweight("food");
            var drinkFlyweight = factory.GetTextWriteFlyweight("drink");
            foodFlyweight.Write(textPrint, "AAA");
            drinkFlyweight.Write(textPrint, "BBB");
            textPrint.Print();
         */
    }
}

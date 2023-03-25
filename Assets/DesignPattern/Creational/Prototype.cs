using System;
/// <summary>
/// 原型模式
/// </summary>
/// <remarks></remarks>
public class Prototype
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product : ICloneable
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Product p = new Product();
            p.Name = Name;
            p.Price = Price;
            return p;
        }

        //或者使用自带的MemberwiseClone浅克隆
        /*
        public object Clone()
        {
            return this.MemberwiseClone();
        }*/
    }

    /*
        调用方式：
        var product = new Product();
        var product1 = (Product)product.Clone();
     */
}

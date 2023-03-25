using UnityEngine;
/// <summary>
/// 建造者模式
/// </summary>
/// <remarks>
/// 分为以下结构：
/// 需要构建的类
/// 抽象建造者
/// 具体建造者
/// 指挥者
/// </remarks>
public class Builder
{
    /// <summary>
    /// 产品
    /// </summary>
    /// <remarks>
    /// 需要构建的类
    /// </remarks>
    public class Product
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        private int Price { get; set; }

        /// <summary>
        /// 设置名称
        /// </summary>
        public void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 设置价格
        /// </summary>
        public void SetPrice(int price)
        {
            this.Price = price;
        }

        /// <summary>
        /// 输出产品信息
        /// </summary>
        public void Print()
        {
            Debug.Log($"{Name}:{Price}");
        }
    }

    /// <summary>
    /// 产品抽象建造者
    /// </summary>
    public abstract class ProductBuilder
    {
        //创建产品对象
        protected Product Product = new Product();

        public abstract void BuildName();

        public abstract void BuildPrice();

        /// <summary>
        /// 返回产品对象
        /// </summary>
        /// <returns></returns>
        public Product GetProduct()
        {
            return Product;
        }
    }

    /// <summary>
    /// 食物产品构建者
    /// </summary>
    /// <remarks>具体建造者</remarks>
    public class FoodBuilder : ProductBuilder
    {
        public override void BuildName()
        {
            Product.SetName("Apple");
        }

        public override void BuildPrice()
        {
            Product.SetPrice(1);
        }
    }

    /// <summary>
    /// 饮品产品构建者
    /// </summary>
    /// <remarks>具体建造者</remarks>
    public class DrinkBuilder : ProductBuilder
    {
        public override void BuildName()
        {
            Product.SetName("Cola");
        }

        public override void BuildPrice()
        {
            Product.SetPrice(3);
        }
    }

    /// <summary>
    /// 产品构建指挥者
    /// </summary>
    public class ProductDirector
    {
        private ProductBuilder Builder;

        public ProductDirector(ProductBuilder builder)
        {
            this.Builder = builder;
        }

        /// <summary>
        /// 组装产品对象
        /// </summary>
        /// <returns></returns>
        public Product Decorate()
        {
            Builder.BuildName();
            Builder.BuildPrice();

            return Builder.GetProduct();
        }
    }

    /*
        调用方式：
        var builder = new FoodBuilder();
        var director = new ProductDirector(builder);
        director.Decorate();
     */
}

using UnityEngine;
/// <summary>
/// 装饰器模式
/// </summary>
public class Decorator
{
    /// <summary>
    /// 抽象产品
    /// </summary>
    public interface Product
    {
        void PrintName();
    }

    /// <summary>
    /// 食物
    /// </summary>
    /// <remarks>具体产品</remarks>
    public class Food : Product
    {
        public void PrintName()
        {
            Debug.Log("Food");
        }
    }

    /// <summary>
    /// 抽象装饰器
    /// </summary>
    public class ProductDecorator : Product
    {
        private Product Product;

        public ProductDecorator(Product product)
        {
            this.Product = product;
        }

        public virtual void PrintName()
        {
            this.Product.PrintName();
        }
    }

    /// <summary>
    /// 价格装饰器
    /// </summary>
    /// <remarks>装饰器具体实现</remarks>
    public class PriceProductDecorator : ProductDecorator
    {
        public PriceProductDecorator(Product product) : base(product)
        {
        }

        public override void PrintName()
        {
            base.PrintName();

            //添加打印价格的功能
            Debug.Log("Price: 100$");
        }
    }

    /*
        调用方式：
        Product product = new Food();
        Product decorator = new PriceProductDecorator(product);
        decorator.PrintName();
     */
}

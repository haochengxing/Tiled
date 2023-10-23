using System;
/// <summary>
/// 工厂模式
/// </summary>
public class Factory
{
    #region 定义

    /// <summary>
    /// 商品接口
    /// </summary>
    public interface ICommodity
    {
        /// <summary>
        /// 输出商品信息
        /// </summary>
        /// <returns></returns>
        void PrintInfo();
    }

    /// <summary>
    /// 食物商品
    /// </summary>
    public class FoodCommodity : ICommodity
    {
        public FoodCommodity(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 食物名称
        /// </summary>
        public string Name { get; set; }

        public void PrintInfo()
        {
            Console.WriteLine($"Food: {this.Name}");
        }
    }

    /// <summary>
    /// 电子商品
    /// </summary>
    public class ElectricCommodity : ICommodity
    {
        public ElectricCommodity(string model)
        {
            this.Model = model;
        }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string Model { get; set; }

        public void PrintInfo()
        {
            Console.WriteLine($"Electric: {this.Model}");
        }
    }

    #endregion

    #region 普通工厂模式
    /// <summary>
    /// 普通工厂模式
    /// 创建指定商品
    /// </summary>
    /// <remarks>
    /// 输入类型标识创建对象，但需要对类型标识了解，用工厂方法模式可解决
    /// </remarks>
    /// <param name="type">类型标识</param>
    /// <returns></returns>
    public ICommodity Create(string type)
    {
        switch (type)
        {
            case "food":
                return new FoodCommodity("Apple");
            case "electric":
                return new ElectricCommodity("REDMI K50");
            default:
                throw new ArgumentException("type is invalid");
        }
    }
    #endregion

    #region 工厂方法模式

    /*
        工厂方法模式创建对象变成了方法，不再依赖于类型标识
     */

    /// <summary>
    /// 创建食物商品
    /// </summary>
    /// <returns></returns>
    public ICommodity CreateFood()
    {
        return new FoodCommodity("Apple");
    }

    /// <summary>
    /// 创建电子商品
    /// </summary>
    /// <returns></returns>
    public ICommodity CreateElectric()
    {
        return new ElectricCommodity("REDMI K50");
    }

    /*
        调用方式：
        Factory factory = new Factory();
        factory.CreateFood();
     */

    #endregion

    #region 抽象工厂模式

    /*
        工厂方法模式在添加新类型时需要在Factory类中添加新的方法，违背了开闭原则
        将工厂抽象为一个接口，每种实现都建立对应一个工厂并实现该接口，即可解决此问题
     */

    /// <summary>
    /// 抽象工厂接口
    /// </summary>
    public interface ICommodityFactory
    {
        ICommodity Create();
    }

    /// <summary>
    /// 食物商品抽象工厂
    /// </summary>
    public class FoodCommodityFactory : ICommodityFactory
    {
        public ICommodity Create()
        {
            return new FoodCommodity("Apple");
        }
    }

    /// <summary>
    /// 电子商品抽象工厂
    /// </summary>
    public class ElectricCommodityFactory : ICommodityFactory
    {
        public ICommodity Create()
        {
            return new ElectricCommodity("REDMI K50");
        }
    }

    /*
        调用方式：
        ICommodityFactory factory = new FoodCommodityFactory();
        factory.Create();
     */
    #endregion
}

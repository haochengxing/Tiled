using UnityEngine;
/// <summary>
/// 访问者模式
/// </summary>
public class Visitor
{
    /// <summary>
    /// 商品抽象
    /// </summary>
    public interface ICommodity
    {
        void Accept(ICommodityVistor visitor);
    }

    /// <summary>
    /// 食物
    /// </summary>
    /// <remarks>具体商品</remarks>
    public class Food : ICommodity
    {
        public void Accept(ICommodityVistor visitor)
        {
            visitor.Do(this);
        }

        public string GetFoodName()
        {
            return "Apple";
        }
    }

    /// <summary>
    /// 饮品
    /// </summary>
    /// <remarks>具体商品</remarks>
    public class Drink : ICommodity
    {
        public void Accept(ICommodityVistor visitor)
        {
            visitor.Do(this);
        }

        public string GetDrinkName()
        {
            return "Cola";
        }
    }

    /// <summary>
    /// 定义商品访问者
    /// </summary>
    public interface ICommodityVistor
    {
        /// <summary>
        /// 对食物的处理
        /// </summary>
        void Do(Food food);

        /// <summary>
        /// 对饮品的处理
        /// </summary>
        void Do(Drink drink);
    }

    /// <summary>
    /// 购买访问者
    /// </summary>
    public class BuyVistor : ICommodityVistor
    {
        public void Do(Food food)
        {
            Debug.Log($"Buy Food: {food.GetFoodName()}");
        }

        public void Do(Drink drink)
        {
            Debug.Log($"Buy Drink: {drink.GetDrinkName()}");
        }
    }

    /// <summary>
    /// 打印访问者
    /// </summary>
    public class PrintVistor : ICommodityVistor
    {
        public void Do(Food food)
        {
            Debug.Log($"Print Food: {food.GetFoodName()}");
        }

        public void Do(Drink drink)
        {
            Debug.Log($"Print Drink: {drink.GetDrinkName()}");
        }
    }

    /*
        调用方式：
        ICommodity food = new Food();
        food.Accept(new PrintVistor());
        food.Accept(new BuyVistor());

        ICommodity drink = new Drink();
        drink.Accept(new PrintVistor());
        drink.Accept(new BuyVistor());
     */
}

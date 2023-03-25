using UnityEngine;
/// <summary>
/// 职责链
/// </summary>
public class ChainOfResponsibility
{
    /// <summary>
    /// 抽象处理者
    /// </summary>
    public abstract class IHander
    {
        /// <summary>
        /// 职责链的下一个处理者
        /// </summary>
        private IHander Next;

        /// <summary>
        /// 设置职责链的下一个处理者
        /// </summary>
        public void SetNext(IHander next)
        {
            this.Next = next;
        }

        /// <summary>
        /// 执行下一个处理者
        /// </summary>
        /// <remarks>公共方法</remarks>
        public void DoNext(string type)
        {
            if (Next != null)
            {
                Next.Execute(type);
            }
        }

        /// <summary>
        /// 处理方法
        /// </summary>
        public abstract void Execute(string type);
    }

    /// <summary>
    /// 食品处理者
    /// </summary>
    /// <remarks>具体处理者</remarks>
    public class FoodHandler : IHander
    {
        public override void Execute(string type)
        {
            if (type.Equals("food"))
            {
                Debug.Log("处理食品");
                return;
            }
            //如果不处理，调用下一个处理对象
            DoNext(type);
        }
    }

    /// <summary>
    /// 饮品处理者
    /// </summary>
    /// <remarks>具体处理者</remarks>
    public class DrinkHandler : IHander
    {
        public override void Execute(string type)
        {
            if (type.Equals("drink"))
            {
                Debug.Log("处理饮品");
                return;
            }
            //如果不处理，调用下一个处理对象
            DoNext(type);
        }
    }

    /*
        调用方式：
        IHander foodHander = new FoodHandler();
        IHander drinkHander = new DrinkHandler();

        //组装职责链
        foodHander.SetNext(drinkHander);

        //提交请求
        foodHander.Execute("drink");
     */
}

using UnityEngine;
/// <summary>
/// 外观模式
/// </summary>
public class Facade
{
    /// <summary>
    /// 吃饭
    /// </summary>
    /// <remarks>子系统</remarks>
    public class Eat
    {
        public void Do()
        {
            Debug.Log("Eat");
        }
    }

    /// <summary>
    /// 睡觉
    /// </summary>
    /// <remarks>子系统</remarks>
    public class Sleep
    {
        public void Do()
        {
            Debug.Log("Sleep");
        }
    }

    /// <summary>
    /// 玩
    /// </summary>
    /// <remarks>子系统</remarks>
    public class Play
    {
        public void Do()
        {
            Debug.Log("Sleep");
        }
    }

    /// <summary>
    /// 一天要做的事
    /// </summary>
    /// <remarks>外观模式</remarks>
    public class OneDayFacade
    {
        private Eat Eat = new Eat();
        private Sleep Sleep = new Sleep();
        private Play Play = new Play();

        /// <summary>
        /// 组合执行
        /// </summary>
        public void Do()
        {
            Eat.Do();
            Sleep.Do();
            Play.Do();
        }
    }

    /*
        调用方式：
        OneDayFacade oneDayFacade = new OneDayFacade();
        oneDayFacade.Do();
     */
}

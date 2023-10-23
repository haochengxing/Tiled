using UnityEngine;
/// <summary>
/// 模板方法（类）
/// </summary>
public class TemplateMethod
{
    /// <summary>
    /// 抽象模板方法类
    /// </summary>
    public abstract class PrintInfo
    {
        /// <summary>
        /// 打印信息
        /// </summary>
        /// <remarks>模板方法</remarks>
        public void Print()
        {
            var name = GetName();
            var age = GetAge();

            Debug.Log($"Name:\t{name}");
            Debug.Log($"Age:\t{age}");
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <remarks>子类实现</remarks>
        /// <returns></returns>
        public abstract string GetName();

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <remarks>子类实现</remarks>
        /// <returns></returns>
        public abstract int GetAge();
    }

    /// <summary>
    /// 小明信息打印类
    /// </summary>
    /// <remarks>具体子类</remarks>
    public class XiaoMingPrintInfo : PrintInfo
    {
        public override int GetAge()
        {
            return 11;
        }

        public override string GetName()
        {
            return "小明";
        }
    }

    /*
        调用方式：
        PrintInfo printInfo = new XiaoMingPrintInfo();
        printInfo.Print();
     */
}

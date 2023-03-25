using UnityEngine;
/// <summary>
/// 适配器模式
/// </summary>
public class Adapter
{
    /// <summary>
    /// 打印接口
    /// </summary>
    /// <remarks>
    /// 目标接口(需要适配的接口)
    /// </remarks>
    public interface Print
    {
        void Print();
    }

    /// <summary>
    /// 人类
    /// </summary>
    /// <remarks>
    /// 适配者(被适配的类型)
    /// </remarks>
    public class Person
    {
        public string GetName()
        {
            return "ABC";
        }
    }

    /// <summary>
    /// 人输出适配器类
    /// </summary>
    /// <remarks>
    /// 结构型适配器，继承需要适配的类型并实现需要适配的接口
    /// </remarks>
    public class PersonPrintAdapter : Person, Print
    {
        /// <summary>
        /// 输出人的名称
        /// </summary>
        public void Print()
        {
            Debug.Log(GetName());
        }
    }

    /*
        调用方式：
        Print print = new PersonPrintAdapter();
        print.Print();
     */

    /// <summary>
    /// 人输出适配器类
    /// </summary>
    /// <remarks>
    /// 对象型适配器
    /// </remarks>
    public class PersonPrintObjectAdapter : Print
    {
        private Person Person;

        public PersonPrintObjectAdapter(Person person)
        {
            this.Person = person;
        }

        public void Print()
        {
            Debug.Log(Person.GetName());
        }
    }

    /*
        调用方式：
        Person person = new Person();
        Print print = new PersonPrintObjectAdapter(person);
        print.Print();
     */
}

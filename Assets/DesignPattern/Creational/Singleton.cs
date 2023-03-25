using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// 单例模式
/// 只有一个实例，且提供全局访问点
/// </summary>
/// <remarks>
/// 1、减少对象创建次数
/// 2、由类内部创建实例，解除耦合关系
/// </remarks>
public class Singleton
{
    /// <summary>
    /// 饿汉式全局访问点
    /// </summary>
    public static readonly Singleton Instance = new Singleton();

    /// <summary>
    /// 私有的构造函数，保证由类内部构建
    /// </summary>
    private Singleton()
    {
    }

    private static Singleton instance = null;

    /// <summary>
    /// 懒汉式全局访问点
    /// </summary>
    /// <remarks>
    /// 声明为同步访问，防止线程安全问题
    /// </remarks>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static Singleton GetInstance()
    {
        if (instance == null)
        {
            instance = new Singleton();
        }
        return instance;
    }

    /// <summary>
    /// 测试方法
    /// </summary>
    public void Test()
    {
        Debug.Log("Hello Singleton");
    }
}

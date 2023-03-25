using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 观察者模式
/// </summary>
public class Observer
{
    /// <summary>
    /// 观察者抽象
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 被通知的处理方法
        /// </summary>
        void Handler(string state);
    }

    /// <summary>
    /// 观察者实现
    /// </summary>
    public class FoodObserver : IObserver
    {
        public void Handler(string state)
        {
            Debug.Log($"Food: {state}");
        }
    }

    /// <summary>
    /// 观察者实现
    /// </summary>
    public class DrinkObserver : IObserver
    {
        public void Handler(string state)
        {
            Debug.Log($"Drink: {state}");
        }
    }

    /// <summary>
    /// 目标对象
    /// </summary>
    /// <remarks>被观察的对象</remarks>
    public class Subject
    {
        List<IObserver> Observers = new List<IObserver>();

        public string State { get; set; }

        /// <summary>
        /// 状态改变时通知所有观察者
        /// </summary>
        /// <param name="state"></param>
        public void SetState(string state)
        {
            this.State = state;
            Notify();
        }

        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="observer"></param>
        public void AddObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        /// <summary>
        /// 通知所有观察者
        /// </summary>
        public void Notify()
        {
            foreach (IObserver observer in Observers)
            {
                observer.Handler(State);
            }
        }
    }

    /*
        调用方式：
        Subject subject = new Subject();
        subject.AddObserver(new FoodObserver());
        subject.AddObserver(new DrinkObserver());

        subject.SetState("abc");
     */
}

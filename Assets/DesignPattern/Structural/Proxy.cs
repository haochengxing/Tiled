using UnityEngine;
/// <summary>
/// 代理模式
/// </summary>
public class Proxy
{
    //抽象主题
    public interface Subject
    {
        void Request();
    }

    //真实主题
    public class RealSubject : Subject
    {
        public void Request()
        {
            Debug.Log("RealSubject");
        }
    }

    //代理
    public class RealSubjectProxy : Subject
    {
        private RealSubject RealSubject;

        public void Request()
        {
            if (RealSubject == null)
            {
                RealSubject = new RealSubject();
            }
            PreRequest();
            RealSubject.Request();
            PostRequest();
        }

        public void PreRequest()
        {
            Debug.Log("预处理");
        }

        public void PostRequest()
        {
            Debug.Log("后续处理");
        }
    }

    /*
        调用方式：
        RealSubjectProxy proxy = new RealSubjectProxy();
        proxy.Request();
     */
}

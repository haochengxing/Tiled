using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 中介者模式
/// </summary>
public class Mediator
{
    /// <summary>
    /// 聊天室
    /// </summary>
    /// <remarks>中介者</remarks>
    public class ChatRoom
    {
        List<Client> Clients = new List<Client>();

        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(Client client)
        {
            this.Clients.Add(client);
        }

        /// <summary>
        /// 转发消息
        /// </summary>
        /// <param name="client">发送者</param>
        /// <param name="message">发送消息</param>
        public void Relay(Client client, string message)
        {
            foreach (Client clientItem in Clients)
            {
                //仅转发给其他客户端
                if (!clientItem.Equals(client))
                {
                    clientItem.Receive(message);
                }
            }
        }
    }

    /// <summary>
    /// 抽象客户端
    /// </summary>
    public abstract class Client
    {
        /// <summary>
        /// 聊天室
        /// </summary>
        protected ChatRoom ChatRoom { get; set; }

        public Client(ChatRoom chatRoom)
        {
            this.ChatRoom = chatRoom;
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        public abstract void Receive(string message);

        /// <summary>
        /// 发送消息
        /// </summary>
        public abstract void Send();
    }

    /// <summary>
    /// App客户端
    /// </summary>
    public class AppClient : Client
    {
        public AppClient(ChatRoom chatRoom) : base(chatRoom)
        {
        }

        public override void Receive(string message)
        {
            Debug.Log($"App Receive: {message}");
        }

        public override void Send()
        {
            ChatRoom.Relay(this, "App Message");
        }
    }

    /// <summary>
    /// Web客户端
    /// </summary>
    public class WebClient : Client
    {
        public WebClient(ChatRoom chatRoom) : base(chatRoom)
        {
        }

        public override void Receive(string message)
        {
            Debug.Log($"Web Receive: {message}");
        }

        public override void Send()
        {
            ChatRoom.Relay(this, "Web Message");
        }
    }

    /*
        调用方式：
        ChatRoom room = new ChatRoom();
        var app = new AppClient(room);
        var web = new WebClient(room);
        room.RegisterClient(app);
        room.RegisterClient(web);

        app.Send();
        web.Send();
     */
}

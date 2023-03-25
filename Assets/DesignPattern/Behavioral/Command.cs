using UnityEngine;
/// <summary>
/// 命令模式
/// </summary>
public class Command
{
    /// <summary>
    /// 命令抽象
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }

    /// <summary>
    /// 打印命令
    /// </summary>
    public class PrintCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("Print");
        }
    }

    /// <summary>
    /// 调用者
    /// </summary>
    public class LogInvoker
    {
        /// <summary>
        /// 命令对象
        /// </summary>
        private ICommand Command;

        public LogInvoker(ICommand command)
        {
            this.Command = command;
        }

        public void SetCommand(ICommand command)
        {
            this.Command = command;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Call()
        {
            //目的：在执行前后可增加任意内容
            Debug.Log("开始执行日志");
            this.Command.Execute();
            Debug.Log("执行完毕日志");
        }
    }

    /*
        调用方式：
        ICommand command = new PrintCommand();
        LogInvoker invoker = new LogInvoker(command);
        invoker.Call();
     */
}

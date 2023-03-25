/// <summary>
/// 备忘录模式
/// </summary>
public class Memento
{
    /// <summary>
    /// 文本备忘录
    /// </summary>
    /// <remarks>存储发起人的任意状态值</remarks>
    public class TextMemento
    {
        public TextMemento(string text)
        {
            this.Text = text;
        }

        private string Text { get; set; }

        public void SetText(string text)
        {
            this.Text = text;
        }

        public string GetText()
        {
            return this.Text;
        }
    }

    /// <summary>
    /// 发起人
    /// </summary>
    /// <remarks>记录当前状态</remarks>
    public class Originator
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State { get; set; }

        public void SetState(string state)
        {
            this.State = state;
        }

        public string GetState()
        {
            return this.State;
        }

        /// <summary>
        /// 创建一个保存状态
        /// </summary>
        /// <returns></returns>
        public TextMemento CreateMemento()
        {
            return new TextMemento(State);
        }

        /// <summary>
        /// 返回一个保存状态
        /// </summary>
        public void RestoreMemento(TextMemento memento)
        {
            this.SetState(memento.GetText());
        }
    }

    /// <summary>
    /// 管理者
    /// </summary>
    /// <remarks>管理备忘录，但不能对备忘录内容修改</remarks>
    public class Caretaker
    {
        private TextMemento Memento { get; set; }

        public void SetMemento(TextMemento memento)
        {
            this.Memento = memento;
        }

        public TextMemento GetMemento()
        {
            return this.Memento;
        }
    }

    /*
        调用方式：
        Originator originator = new Originator();
        Caretaker caretaker = new Caretaker();

        originator.SetState("AAA");
        Console.WriteLine(originator.GetState());

        //保存一个状态
        caretaker.SetMemento(originator.CreateMemento());

        //赋予新状态
        originator.SetState("BBB");
        Console.WriteLine(originator.GetState());

        //恢复状态
        originator.RestoreMemento(caretaker.GetMemento());
        Console.WriteLine(originator.GetState());
     */
}

using UnityEngine;
/// <summary>
/// 状态模式
/// </summary>
public class State1
{
    /// <summary>
    /// 抽象播放器状态类
    /// </summary>
    public abstract class PlayerState
    {
        /// <summary>
        /// 执行状态
        /// </summary>
        /// <param name="musicPlayer">音乐播放器</param>
        public abstract void Handle(MusicPlayer musicPlayer);

        /// <summary>
        /// 当前状态字符串
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
    }

    /// <summary>
    /// 开始状态
    /// </summary>
    public class StartState : PlayerState
    {
        public override void Handle(MusicPlayer musicPlayer)
        {
            musicPlayer.SetState(this);
        }

        public override string ToString()
        {
            return "Start";
        }
    }

    /// <summary>
    /// 停止状态
    /// </summary>
    public class StopState : PlayerState
    {
        public override void Handle(MusicPlayer musicPlayer)
        {
            musicPlayer.SetState(this);
        }

        public override string ToString()
        {
            return "Stop";
        }
    }


    /// <summary>
    /// 音乐播放器
    /// </summary>
    /// <remarks>环境类</remarks>
    public class MusicPlayer
    {
        public PlayerState State { get; set; }

        public void SetState(PlayerState state)
        {
            this.State = state;
        }

        /// <summary>
        /// 打印当前状态
        /// </summary>
        public void PrintState()
        {
            Debug.Log(State.ToString());
        }
    }

    /*
        调用方式：
        MusicPlayer musicPlayer = new MusicPlayer();

        //执行Start状态
        StartState start = new StartState();
        start.Handle(musicPlayer);
        musicPlayer.PrintState();

        //执行Stop状态
        StopState stop = new StopState();
        stop.Handle(musicPlayer);
        musicPlayer.PrintState();
     */
}

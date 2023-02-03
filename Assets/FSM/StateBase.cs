using System;

/// <summary>
/// 状态对象基类
/// 之后的所以状态都继承自这个基类
/// 如：idel wlak attack
/// </summary>
public abstract class StateBase
{
    //当前 状态对象 代表的 枚举状态
    public Enum stateType;
    //当前控制的角色（角色控制器）
    public FSMController controller;

    public virtual void Init(FSMController controller, Enum stateType)
    {
        this.controller = controller;
        this.stateType = stateType;
    }

    //进入该状态时
    public abstract void OnEnter();
    //更新该状态时
    public abstract void OnUpdate();
    //退出该状态时
    public abstract void OnExit();
}

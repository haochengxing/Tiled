using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class FSMController : MonoBehaviour
{
    //当前状态
    public abstract Enum CurretState { get; set; }
    //当前的状态对象
    protected StateBase CurrStateObj;
    //保存所有可使用的状态（避免重复的创建或销毁对象影响性能）
    private List<StateBase> stateList = new List<StateBase>(16);


    public void AddState(Enum stateType)
    {
        StateBase state = Activator.CreateInstance(Type.GetType(stateType.ToString())) as StateBase;
        state.Init(this, stateType);
        stateList.Add(state);
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="NetState"></param>
    /// <param name="reCurrState"></param>
    public void ChangeState(Enum newState, bool reCurrState = false)
    {
        //如果要改变的新状态与当前状态相同 并且 不需要更新状态 则 返回
        if (newState == CurretState && !reCurrState) return;
        //如果当前持有的状态对象不为空 则 退出当前持有的状态对象
        if (CurrStateObj != null) CurrStateObj.OnExit();

        //获取要改变的新状态对象并执行进入状态时的逻辑
        CurrStateObj = GetStateObj(newState);
        CurrStateObj.OnEnter();
    }

    /// <summary>
    /// 获取状态对象
    /// 给出一个枚举，返回与这个枚举同名的状态对象
    /// 要确保返回值不为空
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    private StateBase GetStateObj(Enum stateType)
    {
        //如果列表中已有，返回已有的状态对象
        for (int i = 0; i < stateList.Count; i++)
        {
            if (stateList[i].stateType == stateType) return stateList[i];
        }
        //如果没有，创建新的状态对象并加入表中
        //反射  根据名称创建对象实例
        StateBase state = Activator.CreateInstance(Type.GetType(stateType.ToString())) as StateBase;
        state.Init(this, stateType);
        stateList.Add(state);

        return state;
    }

    protected virtual void Update()
    {
        if (CurrStateObj != null) CurrStateObj.OnUpdate();
    }
}

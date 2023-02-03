using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkEnd : StateBase
{
    private float curTime = 0f;
    private float aniTime = 0f;
    public override void OnEnter()
    {
        curTime = 0f;
        aniTime = Random.Range(1f, 3f);

        Debug.LogFormat("结束攻击 等待{0}秒", aniTime);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        curTime += Time.deltaTime;
        if (curTime >= aniTime)
        {
            controller.ChangeState(State.AtkStart);
        }
    }
}

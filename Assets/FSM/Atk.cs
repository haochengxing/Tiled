using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk : StateBase
{
    private float curTime = 0f;
    private float aniTime = 0f;
    public override void OnEnter()
    {
        curTime = 0f;
        aniTime = Random.Range(5f, 10f);

        Debug.LogFormat("正在攻击 持续{0}秒", aniTime);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        curTime += Time.deltaTime;
        if (curTime >= aniTime)
        {
            controller.ChangeState(State.AtkEnd);
        }
    }
}

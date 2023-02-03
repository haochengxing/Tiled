using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkStart : StateBase
{
    private float curTime = 0f;
    private float aniTime = 0f;
    public override void OnEnter()
    {
        System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        var timestamp = System.Convert.ToInt32(ts.TotalSeconds);


        Random.InitState(timestamp);

        curTime = 0f;
        aniTime = Random.Range(2f, 5f);

        Debug.LogFormat("开始攻击 等待{0}秒", aniTime);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        curTime += Time.deltaTime;
        if(curTime>= aniTime)
        {
            controller.ChangeState(State.Atk);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTest : FSMController
{
    public override Enum CurretState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        AddState(State.AtkStart);
        AddState(State.Atk);
        AddState(State.AtkEnd);


        ChangeState(State.AtkStart);
    }

    // Update is called once per frame

}

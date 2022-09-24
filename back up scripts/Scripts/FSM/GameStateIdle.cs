using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateIdle : GameStateBase
{
    public override void EnterState(GameManager manager){}
    public override void Update(GameManager manager){
        if (Input.GetKeyDown(KeyCode.O)) {
            manager.ChangeState(manager.stateIntro);
        }
    }
    public override void LeaveState(GameManager manager){}
}

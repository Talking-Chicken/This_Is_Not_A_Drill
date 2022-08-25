using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateIntro : GameStateBase
{
    public override void EnterState(GameManager manager){
        manager.StartCoroutine(manager.waitToChangeState(5, manager.statePlay));
    }
    public override void Update(GameManager manager){}
    public override void LeaveState(GameManager manager){}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateReview : GameStateBase
{
    public override void EnterState(GameManager manager){
        manager.roundEnd();
    }
    
    public override void Update(GameManager manager){
        //recieve signal of whether game continued or game has ended
        if (Input.GetKeyDown(KeyCode.O)) {
            manager.ChangeState(manager.statePlay);
        }
        if (Input.GetKeyDown(KeyCode.P))
            manager.ChangeState(manager.stateEnd);
    }
    public override void LeaveState(GameManager manager){
        manager.ActiveCards.Clear();
        foreach(ActivityRole role in manager.Roles) {
            role.Activity = null;
        }
    }
}

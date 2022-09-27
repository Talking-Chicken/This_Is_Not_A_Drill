using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateReview : GameStateBase
{
    public override void EnterState(GameManager manager){
        manager.roundEnd();
        manager.UiControl.CurrentMonthIndex = 0;
        manager.UiControl.StopAllCoroutines();
        manager.StartCoroutine(manager.waitToChangeState(1.5f, manager.statePlay));
        manager.resetTimer();
        manager.UiControl.SecondText.text = "00";
    }
    
    public override void Update(GameManager manager){
        //recieve signal of whether game continued or game has ended
        
        if (Input.GetKeyDown(KeyCode.O)) {
            manager.ChangeState(manager.statePlay);
        }
        if (Input.GetKeyDown(KeyCode.P))
            manager.ChangeState(manager.stateEnd);
        
        if (int.Parse(manager.UiControl.YearText.text) >= manager.EndYear)
            manager.StartCoroutine(manager.waitToChangeState(5, manager.stateEnd));
    }
    public override void LeaveState(GameManager manager){
        manager.ActiveCards.Clear();
        foreach(ActivityRole role in manager.Roles) {
            role.Activity = null;
        }
    }
}

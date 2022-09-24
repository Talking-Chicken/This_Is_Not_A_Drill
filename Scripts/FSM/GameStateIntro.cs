using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateIntro : GameStateBase
{
    public override void EnterState(GameManager manager){
        //clear remaining data from last game
        manager.ActiveCards.Clear();
        foreach(ActivityRole role in manager.Roles) {
            role.Activity = null;
            role.reset();
        }
        //manager.StartCoroutine(manager.waitToChangeState(5, manager.statePlay));
    }

    public override void Update(GameManager manager){
        manager.showRoleNames();

        if (SceneManager.GetActiveScene().name.Equals("scene3"))
            manager.ChangeState(manager.statePlay);
    }

    public override void LeaveState(GameManager manager){
        manager.UiControl.showGameUI();
    }
}

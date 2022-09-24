using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateIdle : GameStateBase
{
    public override void EnterState(GameManager manager){}
    public override void Update(GameManager manager){
        
        if (manager.isManualControl) {
            if (Input.GetKeyDown(KeyCode.O)) {
                manager.ChangeState(manager.stateIntro);
            }
        } else {
            if (SceneManager.GetActiveScene().name.Equals("scene2"))
                manager.ChangeState(manager.stateIntro);
        }
    }
    public override void LeaveState(GameManager manager){}
}

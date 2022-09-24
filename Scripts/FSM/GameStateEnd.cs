using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEnd : GameStateBase
{
    public override void EnterState(GameManager manager){}
    public override void Update(GameManager manager){
        foreach (ActivityRole role in manager.Roles) {
            role.NarrativeText.text = role.ActivityClass + " has prevented x amout of carbon emission. However, their living quality has changed from " + role.InitialScore + " to " + role.Score;
            manager.rebuildUI();
        }
    }
    public override void LeaveState(GameManager manager){}
}

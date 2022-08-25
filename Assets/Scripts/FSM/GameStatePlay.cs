using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatePlay : GameStateBase
{
    public override void EnterState(GameManager manager){}
    public override void Update(GameManager manager){
        //recieve cards info and show them on the UI
        manager.showTappedCards();

        if(Input.GetKeyDown(KeyCode.Q))
            manager.addToActiveCards("Oneself");
        if(Input.GetKeyDown(KeyCode.W))
            manager.addToActiveCards("Divest");
        if(Input.GetKeyDown(KeyCode.E))
            manager.addToActiveCards("APSS");
        if(Input.GetKeyDown(KeyCode.R))
            manager.addToActiveCards("Reduce energy use");

        //if recieved signal that the round has end, change to review state
        if (Input.GetKeyDown(KeyCode.O)) {
            manager.ChangeState(manager.stateReview);
        }
    }
    public override void LeaveState(GameManager manager){}
}
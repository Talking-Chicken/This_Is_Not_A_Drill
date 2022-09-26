using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatePlay : GameStateBase
{
    public override void EnterState(GameManager manager){
        manager.UiControl.showGameUI();
        //manager.UiControl.StartCoroutine(manager.UiControl.monthCountDown());
        manager.IsFirstCard = true;
    }
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

        if (manager.IsFirstCard && manager.addToACtiveCardsFromPython()) {
            manager.UiControl.monthCountDown();
            manager.IsFirstCard = false;
        }
        
        if (manager.IsFirstCard && Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Coutning down");
            manager.UiControl.StartCoroutine(manager.UiControl.monthCountDown());
            manager.IsFirstCard = false;
        }

        if (!manager.IsFirstCard)
            if (!manager.timerCountDown(15))
                if (15-(int)manager.CurrentTime < 10)
                    manager.UiControl.SecondText.text = "0"+(15-(int)manager.CurrentTime)+"";
                else
                    manager.UiControl.SecondText.text = 15-(int)manager.CurrentTime+"";

        //if recieved signal that the round has end, change to review state
        if (Input.GetKeyDown(KeyCode.O)) {
            manager.ChangeState(manager.stateReview);
        }
    }
    
    public override void LeaveState(GameManager manager){
        if (manager.IsFirstRound) {
            manager.IsFirstRound = false;
            foreach (ActivityRole role in manager.Roles) {
                if (role.Activity != null)
                    role.IsInGame = true;
            }
        }

        manager.IsFirstCard = false;
        manager.UiControl.stopMonthCountDown();
    }
}

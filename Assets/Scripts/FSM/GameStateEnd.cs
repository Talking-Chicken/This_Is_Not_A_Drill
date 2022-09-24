using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEnd : GameStateBase
{
    public override void EnterState(GameManager manager){}
    public override void Update(GameManager manager){
        float totalCarbon = 0.0f;
        int inGameRoleCount = 0;
        foreach (ActivityRole role in manager.Roles) {
            totalCarbon += role.CarbonEmission;
            if (role.IsInGame)
                inGameRoleCount++;
        }

        foreach (ActivityRole role in manager.Roles) {
            role.NarrativeText.text = role.ActivityClass + " has prevented " + (int)(role.CarbonEmission / totalCarbon) + "% of carbon emission " +
                                      "among all " + inGameRoleCount + " roles. However, ";
            switch (role.ActivityClass.Trim().ToLower()) {
                case "working class":
                case "middle class":
                case "upper class":
                    role.NarrativeText.text += "living quality ";
                    break;
                case "company":
                    role.NarrativeText.text += "revenue ";
                    break;
                case "policymaker":
                    role.NarrativeText.text += "influence ";
                    break;
                default:
                    role.NarrativeText.text = "can't find class " + role.ActivityClass;
                    break;
            }
            role.NarrativeText.text += "has changed from " + role.InitialScore + " to " + role.Score;

            string mostActivity = "";
            int currentCount = 0;
            foreach(var activityCount in role.ActivityCounts) {
                if (activityCount.Value > currentCount) {
                    mostActivity = activityCount.Key;
                    currentCount = activityCount.Value;
                }
            }
            manager.rebuildUI();
        }
    }
    public override void LeaveState(GameManager manager){}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEnd : GameStateBase
{
    public override void EnterState(GameManager manager){
        //manager.waitToChangeState(5.0f, manager.stateIdle);
        manager.UiControl.showGameEndUI();
    }
    public override void Update(GameManager manager){
        float totalCarbon = 0.0f;
        int inGameRoleCount = 0;
        foreach (ActivityRole role in manager.Roles) {
            totalCarbon += role.CarbonEmission;
            if (role.IsInGame)
                inGameRoleCount++;
        }

        foreach (ActivityRole role in manager.Roles) {
            if (role.IsInGame) {
                role.EndNarrativeText.text = role.ActivityClass + " has prevented " + ((int)((role.CarbonEmission / totalCarbon)* 100)) + "% of carbon emission " +
                                        "among all " + inGameRoleCount + " roles. However, ";
                switch (role.ActivityClass.Trim().ToLower()) {
                    case "working class":
                    case "middle class":
                    case "upper class":
                        role.EndNarrativeText.text += "living quality ";
                        break;
                    case "company":
                        role.EndNarrativeText.text += "revenue ";
                        break;
                    case "policymaker":
                        role.EndNarrativeText.text += "influence ";
                        break;
                    default:
                        role.EndNarrativeText.text = "can't find class " + role.ActivityClass;
                        break;
                }
                role.EndNarrativeText.text += "has changed from " + role.InitialScore + " to " + role.Score;

                string mostActivity = "";
                int currentCount = 0;
                foreach(var activityCount in role.ActivityCounts) {
                    if (activityCount.Value > currentCount) {
                        mostActivity = activityCount.Key;
                        currentCount = activityCount.Value;
                    }
                }

                //swtich to each line afterwards
                role.EndNarrativeText.text += " . In this game, " + role.ActivityClass + " used " + mostActivity + " the most.";
                role.EndNarrativeText.text += getActivityDes(mostActivity);
            } else {
                role.EndNarrativeText.text = role.ActivityClass + " didn't participate in the game.";
            }   
            manager.rebuildUI();
        }

        if (!manager.timerCountDown(10))
            manager.UiControl.EndGameSecondText.text = 10-(int)manager.CurrentTime+"";
        else
            manager.ChangeState(manager.stateIdle);
    }
    public override void LeaveState(GameManager manager){
        manager.UiControl.hideGameEndUI();
    }

    //return the description of the most used activity
    private string getActivityDes(string mostActivity) {
        if (mostActivity != null || !mostActivity.Equals("")) {
            switch (mostActivity.Trim().ToLower()) {
                //working class
                case "riot":
                    return "Working class took the aggresive way, it's effective in some way. However, it's impacting their lives as well.";
                case "working class public transportation":
                    return "Working class changed their way of commute to public transportation. Although, they may meet different circumstances like" + 
                           "delayed, cancelled trains, and strange passengers, it's convinience. Hope they think the benefit is greater than the down side.";
                case "working class local seasonal food":
                    return "Working class bought a lot of local and seasonal foods. The variation of food choice is limited. Perhaps they are not that " + 
                           "picky about their food :)";
                case "working class building":
                    return "Working class moved to a new building! NEW HOME! But, how about their rising living fee, and their longer commute time?";
                case "protest":
                    return "Working class took that aggresive way, not that aggresive comparing to riot, but they showed their determintation. Protest " + 
                           "definitely consumes time and energy. Hope it's not affecting their lives that much.";
                case "working class meat":
                    return "Working class bought much less meats in the past 20 years. Just saying that vegitables are not necessarily cheaper " + 
                           "than meats, and one needs to consume more vegitables to feel \"full\". ";
                case "oneself":
                    return "Working class admitted that their class can't do much comparing to other classes. Only they are actively changin their " + 
                           "living style is not enough to make a impact.";
                
                //middle class
                case "ac":
                    return "Middle class reduced their ac usage in the past 20 years. Honestly, AC do have some sign on climate change. Middle class wants " + 
                           " to sacrifie their living quality for environment. And, this sacrification can grow bigger, since the global climate " + 
                           " is still rising.";
                case "middle class public transportation":
                    return "Middle class rided much more public transportation than before. They may have ";
                case "middle class meat":
                    return "Middle class reduced their meat consumption by a huge amount in the past 20 years. Their living fee didn't go down because of " + 
                           "their choice, and they did contribute something to the whole global climate change situation. At least, hope they feel " + 
                           "healthy.";
                case "building":
                    return "Middle class changed their home to the building that generate power by itself. Living fees go down, but the commute time and " +
                           "rent/mortgage bordered them for a long time.";
                case "climate injustice":
                    return "Middle class fight for climate injustice a lot in the past 20 years. They spended their time and energy to convey their idea. " +
                           "People have different opinions of whether it's worth or not.";
                case "middle class local seasonal food":
                    return "Middle class bought as much local seasonal food as they can, when they making food. In such way, they decreases the demand of " +
                           "transporting foods into different cities. Thus, the usage of truck may decreases, but how about those truck drivers?";
                case "not enough power":
                    return "It's really hard for the middle class alone to drive change. Or, even it's hard for the whole humanity to drive change. At " +
                           "that's what middle class was thinking during those past 20 years.";
                
                //upper class
                case "fly":
                    return "Upper class canceled a bunch of flight during the past 20 years. ";
                case "divest":
                    return "Divest";
                case "electric car":
                    return "electric car";
                case "upper class public transportation":
                    return "";
            }
        }
        return "";
    }
}

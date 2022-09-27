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
                //role.EndNarrativeText.text += " . In this game, " + role.ActivityClass + " used " + mostActivity + " the most.";
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
                    return "Upper class canceled a bunch of flight during the past 20 years. They are slowly getting used to longer commute times, less " +
                           "business trips, and less long distance trips for vocations. Lives of them feels impacted, but they controled their carbon "+
                           "foot prints.";
                case "divest":
                    return "Upper class divested those non-environmental friendly company in the past 20 years. Some of them loses money that they can "+
                           "potentially make, some invested in newly formed businesses. And, some of their decisions decides the fate of the corporation.";
                case "electric car":
                    return "Upper class changed to electric car. It's a trend. Though, it takes longer to charge and they can't enjoy the noise of "+
                           "motor, it uses no fossil fuels. The question is whether those electricity are generated by those fuels.";
                case "upper class public transportation":
                    return "Upper class took transportation the most in the past 20 years, trying to reduce their carbon emission. Without massive "+
                           "emissions from their daily vehicles, they limited their carbon foot prints a lot, but with the cost of situations that "+
                           "happens because of old public transportation system.";
                case "upper class local seasonal food":
                    return "Upper class got rid of their habit of diet in the past 20 years, turned toward local and seasonal food. Choices might be "+
                           "limited, but with their chef, it was not a big change.";
                case "others do it":
                    return "Let's take sometime to think. It's really makeing no reason for upper class to be the first that stand out to fight global "+
                           "climate change. \"Where's government?\" that's what they are thinking right now.";
                case "advertise":
                    return "Reputation is important for a person, especially for a person who wants to grant attentions. To be a role model of reducing "+
                           "carbon footprints becomes the new fanshion among upper class people, trying to make them distinguish from others.";

                //corporation
                case "change plastic package":
                    return "In the past 20 years, corporation changed all their single-used packages to more dissolvable one. It was a long term process "+
                           "and those changes have cost. Fortunately, there're always solutions that can making customers paying for that.";
                case "apss":
                    return "Corporations prepared for years to upgrade their Advance Production Scheduling System.";
                case "nuclear":
                    return "Nuclear energy took the most time in the past 20 years for the corporation.";
                case "renewable energy":
                    return "renewable energy took the most time in the past 20 years for the corporation.";
                case "product lives":
                    return "Increasing product lives become the most long-term task for the corporation in the past 20 years.";
                case "reduce energy use":
                    return "Corporations tried to save energy from their working space the most in the past 20 years.";
                case "not enough benefit":
                    return "There's really no reason for corporations to not maximize their profits. Customers should be thankful for corporations that "+
                           "willing to do things for global climate change, and corporations shouldn't be forced to do that.";
                
                //policymaker
                case "ban ads":
                    return "The most time consuming policy that policymakers tried to push in the past 20 years is ban non-environmental friendly ads.";
                case "control fossil fuel":
                    return "Policymakers tried really hard for getting the bill passed. They asked to control fossil fuel to save the earth.";
                case "single-use plastic":
                    return "Policumakers took care of reducing single-use plastic the most in the past 20 years.";
                case "education":
                    return "Policymakers thought educate next generations is the most important thing that can unite humanity to fight against climate "+
                           "change.";
                case "reduce new road":
                    return "Policymakers made sure that new road instructions should be reduced in the past 20 years.";
                case "don't change a lot":
                    return "A huge change usually won't bring a satisfying result.";
                case "defend":
                    return "Policymakers thought that it's more urgent to defend sea level rising, instead of trying to control it.";
            }
        }
        return "";
    }
}

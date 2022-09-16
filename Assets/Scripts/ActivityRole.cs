using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityRole
{
    private string activityClass, firstNarrative, secondNarrative, scoreName;
    private List<string> firstPendingList = new List<string>(), secondPendingList = new List<string>();
    private List<int> firstPriorityList = new List<int>(), secondPriorityList = new List<int>(),
                      firstScoreList = new List<int>(), secondScoreList = new List<int>();
    private HumanActivity activity;
    private bool isInGame = false;
    private TextMeshProUGUI narrativeText;
    private int score, initialScore;

    //getters & setters
    public string ActivityClass{get=>activityClass;set=>activityClass=value;}
    public string FirstNarrative{get=>firstNarrative;set=>firstNarrative=value;}
    public string SecondNarrative{get=>secondNarrative;set=>secondNarrative=value;}
    public HumanActivity Activity{get=>activity;set=>activity=value;}
    public bool IsInGame{get=>isInGame;set=>isInGame=value;}
    public List<string> FirstPendingList{get=>firstPendingList;set=>firstPendingList=value;}
    public List<string> SecondPendingList{get=>secondPendingList;set=>secondPendingList=value;}
    public List<int> FirstPriorityList{get=>firstPriorityList;set=>firstPriorityList=value;}
    public List<int> SecondPriorityList{get=>secondPriorityList;set=>secondPriorityList=value;}
    public List<int> FirstScoreList{get=>firstScoreList;set=>firstScoreList=value;}
    public List<int> SecondScoreList{get=>secondScoreList;set=>secondScoreList=value;}
    public TextMeshProUGUI NarrativeText{get=>narrativeText; set=>narrativeText=value;}
    public int Score{get=>score;set=>score=value;}
    public int InitialScore{get=>initialScore;set=>initialScore=value;}
    public string ScoreName{get=>scoreName;set=>scoreName=value;}
    
    public ActivityRole(string className) {
        ActivityClass = className;

        //set to correct initial score
        switch (className.Trim().ToLower()) {
            case "working class":
                scoreName = "Living Quality";
                InitialScore = 7;
                break;
            case "middle class":
                scoreName = "Living Quality";
                InitialScore = 12;
                break;
            case "upper class":
                scoreName = "Living Quality";
                InitialScore = 16;
                break;
            case "company":
                scoreName = "Revenue";
                InitialScore = 40;
                break;
            case "policymaker":
                scoreName = "Influence";
                InitialScore = 10;
                break;
            default:
                Debug.Log("can't find class " + className.Trim().ToLower() + " while giving initial score");
                break;
        }
        Score = InitialScore;
    }

    public void reset() {
        Score = InitialScore;
        FirstNarrative = "";
        SecondNarrative = "";
        FirstPendingList.Clear();
        SecondPendingList.Clear();
        FirstPriorityList.Clear();
        SecondPriorityList.Clear();
        IsInGame = false;
    }
}

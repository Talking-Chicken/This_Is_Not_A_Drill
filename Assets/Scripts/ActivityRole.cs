using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityRole
{
    private string activityClass, firstNarrative, secondNarrative;
    private List<string> firstPendingList = new List<string>(), secondPendingList = new List<string>();
    private List<int> firstPriorityList = new List<int>(), secondPriorityList = new List<int>();
    private HumanActivity activity;
    private bool isInGame = false;
    private TextMeshProUGUI narrativeText;

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
    public TextMeshProUGUI NarrativeText{get=>narrativeText; set=>narrativeText=value;}
    
    public ActivityRole(string className) {
        ActivityClass = className;
    }
}

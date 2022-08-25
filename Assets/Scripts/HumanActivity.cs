using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

[Serializable]
public class HumanActivity
{
    [SerializeField] private string activityName, displayName;
    [SerializeField] private List<int> startYears = new List<int>(), endYears = new List<int>();
    [SerializeField] private int duration = 0;
    [SerializeField] private string activityClass, activityGroup;
    [SerializeField] private List<string> firstNarratives = new List<string>(), secondNarratives = new List<string>(), 
                                          firstConditions = new List<string>(), secondConditions = new List<string>(),
                                          secondAffectingTypes = new List<string>();
    [SerializeField] private List<int> firstActivityPriorities = new List<int>(), secondActivityPriorities = new List<int>();
    private bool isActivating = false;

    //getters & setters
    public string ActivityName {get=>activityName;set=>activityName=value;}
    public string DisplayName {get=>displayName;set=>displayName=value;}
    public List<int> StartYears {get=>startYears;set=>startYears=value;}
    public List<int> EndYears {get=>endYears;set=>endYears=value;}
    public int Duration {get=>duration;set=>duration=value;}
    public List<string> FirstNarratives {get=>firstNarratives; set=>firstNarratives = value;}
    public List<string> SecondNarratives {get=>secondNarratives; set=>secondNarratives = value;}
    public List<string> FirstConditions {get=>firstConditions;set=>firstConditions=value;}
    public List<string> SecondConditions {get=>secondConditions;set=>secondConditions=value;}
    public List<string> SecondAffectingTypes {get=>secondAffectingTypes;set=>secondAffectingTypes=value;}
    public List<int> FirstActivityPriorities {get=>firstActivityPriorities;set=>firstActivityPriorities=value;}
    public List<int> SecondActivityPriorities {get=>secondActivityPriorities;set=>secondActivityPriorities=value;}
    public string ActivityClass {get=>activityClass;set=>activityClass = value;}
    public string ActivityGroup {get=>activityGroup;set=>activityGroup=value;}

    public HumanActivity() {
        
    }

    public void activateForAYear(int currentYear) {
        if (!isActivating)
            StartYears.Add(currentYear);
        isActivating = true;
        Duration++;
    }

    /*return the most recent end year
      if haven't end, return -1*/
    public int getRecentEndYear() {
        if (EndYears.Count >= StartYears.Count)
            return EndYears[EndYears.Count-1];
        else
            return -1;
    }

}

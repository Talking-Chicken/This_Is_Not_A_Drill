using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

public enum ActivityClass{Working, Middle, Upper, Company, Policymaker}

[Serializable]
public class HumanActivity
{
    [SerializeField] private string activityName;
    [SerializeField] private List<int> startYears, endYears;
    private int duration = 0;
    [SerializeField] private ActivityClass classType;
    private bool isActivating = false;
    private string narrative = "";

    //getters & setters
    public string ActivityName {get=>activityName;private set=>activityName=value;}
    public List<int> StartYears {get=>startYears;set=>startYears=value;}
    public List<int> EndYears {get=>endYears;set=>endYears=value;}
    public int Duration {get=>duration;set=>duration=value;}
    public ActivityClass ClassType{get=>classType;private set=>classType=value;}

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

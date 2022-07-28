using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ActivityClass{Working, Middle, Upper, Company, Policymaker}

[Serializable]
public class HumanActivity
{
    private string activityName;
    private int startYear, lastEndYear, duration;
    private ActivityClass classType;

    //getters & setters
    public string ActivityName {get=>activityName;private set=>activityName=value;}
    public int StartYear {get=>startYear; set=>startYear=value;}
    public int LastEndYear {get=>lastEndYear; set=>lastEndYear=value;}
    public int Duration {get=>duration;private set=>duration=value;}
    public ActivityClass ClassType{get=>classType;private set=>classType=value;}
}

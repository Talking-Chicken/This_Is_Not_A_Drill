using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentYear;
    private float currentTime, deltaYearTime;
    [SerializeField] private int startYear, endYear;
    [SerializeField] private float totalDuration; //time in seconds

    //getters & setters
    public int CurrentYear{get=>currentYear;}

    void Start()
    {
        currentYear = startYear;
        deltaYearTime = totalDuration/Mathf.Abs(endYear-startYear);
    }

    
    void Update()
    {
        if (currentTime >= deltaYearTime) {
            currentTime = 0;
            currentYear++;
        }
        else
            currentTime += Time.deltaTime;
    }
}

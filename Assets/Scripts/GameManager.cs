using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    private int currentYear;
    private float currentTime, deltaYearTime;
    [SerializeField] private int startYear, endYear;
    [SerializeField] private float totalDuration; //time in seconds
    private List<HumanActivity> activatingActivities = new List<HumanActivity>(), DeactivatingActivities = new List<HumanActivity>();

    [SerializeField] private ActivityData data;
    [SerializeField] private TextAsset cardFile;
    private UIControl uiControl;

    //getters & setters
    public int CurrentYear{get=>currentYear;private set=>currentYear=value;}

    void Start()
    {
        currentYear = startYear;
        deltaYearTime = totalDuration/Mathf.Abs(endYear-startYear);
        ParseCSV(cardFile.text);
        uiControl = FindObjectOfType<UIControl>();

        StartCoroutine(timeCountDown(deltaYearTime));
    }

    
    void Update()
    {
        // if (currentTime >= deltaYearTime) {
        //     nextYear();
        // }
        // else
        //     currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
            data.Activities[0].StartYears.Add(1);
    }

    public void nextYear() {
        currentTime = 0;
        CurrentYear = Mathf.Min(CurrentYear+1, endYear);
        uiControl.randomChangeText();
    }

    IEnumerator timeCountDown(float waitSecond) {
        yield return new WaitForSeconds(waitSecond);

        if (currentYear < endYear) {
            nextYear();
            StartCoroutine(timeCountDown(waitSecond));
        }
    }

    private string[][] ParseCSV(string fileString)
    {
        //split the lines on @/% to separate cards
        string[] cards = fileString.Split('@');
        int numCards = cards.Length;

        foreach(string str in cards)
            Debug.Log("aaa: " + str);

        // Split the lines on newline.
        string[] rows = fileString.Split(System.Environment.NewLine.ToCharArray()); 
        int numRows = rows.Length;     

        if (numRows == 0) return null; // Empty file

        string [][] parsed = new string [numRows][];
        for (int r = 0; r < numRows; r++)
        {
            parsed[r] = rows[r].Split(','); // Split the line on comma.
        }

        return parsed;
    }
}

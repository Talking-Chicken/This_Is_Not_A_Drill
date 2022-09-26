using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI yearText, workingText, middleText, upperText, companyText, policymakerText, monthText;
    [SerializeField] private Color workingColor, middleColor, upperColor, companyColor, policymakerColor;
    [SerializeField] RectTransform descriptions, workingBG, middleBG, upperBG, companyBG, policyBG;
    private List<TextMeshProUGUI> textGroup;

    //months
    private string[] months = new string[]{"", "Jan", "Feb", "March", "April", "May", "Jun", "July", "Aug", "Sep", "Oct", "Nov", "Dec"};
    [SerializeField] private float playDuration;
    private float currentPlayTime, playdeltaTime;
    private int currentMonthIndex = 0, monthLoopCountDown = 0;

    [SerializeField] private string[] randomText;

    //getters & setters
    public TextMeshProUGUI WorkingText{get=>workingText;}
    public TextMeshProUGUI MiddleText{get=>middleText;}
    public TextMeshProUGUI UpperText{get=>upperText;}
    public TextMeshProUGUI CompanyText{get=>companyText;}
    public TextMeshProUGUI PolicymakerText{get=>policymakerText;}
    public RectTransform Descriptions{get=>descriptions;}
    public int CurrentMonthIndex{get=>currentMonthIndex;set=>currentMonthIndex=value;}

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        gameManager = FindObjectOfType<GameManager>();

        workingText.color = workingColor;
        middleText.color = middleColor;
        upperText.color = upperColor;
        companyText.color = companyColor;
        policymakerText.color = policymakerColor;
        
        textGroup = new List<TextMeshProUGUI>() {workingText, middleText, upperText, companyText, policymakerText};

        playdeltaTime = playDuration / 24;
    }

    void Update()
    {
        if (gameManager != null)
            yearText.text = gameManager.CurrentYear+"";
        
        // workingText.text = gameManager.Roles[0].FirstNarrative + " " + gameManager.Roles[0].SecondNarrative;
        // middleText.text = gameManager.Roles[1].FirstNarrative + " " + gameManager.Roles[1].SecondNarrative;
        // upperText.text = gameManager.Roles[2].FirstNarrative + " " + gameManager.Roles[2].SecondNarrative;
        // companyText.text = gameManager.Roles[3].FirstNarrative + " " + gameManager.Roles[3].SecondNarrative;
        // policymakerText.text = gameManager.Roles[4].FirstNarrative + " " + gameManager.Roles[4].SecondNarrative;
    }

    public IEnumerator monthCountDown() {
        if (monthText.text.Equals("Dec")) {
            monthLoopCountDown++;
            gameManager.CurrentYear++;
            if (monthLoopCountDown < 2) {
                monthText.text = months[1];
                currentMonthIndex = 1;
            } else {
                monthText.text = months[0];
                currentMonthIndex = 0;
            }
        }
        else {
            currentMonthIndex++;
            monthText.text = months[currentMonthIndex];
        }
        
        yield return new WaitForSeconds(playdeltaTime);
        
        if (monthLoopCountDown < 2)
            StartCoroutine(monthCountDown());
        else {
            stopMonthCountDown();
            gameManager.ChangeState(gameManager.stateReview);
        }
    }

    public void stopMonthCountDown() {
        StopCoroutine(monthCountDown());
        monthText.text = months[0];
        monthLoopCountDown = 0;
    }

    public void hideGameUI() {
        Descriptions.gameObject.SetActive(false);
    }

    public void showGameUI() {
        Descriptions.gameObject.SetActive(true);
    }

    public void rebuildUI() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(workingBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(middleBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(upperBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(companyBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(policyBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Descriptions.transform as RectTransform);
    }

    // public void randomChangeText() {
    //     foreach(TextMeshProUGUI text in textGroup)
    //         text.text = randomText[Random.Range(0, randomText.Length)];
    //     LayoutRebuilder.ForceRebuildLayoutImmediate(descriptions as RectTransform);
    //     LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    // }



}

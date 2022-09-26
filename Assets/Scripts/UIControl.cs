using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI yearText, workingText, middleText, upperText, companyText, policymakerText, monthText, secondText,
                                             workingEndText, middleEndText, upperEndText, companyEndText, policyEndText, endGameSecondText;
    [SerializeField] private Color workingColor, middleColor, upperColor, companyColor, policymakerColor;
    [SerializeField] RectTransform descriptions, workingBG, middleBG, upperBG, companyBG, policyBG, leftSection, rightSection, 
                                   workingContainer, middleContainer, upperContainer, companyContainer, policyContainer, uiContainer, gameEndUiContainer;
    private List<TextMeshProUGUI> textGroup;

    //animations
    private Animator uiAnimator, endUiAnimator;

    //months
    private string[] months = new string[]{"", "Janurary", "Feburary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
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
    public TextMeshProUGUI WorkingEndText{get=>workingEndText;}
    public TextMeshProUGUI MiddleEndText{get=>middleEndText;}
    public TextMeshProUGUI UpperEndText{get=>upperEndText;}
    public TextMeshProUGUI CompanyEndText{get=>companyEndText;}
    public TextMeshProUGUI PolicyEndText{get=>policyEndText;}
    public RectTransform Descriptions{get=>descriptions;}
    public int CurrentMonthIndex{get=>currentMonthIndex;set=>currentMonthIndex=value;}
    public TextMeshProUGUI SecondText{get=>secondText;set=>secondText=value;}
    public TextMeshProUGUI EndGameSecondText{get=>endGameSecondText;set=>endGameSecondText=value;}

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

        uiAnimator = uiContainer.GetComponent<Animator>();
        endUiAnimator = gameEndUiContainer.GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager != null)
            yearText.text = gameManager.CurrentYear+"";
    }

    public IEnumerator monthCountDown() {
        if (monthText.text.Equals("December")) {
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
        uiContainer.gameObject.SetActive(false);
        uiAnimator.SetBool("Exit", true);
        uiAnimator.SetBool("Enter", false);
    }

    public void showGameUI() {
        uiContainer.gameObject.SetActive(true);
        uiAnimator.SetBool("Enter", true);
        uiAnimator.SetBool("Exit", false);
        endUiAnimator.SetBool("Exit", true);
        endUiAnimator.SetBool("Enter", false);
    }

    public void showGameEndUI() {
        gameEndUiContainer.gameObject.SetActive(true);
        uiAnimator.SetBool("Exit", true);
        uiAnimator.SetBool("Enter", false);
        endUiAnimator.SetBool("Enter", true);
        endUiAnimator.SetBool("Exit", false);

    }

    public void hideGameEndUI() {
        endUiAnimator.SetBool("Exit", true);
        endUiAnimator.SetBool("Enter", false);
        uiAnimator.SetBool("Enter", false);
    }

    public void rebuildUI() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(workingBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(middleBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(upperBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(companyBG.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(policyBG.transform as RectTransform);

        if (Descriptions != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(Descriptions.transform as RectTransform);
        if (leftSection != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(leftSection.transform as RectTransform);
        if (rightSection != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(rightSection.transform as RectTransform);
        if (workingContainer != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(workingContainer.transform as RectTransform);
        if (middleContainer != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(middleContainer.transform as RectTransform);
        if (upperContainer != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(upperContainer.transform as RectTransform);
        if (companyContainer != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(companyContainer.transform as RectTransform);
        if (policyContainer != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(policyContainer.transform as RectTransform);

        if (gameEndUiContainer != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(gameEndUiContainer.transform as RectTransform);
    }
}

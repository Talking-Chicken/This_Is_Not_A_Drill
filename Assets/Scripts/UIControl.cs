using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI yearText, workingText, middleText, upperText, companyText, policymakerText;
    [SerializeField] private Color workingColor, middleColor, upperColor, companyColor, policymakerColor;
    [SerializeField] RectTransform descriptions;
    private List<TextMeshProUGUI> textGroup;

    [SerializeField] private string[] randomText;

    //getters & setters
    public TextMeshProUGUI WorkingText{get=>workingText;}
    public TextMeshProUGUI MiddleText{get=>middleText;}
    public TextMeshProUGUI UpperText{get=>upperText;}
    public TextMeshProUGUI CompanyText{get=>companyText;}
    public TextMeshProUGUI PolicymakerText{get=>policymakerText;}
    public RectTransform Descriptions{get=>descriptions;}

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        workingText.color = workingColor;
        middleText.color = middleColor;
        upperText.color = upperColor;
        companyText.color = companyColor;
        policymakerText.color = policymakerColor;
        
        textGroup = new List<TextMeshProUGUI>() {workingText, middleText, upperText, companyText, policymakerText};
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

    public void randomChangeText() {
        foreach(TextMeshProUGUI text in textGroup)
            text.text = randomText[Random.Range(0, randomText.Length)];
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptions as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }



}

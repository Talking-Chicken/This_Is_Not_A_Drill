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
    }

    public void randomChangeText() {
        foreach(TextMeshProUGUI text in textGroup)
            text.text = randomText[Random.Range(0, randomText.Length)];
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptions as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }

}

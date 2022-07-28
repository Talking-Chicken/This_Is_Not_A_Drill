using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI yearText, workingText, middleText, upperText, companyText, policymakerText;
    [SerializeField] private Color workingColor, middleColor, upperColor, companyColor, policymakerColor;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        workingText.color = workingColor;
        middleText.color = middleColor;
        upperText.color = upperColor;
        companyText.color = companyColor;
        policymakerText.color = policymakerColor;
    }

    void Update()
    {
        if (gameManager != null)
            yearText.text = gameManager.CurrentYear+"";
    }
}

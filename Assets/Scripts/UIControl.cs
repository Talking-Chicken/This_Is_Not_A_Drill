using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI yearText;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        yearText.text = gameManager.CurrentYear+"";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerOfIntro : MonoBehaviour
{
    [SerializeField] private float totalDuration;
    private float currentTime = 0.0f;
    private TextMeshProUGUI timeText;
    void Start()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
        currentTime = 0.0f;
    }

    
    void Update()
    {
        if (currentTime <= totalDuration)
            timeText.text = (int)(totalDuration - currentTime)+"";
        else {
            currentTime = 0.0f;
            SceneManager.LoadScene(2);
        }
    }
}

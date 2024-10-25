using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] HealthScript healthScript;
    

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        // healthScript = FindObjectOfType<HealthScript>();
    }

    private void Start()
    {
        healthSlider.maxValue = healthScript.GetHealthPoints();
    }

    void Update()
    {
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("00000000000");
        healthSlider.value = healthScript.GetHealthPoints();
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Button Setup")] 
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button shootButton;
    
    [Header("UI Setup")]
    [SerializeField] private TMP_Text greetingText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Image Setup")]
    [SerializeField] private Image crossHair;
    
    public static event Action OnStartButtonPressed;
    public static event Action OnRestartButtonPressed;
    public static event Action OnShootButtonPressed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(OnUIStartButtonPressed);
        restartButton.onClick.AddListener(OnUIRestartButtonPressed);
        shootButton.onClick.AddListener(OnUIShootButtonPressed);
        
        restartButton.gameObject.SetActive(false);
        shootButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);

        scoreText.text = "Score: 0";
    }

    void OnUIStartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
        
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        shootButton.gameObject.SetActive(true);
        //greetingText.text = "SHOOT THE ENEMY!!!";
        greetingText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    void OnUIRestartButtonPressed()
    {
        OnRestartButtonPressed?.Invoke();
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        shootButton.gameObject.SetActive(false);
        greetingText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);
    }

    void OnUIShootButtonPressed()
    {
        OnShootButtonPressed?.Invoke();
    }

    public void UpdateDateScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }
}

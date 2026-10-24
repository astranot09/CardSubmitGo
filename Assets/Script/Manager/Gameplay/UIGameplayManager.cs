using UnityEngine;
using TMPro;

public class UIGameplayManager : MonoBehaviour
{

    [Header("Panel")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject guessCardPanel;

    [Header("UI -- Score")]
    [SerializeField] private TMP_Text scoreText;

    [Header("UI -- Health")]
    [SerializeField] private TMP_Text playerHealth;

    [SerializeField] private GameplayManager gameplayManager;

    private void OnEnable()
    {
        gameplayManager.OnGameStart += OpenGameplayPanel;

        gameplayManager.OnResetAll += UpdateScoreUI;
        gameplayManager.OnResetAll += UpdateHealthUI;

        gameplayManager.OnPlayerGuess += ShowGuessNextCardUI;

        gameplayManager.OnConclusion += CloseGuessNextCardUI;



        gameplayManager.OnHealthChange += UpdateHealthUI;
        gameplayManager.OnScoreChange += UpdateScoreUI;

    }

    private void OnDisable()
    {
        gameplayManager.OnGameStart -= OpenGameplayPanel;

        gameplayManager.OnResetAll -= UpdateScoreUI;
        gameplayManager.OnResetAll += UpdateHealthUI;

        gameplayManager.OnPlayerGuess -= ShowGuessNextCardUI;

        gameplayManager.OnConclusion -= CloseGuessNextCardUI;

        gameplayManager.OnHealthChange -= UpdateHealthUI;
        gameplayManager.OnScoreChange -= UpdateScoreUI;

    }


    public void OpenGameplayPanel()
    {
        Debug.Log("Buka Panel");
        UIManager.instance.OpenPanel(gameplayPanel);
    }

    public void CloseGameplayPanel()
    {
        UIManager.instance.ClosePanel(gameplayPanel);
    }



    //=========== SCORE ===========
    public void UpdateScoreUI()
    {
        scoreText.text = GameplayManager.instance.PlayerScore.ToString();
    }


    //=========== HEALTH ===========
    public void UpdateHealthUI()
    {
        playerHealth.text = GameplayManager.instance.CurrentHealth.ToString();
    }

    //=========== GAMEPLAY ===========

    public void ShowGuessNextCardUI()
    {
        UIManager.instance.OpenPanel(guessCardPanel);
    }
    public void CloseGuessNextCardUI()
    {
        UIManager.instance.ClosePanel(guessCardPanel);
    }
}

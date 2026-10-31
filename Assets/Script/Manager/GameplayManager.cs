using System;
using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [Header("Health")]
    [SerializeField] private int playerHealth = 5;
    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;

    [Header("Score")]
    [SerializeField] private int playerScore;
    public int PlayerScore => playerScore;

    [Header("Card")]
    [SerializeField] private CardData tableCard;
    [SerializeField] private CardData playerCard;
    private int playerGuess = 0; // -1 -> lower , 1 -> higher

    [Header("UI -- Card")]
    [SerializeField] private CardUIPrefab tableUICard;
    [SerializeField] private CardUIPrefab playerUICard;

    [Header("Setting Gameplay")]
    [SerializeField] private int scoreGain = 10;

    [Header("Setting")]
    [SerializeField] private float delayBeforeShowConclusion = 1f;
    [SerializeField] private float delayBeforeNextRound = 0.5f;

    public event Action OnGameStart; // first time enter game state
    public event Action OnResetAll; // after enter game state and before round start
    public event Action OnRoundStart;
    public event Action OnPlayerGuess;
    public event Action OnConclusion;
    public event Action OnRoundFinish;

    public event Action OnHealthChange;
    public event Action OnScoreChange;

    public void GameStart()
    {
        OnGameStart?.Invoke();
        GameReset();
    }

    public void GameReset()
    {
        playerScore = 0;
        currentHealth = playerHealth;
        OnResetAll?.Invoke();
        RoundStart();
    }

    //================== Game Flow ==============================

    public void TakeTableCard()
    {
        tableCard = CardManager.instance.GetCardData();
        tableUICard.CardUISetUp(tableCard);
        PlayerGuessNextCard();
    }

    public void PlayerGuessNextCard()
    {
        OnPlayerGuess?.Invoke();
    }

    public void PlayerGuessLower()
    {
        playerGuess = -1;
        ShowNextCard();
    }
    public void PlayerGuessHigher()
    {
        playerGuess = 1;
        ShowNextCard();
    }
    public void ShowNextCard()
    {
        playerCard = CardManager.instance.GetCardData();
        playerUICard.CardUISetUp(playerCard);
        OnConclusion?.Invoke();
        StartCoroutine(ConclusionRound());
    }

    private IEnumerator ConclusionRound()
    {
        yield return new WaitForSeconds(delayBeforeShowConclusion);
        CheckConclusion();
        yield return new WaitForSeconds(delayBeforeNextRound);
        RoundFinish();

    }

    private void CheckConclusion()
    {
        if((tableCard.cardValue > playerCard.cardValue && playerGuess == -1) || (tableCard.cardValue < playerCard.cardValue && playerGuess == 1))
        {
            ConclusionCorrect();
        }
        else if(tableCard.cardValue == playerCard.cardValue)
        {
            bool playerHigher = CardManager.instance.CheckPlayerTypeHierarchy(playerCard.cardType, tableCard.cardType);

            if((playerHigher && playerGuess == 1) || (!playerHigher && playerGuess == -1))
            {
                ConclusionCorrect();
            }
            else
            {
                ConclusionFalse();
            }
        }
        else
        {
            ConclusionFalse();
        }
    }

    private void ConclusionCorrect()
    {
        Debug.Log("Conclusion Correct");
        AddScoreValue(scoreGain);
    }
    private void ConclusionFalse()
    {
        Debug.Log("ConclusionFalse");
        ChangeHealthValue(-1);
    }

    private void RoundStart()
    {
        playerGuess = 0;
        OnRoundStart?.Invoke();
        TakeTableCard();
    }
    private void RoundFinish()
    {
        OnRoundFinish?.Invoke();

        if (currentHealth > 0)
        {
            RoundStart();
        }
    }


    //================== Player Health ==============================

    public void ChangeHealthValue(int value)
    {
        currentHealth += value;
        OnHealthChange?.Invoke();
        CheckHealth();
    }


    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            StateManager.instance.SwitchState(GameState.GameOverState);
        }
    }

    //================== Player Score ==============================

    public void AddScoreValue(int value)
    {
        playerScore += value;
        OnScoreChange?.Invoke();
    }

}

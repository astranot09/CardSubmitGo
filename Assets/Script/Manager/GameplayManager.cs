using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int playerHealth = 5;
    [SerializeField] private int currentHealth;

    [Header("Score")]
    [SerializeField] private int playerScore;



    public void GameStart()
    {
        playerScore = 0;
        currentHealth = playerHealth;
    }


    //================== Player Health ==============================

    public void ChangeHealthValue(int value)
    {
        currentHealth += value;

        CheckHealth();
    }


    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            GameManager.instance.SwitchState(GameState.GameOverState);
        }
    }

    //================== Player Score ==============================

    public void AddScoreValue(int value)
    {
        playerScore += value;
    }
}

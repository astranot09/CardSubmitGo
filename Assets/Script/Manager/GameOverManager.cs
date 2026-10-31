using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private GameObject gameOverPanel;


    public void PlayerGameOver()
    {
        UIManager.instance.OpenPanel(gameOverPanel);
        Debug.Log("Player Game Over");
    }

    public void RestartGame()
    {
        UIManager.instance.ClosePanel(gameOverPanel);
        StateManager.instance.SwitchState(GameState.GameplayState);
    }
    public void BackToMainMenu()
    {
        UIManager.instance.ClosePanel(gameOverPanel);
        StateManager.instance.SwitchState(GameState.MainMenuState);
    }
}

using UnityEngine;

public enum GameState
{
    MainMenuState,
    GameplayState,
    GameOverState
}


public class StateManager : MonoBehaviour
{

    public static StateManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private GameState currentGameState;


    //================== Game State Logic =====================================
    public void SwitchState(GameState newState)
    {
        if(currentGameState == newState) return;

        CheckStateTransition(newState);

        currentGameState = newState;
    }

    private void CheckStateTransition(GameState newState)
    {

        UIManager.instance.CloseAllPanels();

        switch (newState)
        {
            case GameState.MainMenuState:

                MainMenuManager.instance.SetUpMainMenu();

                break;

            case GameState.GameplayState:

                GameplayManager.instance.GameStart();

                break;

            case GameState.GameOverState:
                GameOverManager.instance.PlayerGameOver();
                break;
        }
    }


}
